using Lzo64;
using System;
using System.IO;

namespace PaladinsTfc
{
  class Program
  {
    private static bool logPeekTexData = true;
    private static bool logTexData = false;
    private static bool writeTexture = false;

    private static int minDump = 120;
    private static int maxDump = 140;

    private struct TexBlock {
      long loc_texBlockStart;
      long loc_compressedSize;
      long loc_originalSize;
    }
    private struct Tex {
      long loc_textureStart;
      long loc_unknown1;
      long loc_totalTextureBytes;
      long loc_unknown2;
      List<TexBlock> blocks;
    }
    private struct TFCFile {
      string srcFile;
      List<Tex> texs;
    }

    private static bool isInDumpRange(int x){
      return (minDump <= x && x <= maxDump);
    }
    private static bool tryRead(BinaryReader reader){
      try{
        if (reader.ReadUInt32() == 0x9e2a83c1) { //Found start of Texture, some magic header constant
          return false;
        } else {
          //Console.WriteLine("why am i reading in the wrong location?");
          return true;
        }
      } catch(System.IO.EndOfStreamException) {
        Console.WriteLine("Reached end of file");
        Environment.Exit(0);
        return false;
      }
    }
    private static string peek(byte[] bs, int nPeek){
      string sx = "";
      for (int i = 0; i < nPeek; i++){
        if(i > 0 && i %4 == 0) sx+=" |";
        sx+= (" "+bs[i].ToString("x2"));
      }
      return sx;
    }
    private static void printpeek(FileStream fs, int nPeek){
      if( logPeekTexData == false ) return;

      byte[] bs = new byte[nPeek];
      fs.Read(bs, 0, (int)nPeek);
      fs.Seek(-((long)nPeek), SeekOrigin.Current);

      string sx = "F[0x" + fs.Position.ToString("X8") + "] =";
      Console.WriteLine(sx + peek(bs, nPeek));
    }

    // ====================================================== //
    private static void Main(string[] args) {
      Dictionary<int,string> texid2file = new Dictionary<int, string>();
      //texid2file.Add(121, null);
      //texid2file.Add(122, null);
      texid2file.Add(122, "example/CharTextures3PATCH_122_512xDXT1.dds");
      //texid2file.Add(123, null);
      //texid2file.Add(124, null);
      //testLZO();
      
      if(args.Length == 0)
        args = new String[1]{@"E:\BACKUP\wilux\git\paladins_tfc\example\CharTextures3PATCH.tfc"};
      String outFile = "out_tfc/CharTextures3PATCH.tfc";

      File.Copy(args[0], outFile, true);

      LZOCompressor lzo = new LZOCompressor();
      FileStream fs = new FileStream(outFile, FileMode.Open);
      BinaryReader reader = new BinaryReader((Stream) fs);

      if (Path.GetExtension(args[0]).ToLower() == ".tfc")
      {
        int nFilesDone = 0;
        int[] originalTextureBlockSizes = new int[200];
        int[] compressedTextureBlockSizes = new int[200]; 
        long[] loc_originalTextureBlockSizes = new long[200]; //memory location of header
        long[] loc_compressedTextureBlockSizes = new long[200]; //memory location of header

        //DDS textured are sorta limited to 512x512 but allows higher resoution by taking multiple encoded 512x512 textures stored after each other 
        //splices them together, guessing a DX limitation/optimisation of sorts?

        while (fs.Position < fs.Length) {
          long loc_searchStart = fs.Position;

          // searches backwards until it reaches "0x9e2a83c1", Starting bit identifier?
          while (tryRead(reader)) 
            fs.Seek(-3L, SeekOrigin.Current);

          //Texture 
          /* LITLLE ENDIAN!!
          C1 83 2A 9E | Block start identifier
          00 00 02 00 | ? (same for all textures)
          D2 72 04 00 | remaningBytes (total bytes for whole texture)
          00 00 08 00 | some kind of dimension info?
          29 1C 01 00 | compressedTextureBlockSize[0]
          00 00 02 00 | originalTextureBlockSize[0]
          D3 36 01 00 | compressedTextureBlockSize[1] 
          00 00 02 00 | originalTextureBlockSize[1] 
          ...
          XX XX XX XX | compressed texture blocks after each other, no separator or header
          ...
          C1 83 2A 9E | Next Block start identifier
          */
          long loc_textureStart = fs.Position;
          Console.WriteLine("FOUND tex " + nFilesDone + " at 0x" + loc_textureStart.ToString("X8") + " (searched from 0x" + loc_searchStart.ToString("X8") + ")");

          printpeek(fs, 32);

          int unknown1 = reader.ReadInt32(); //seems to equal 0x00020000 for all textures

          long loc_totalTextureBytes = fs.Position;
          int totalTextureBytes = reader.ReadInt32(); 
          int nTextureBlocks = 0;
          int unknown2 = reader.ReadInt32();
          
          int remaningBytes = totalTextureBytes;
          while (remaningBytes > 0)
          {
            loc_compressedTextureBlockSizes[nTextureBlocks] = fs.Position;
            compressedTextureBlockSizes[nTextureBlocks] = reader.ReadInt32();
            loc_originalTextureBlockSizes[nTextureBlocks] = fs.Position;
            originalTextureBlockSizes[nTextureBlocks] = reader.ReadInt32();
            remaningBytes -= compressedTextureBlockSizes[nTextureBlocks];

            int x = compressedTextureBlockSizes[nTextureBlocks];
            fs.Seek(x, SeekOrigin.Current);
            printpeek(fs, 32);
            fs.Seek(-x, SeekOrigin.Current);

            ++nTextureBlocks;
          }

          int ddsW = 512;
          int ddsH = 512;
          int ddsSize = originalTextureBlockSizes[0];

          const int dxt1 = 0x31545844; //DXT1
          const int dxt2 = 0x35545844; //DXT5

          if (ddsSize >= 0x0800) {

            int ddsFormat = dxt1;
            switch (nTextureBlocks)
            {
              case 2:
                ddsW = 512;
                ddsH = 512;
                ddsSize = ddsW * ddsH;
                ddsFormat = dxt2;
                break;
              case 4:
                ddsW = 1024;
                ddsH = 1024;
                ddsSize = ddsW * ddsH / 2;
                ddsFormat = dxt1;
                break;
              case 8:
                ddsW = 1024;
                ddsH = 1024;
                ddsSize = ddsW * ddsH;
                ddsFormat = dxt2;
                break;
              case 16:
                ddsW = 2048;
                ddsH = 2048;
                ddsSize = ddsW * ddsH / 2;
                ddsFormat = dxt1;
                break;
              default:
                switch (ddsSize)
                {
                  case 0x0800:
                    ddsW = 64;
                    ddsH = 64;
                    ddsSize = ddsW * ddsH / 2;
                    ddsFormat = dxt1;
                    break;
                  case 0x1000:
                    ddsW = 64;
                    ddsH = 64;
                    ddsSize = ddsW * ddsH;
                    ddsFormat = dxt2;
                    break;
                  case 0x2000:
                    ddsW = 128;
                    ddsH = 128;
                    ddsSize = ddsW * ddsH / 2;
                    ddsFormat = dxt1;
                    break;
                  case 0x4000:
                    ddsW = 128;
                    ddsH = 128;
                    ddsSize = ddsW * ddsH;
                    ddsFormat = dxt2;
                    break;
                  case 0x8000:
                    ddsW = 256;
                    ddsH = 256;
                    ddsSize = ddsW * ddsH / 2;
                    ddsFormat = dxt1;
                    break;
                  case 0x10000 :
                    ddsW = 256;
                    ddsH = 256;
                    ddsSize = ddsW * ddsH;
                    ddsFormat = dxt2;
                    break;
                  case 0x20000:
                    ddsW = 512;
                    ddsH = 512;
                    ddsSize = ddsW * ddsH / 2;
                    ddsFormat = dxt1;
                    break;
                }
                break;
            }

            // File creating / Insertion
            if (isInDumpRange(nFilesDone)) {
              string ddsFormatName = ddsFormat == dxt1 ? "DXT1" : "DXT2";
              string withoutExtension = Path.GetFileNameWithoutExtension(args[0]);
              String outPath = "out_textures/" + withoutExtension + "_" + nFilesDone.ToString("d3") + "_" + ddsW + "x" + ddsFormatName + ".dds";
              Directory.CreateDirectory(Path.GetDirectoryName(outPath));
              FileStream fsDump = new FileStream(outPath, FileMode.Create);
              BinaryWriter bwDump = new BinaryWriter((Stream) fsDump);

              // DDS spec: https://docs.microsoft.com/en-us/windows/win32/direct3ddds/dx-graphics-dds-pguide
              // writing DDS header
              bwDump.Write(0x20534444); // dwMagic (constant to identify that this is a dds file)
              bwDump.Write(0x7c);       // header
              bwDump.Write(0x1007);     // header DX10
              bwDump.Write(ddsH);
              bwDump.Write(ddsW);
              bwDump.Write(ddsSize);
              bwDump.Write(0);
              bwDump.Write(1);
              fsDump.Seek(44L, SeekOrigin.Current);
              bwDump.Write(32);
              bwDump.Write(4);
              bwDump.Write(ddsFormat);
              fsDump.Seek(40L, SeekOrigin.Current);
              // writing DDS texture
              long textureStart = fs.Position;
              for (int i = 0; i < nTextureBlocks; ++i)
              {
                long loc_textureBlock = fs.Position;

                byte[] compressedTextureBlock = new byte[compressedTextureBlockSizes[i]];
                fs.Read(compressedTextureBlock, 0, compressedTextureBlockSizes[i]);
                byte[] buffer = lzo.Decompress(compressedTextureBlock, originalTextureBlockSizes[i]);
                fsDump.Write(buffer, 0, buffer.Length);
                long textureBlockEnd = fs.Position;
                
                if(texid2file.ContainsKey(nFilesDone)){
                  string replacementFPath = texid2file[nFilesDone];
                  Console.WriteLine("Replacing tex " + nFilesDone + ":" + i + " with " + replacementFPath);

                  if(replacementFPath != null){                   
                    fs.Seek(loc_textureBlock, SeekOrigin.Begin);
                    byte[] empty = new byte[compressedTextureBlockSizes[i]];
                    fs.Write(empty, 0, compressedTextureBlockSizes[i]);

                    byte[] rawread = File.ReadAllBytes(replacementFPath).Skip(0x80).ToArray(); //strip DDS header
                    int rawreadLength = rawread.Length;

                    if(rawreadLength != originalTextureBlockSizes[i]){
                      throw new Exception("replacement texture block is not the same size texture its trying to replace, wrong dds format?");
                    }

                    Console.WriteLine(String.Format("Replacedata: len=0x{0} data = {1}", rawreadLength.ToString("X8") , peek(rawread, 32)));

                    byte[] lzo999compressed = lzo.Compress(rawread);
                    int compressedSize = lzo999compressed.Length;
                    
                    /*
                    byte[] testuncompress = lzo.Decompress(lzo999compressed, originalTextureBlockSizes[i]);
                    if( testuncompress.SequenceEqual(rawread) == false){
                      throw new Exception("Compression failed, how the fuck is this happening");
                    }*/

                    fs.Seek(loc_textureBlock, SeekOrigin.Begin);
                    if(compressedSize <= compressedTextureBlockSizes[i]){
                      fs.Write(empty, 0, compressedSize);

                      fs.Seek(loc_compressedTextureBlockSizes[i], SeekOrigin.Begin); 
                      byte[] blockLengthHeader = BitConverter.GetBytes(compressedSize);
                      fs.Write(blockLengthHeader, 0, 4);

                      //this causes mega errors for multiple blocks but it works for now
                      fs.Seek(loc_totalTextureBytes, SeekOrigin.Begin);
                      fs.Write(blockLengthHeader, 0, 4);

                      fs.Seek(loc_textureBlock, SeekOrigin.Begin);
                      fs.Write(lzo999compressed, 0, compressedSize);

                      Console.WriteLine(String.Format(
                        "\nReplaced tex {0}:{1}, blockSize: 0x{2} -> 0x{3}, totalTextureBytes: 0x{4} -> 0x{5}", 
                        nFilesDone,
                        i,
                        compressedTextureBlockSizes[i].ToString("X8"), 
                        compressedSize.ToString("X8"),
                        totalTextureBytes.ToString("X8"),
                        compressedSize.ToString("X8")
                      ));
                      fs.Seek(loc_textureStart, SeekOrigin.Begin);
                      Console.WriteLine("Post insertion:");
                      printpeek(fs, 32);
                      Console.WriteLine("");
                    } else {
                      throw new ArgumentException(string.Format("Image can not be sufficently compressed {0} > {1} try reducing noise", compressedSize, compressedTextureBlockSizes[i]));
                    }
                  }

                  fs.Seek(textureBlockEnd, SeekOrigin.Begin);
                }
                Console.WriteLine(
                  "orgSize=0x" + originalTextureBlockSizes[i].ToString("X8") 
                  + " comprSize=0x" + compressedTextureBlock[i].ToString("X8") 
                  + " for "+nFilesDone+":"+i);
              }
              long textureEnd = fs.Position;

              fsDump.Close();
              bwDump.Close();
              
              if(logTexData){  
                if(unknown1 != 0x00020000){
                  Console.WriteLine("unknown1 = 0x" + unknown1.ToString("X8"));
                }            
                Console.WriteLine("UNK2 = 0x" + unknown2.ToString("X8") + " " + unknown2 
                  + " DDSH=" + ddsH 
                  + " UNK2/DDSH=" + unknown2/ddsH 
                  + " TXID=" + (nFilesDone) 
                  + " DT=" + ddsFormatName
                  + " NBLKS=" + (nTextureBlocks+1)
                  + " TXstart=0x" + textureStart.ToString("X8")
                  + " TXend=0x" + textureEnd.ToString("X8")
                  + " TXlen=0x" + (textureEnd-textureStart).ToString("X8")
                );
              }          
            }
            
            ++nFilesDone;    

            if(nFilesDone > maxDump){
              break;
            }
          }
        }
      }

      reader.Close();
      fs.Close();
    }

    public static void testLZO(){
      //int origLen = "0x0001152E";
      LZOCompressor lzo = new LZOCompressor();
      byte[] compressed = File.ReadAllBytes("SULFPIP_rawcompressed.bin");
      byte[] decompressed = lzo.Decompress(compressed, 0x20000);

      byte[] recompressed = lzo.Compress(decompressed);

      Console.WriteLine(
        "ORIGSIZE=0x" + compressed.Length.ToString("X8")
        + "\nSRCSIZE=0x" + decompressed.Length.ToString("X8")
        + "\nNEWSIZE=0x" + recompressed.Length.ToString("X8")
      );

      Environment.Exit(0);
    }
  }
}

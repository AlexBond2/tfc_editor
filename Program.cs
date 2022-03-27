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
    private static bool compareReplacement = true;

    private static int minDump = 120;
    private static int maxDump = 130;

    public class TexBlock {
      public long loc_compressedSize;
      public int compressedSize;
      public long loc_originalSize;
      public int originalSize;

      public long loc_texBlockStart; //actual compressed data begins here
    }
    public class Tex {
      public int id;

      public long loc;
      public long loc_unknown1;
      public int unknown1;
      public long loc_totalTextureBytes;
      public int totalTextureBytes;
      public long loc_unknown2;
      public int unknown2;
      public long loc_blockHeaderStart;

      public List<TexBlock> blocks;
    }
    public class TFCInfo {
      public string srcFile;
      public List<Tex> texs;
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

    private static TFCInfo getTFCInfo(string tfcFile){
      if (Path.GetExtension(tfcFile).ToLower() != ".tfc") {
        throw new ArgumentException("File is not a TFC file");
      }

      FileStream fs = new FileStream(tfcFile, FileMode.Open);
      BinaryReader br = new BinaryReader((Stream) fs);
      
      //Tfc Texture 
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

      List<Tex> texs = new List<Tex>();
      while (fs.Position < fs.Length) {
        long loc_searchStart = fs.Position;
        while (tryRead(br)) 
          fs.Seek(-3L, SeekOrigin.Current);
        long loc_textureStart = fs.Position;

        Console.WriteLine("FOUND tex " + texs.Count() + " at 0x" + loc_textureStart.ToString("X8") + " (searched from 0x" + loc_searchStart.ToString("X8") + ")");
        printpeek(fs, 32);
        Console.WriteLine("Begin blocks");
        
        Tex tex = new Tex();
        tex.loc = fs.Position;
        tex.loc_unknown1 = fs.Position;
        tex.unknown1 = br.ReadInt32();
        tex.loc_totalTextureBytes = fs.Position;
        tex.totalTextureBytes = br.ReadInt32(); 
        tex.loc_unknown2 = fs.Position;
        tex.unknown2 = br.ReadInt32(); 
        tex.loc_blockHeaderStart = fs.Position;
        tex.id = texs.Count();

        tex.blocks = new List<TexBlock>();
        int remaningBytes = tex.totalTextureBytes;
        while (remaningBytes > 0) {
          TexBlock block = new TexBlock();
          block.loc_compressedSize = fs.Position;
          block.compressedSize = br.ReadInt32();
          block.loc_originalSize = fs.Position;
          block.originalSize = br.ReadInt32();

          tex.blocks.Add(block);
          remaningBytes -= block.compressedSize;
        }

        Console.WriteLine(String.Format("tex {0} @ {1}, NBLOCKS = {2}, NBYTE = {3}, BLOCKHEADER = {4}, LOC:TEXNBYTE = {5}",
          tex.id,
          "0x"+tex.loc.ToString("X8"), 
          tex.blocks.Count(), 
          "0x"+tex.totalTextureBytes.ToString("X8"),
          "0x"+tex.loc_blockHeaderStart.ToString("X8"),          
          "0x"+tex.loc_totalTextureBytes.ToString("X8")
        ));

        long offset = 0;
        for (int i = 0; i < tex.blocks.Count(); i++){
          tex.blocks[i].loc_texBlockStart = tex.loc_blockHeaderStart + tex.blocks.Count()*8 + offset;
          offset += tex.blocks[i].compressedSize;
        }
        TexBlock last = tex.blocks.Last();
        long endoftexA = last.loc_texBlockStart + last.compressedSize;
        long endoftexB = tex.loc_blockHeaderStart + tex.blocks.Count()*8 + tex.totalTextureBytes;

        //Console.WriteLine("loc_texBlockStart: 0x" + last.loc_texBlockStart.ToString("X8"));
        //Console.WriteLine("loc_blockHeaderStart: 0x" + tex.loc_blockHeaderStart.ToString("X8"));
        if(endoftexA != endoftexB){
          fs.Seek(tex.loc, SeekOrigin.Begin);
          printpeek(fs, 32);
          throw new Exception(String.Format("I am dumb 0x{0} != 0x{1}", endoftexA.ToString("X8"), endoftexB.ToString("X8")));
        }
        fs.Seek(endoftexA, SeekOrigin.Begin);

        /*if(tex.id == 126){
          throw new Exception("STOP");
        }/**/

        texs.Add(tex);
        Console.WriteLine(string.Format("Tex {0} done, a' {1}", tex.id, tex.blocks.Count()));
      }

      fs.Close();
      br.Close();

      TFCInfo tfcinfo = new TFCInfo();
      tfcinfo.srcFile = tfcFile;
      tfcinfo.texs = texs;

      Console.WriteLine(string.Format("TFC {0} done, contains {1} textures", tfcinfo.srcFile, tfcinfo.texs.Count()));
      
      return tfcinfo;
    }

    private static void dumpTex(Tex tex, FileStream fs, LZOCompressor lzo, string fileName){
      if(isInDumpRange(tex.id) == false) return;

      int ddsW = 512;
      int ddsH = 512;

      const int dxt1 = 0x31545844; //DXT1
      const int dxt2 = 0x35545844; //DXT5

      if(tex.blocks.Count() >= 1 && tex.blocks[0].originalSize >= 0x0800) { //if texture too small this switch statement does not work
        int ddsSize = tex.blocks[0].originalSize;
        int ddsFormat = -1;
        switch (tex.blocks.Count()) {
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
            switch (ddsSize) {
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
        
        string ddsFormatName = ddsFormat == dxt1 ? "DXT1" : "DXT2";
        string withoutExtension = Path.GetFileNameWithoutExtension(fileName);
        String outPath = string.Format("out_textures/{0}_{1}_{2}x{3}.dds", 
          withoutExtension, 
          tex.id.ToString("d3"),
          ddsW,
          ddsFormatName
        );
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

        for (int i = 0; i < tex.blocks.Count(); ++i) {
          TexBlock block = tex.blocks[i];

          fs.Seek(block.loc_texBlockStart, SeekOrigin.Begin);
          
          byte[] compressedTextureBlock = new byte[block.compressedSize];
          fs.Read(compressedTextureBlock, 0, block.compressedSize);
          byte[] buffer = lzo.Decompress(compressedTextureBlock, block.originalSize);
          fsDump.Write(buffer, 0, buffer.Length);
        }
      } else {
        Console.WriteLine(String.Format("Texture {0} is too small, skipping", tex.id));
      }
    }

    private static void replaceTexture(TFCInfo tfcinfo, FileStream fs, LZOCompressor lzo, int id, string replacementDDSPath) {
      Tex tex = tfcinfo.texs[id];
      if (tex.id != id)
        throw new Exception("RedundantDataException, tex id are wrong");

      if(tex.blocks.Count() != 1)
        throw new ArgumentException("[TODO ERROR] Can not work with texture containing more than 1 block YET => only replace 512x or smaller textures");

      //TODO? check if dds header is correct
      byte[] rawread = File.ReadAllBytes(replacementDDSPath).Skip(0x80).ToArray(); //strip dds header
      Console.WriteLine("Replacing tex " + tex.id + ":0" + " with " + replacementDDSPath);

      TexBlock block = tex.blocks[0];
      int blockId = 0;

      if(rawread.Length != block.originalSize)
        throw new Exception("replacement texture block is not the same size texture its trying to replace");

      byte[] lzo999compressed = lzo.Compress(rawread);
      int newSize = lzo999compressed.Length;
      
      if(newSize > block.compressedSize)
        throw new Exception("replacement texture can not be sufficently compressed. Reducing noise in the texture should fix this");
      
      fs.Seek(tex.loc, SeekOrigin.Begin);
      Console.Write("Replace pre :");
      printpeek(fs, 32);

      //Write headers
      fs.Seek(block.loc_compressedSize, SeekOrigin.Begin); 
      byte[] injectCompressedSize = BitConverter.GetBytes(newSize);
      fs.Write(injectCompressedSize, 0, 4);

      fs.Seek(block.loc_originalSize, SeekOrigin.Begin); 
      byte[] injectOriginalSize = BitConverter.GetBytes(block.originalSize); // originalsize should (probably) be unchanged for the replacement so this code does nothing. But it does make it more understandable.
      fs.Write(injectOriginalSize, 0, 4);

      //Write data
      fs.Seek(block.loc_texBlockStart, SeekOrigin.Begin);
      fs.Write(new byte[block.compressedSize], 0, block.compressedSize); //overwrite whole source data with 0, this is maybe unneccesary but makes the output easier to diagnose
      fs.Seek(block.loc_texBlockStart, SeekOrigin.Begin);
      fs.Write(lzo999compressed, 0, newSize);

      fs.Seek(tex.loc_totalTextureBytes, SeekOrigin.Begin); //MEGA ERROR for multiple block texs
      fs.Write(injectCompressedSize, 0, 4);

      
      fs.Seek(tex.loc, SeekOrigin.Begin);
      Console.Write("Replace post:");
      printpeek(fs, 32);

      Console.WriteLine(string.Format(
        "Replaced tex {0}:{1}, blockSize: 0x{2} -> 0x{3}, totalTextureBytes: 0x{4} -> 0x{5}\n", 
        tex.id,
        blockId,
        block.compressedSize.ToString("X8"), 
        newSize.ToString("X8"),
        tex.loc_totalTextureBytes.ToString("X8"),
        newSize.ToString("X8")
      ));
    }
    // ====================================================== //
    private static void Main(string[] args) {
      Dictionary<int,string> texid2file = new Dictionary<int, string>();
      //texid2file.Add(121, null);
      //texid2file.Add(122, null);
      //texid2file.Add(122, "example/CharTextures3PATCH_122_512xDXT1.dds");
      //texid2file.Add(123, null);
      //texid2file.Add(124, null);
      //testLZO();
      
      if(args.Length == 0)
        args = new String[1]{@"E:\BACKUP\wilux\git\paladins_tfc\example\CharTextures3PATCH.tfc"};
      string inFile = args[0];

      TFCInfo tf = getTFCInfo(inFile);
      FileStream fsIn = new FileStream(inFile, FileMode.Open);
      LZOCompressor lzo = new LZOCompressor();
      foreach (Tex tex in tf.texs) {
        dumpTex(tex, fsIn, lzo, inFile);
      }
      fsIn.Close();
      
      string outFile = "out_tfc/CharTextures3PATCH.tfc";
      File.Copy(inFile, outFile, true);
      FileStream fsOut = new FileStream(outFile, FileMode.Open);
      //replaceTexture(tf, fsOut, lzo, 121, "example/CharTextures3PATCH_122_1024xDXT1.dds");
      replaceTexture(tf, fsOut, lzo, 122, "example/CharTextures3PATCH_122_512xDXT1.dds");
      replaceTexture(tf, fsOut, lzo, 123, "example/CharTextures3PATCH_122_256xDXT1.dds");
      replaceTexture(tf, fsOut, lzo, 124, "example/CharTextures3PATCH_122_128xDXT1.dds");
      //replaceTexture(tf, fsw, lzox, 126, "example/Io_default_specular_512xDXT1.dds");
      fsOut.Close();
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

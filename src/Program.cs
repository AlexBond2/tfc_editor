using Lzo64;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace PaladinsTfc
{
  class Program
  {
    private static bool logPeekTexData = true;
    private static bool logTexDataMake = false;
    private static bool logTexData = false;
    private static bool checkCompression = false;
    private static bool writeTexture = false;
    private static bool compareReplacement = false;

    private static string xfile;
    private static string xdump;
    private static string xreplace;

    private static HashSet<int> dumpRange;
    private static bool dumpAll;

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
      return dumpAll || dumpRange.Contains(x);
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

      long loc = fs.Position;
      byte[] bs = new byte[nPeek];
      fs.Read(bs, 0, (int)nPeek);
      fs.Seek(-((long)nPeek), SeekOrigin.Current);

      string sx = "F[0x" + loc.ToString("X8") + "] =";
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

        if(logTexDataMake) {
          Console.WriteLine("FOUND tex " + texs.Count + " at 0x" + loc_textureStart.ToString("X8") + " (searched from 0x" + loc_searchStart.ToString("X8") + ")");
          printpeek(fs, 32);
          Console.WriteLine("Begin blocks");
        }
        
        Tex tex = new Tex();
        tex.loc = fs.Position;
        tex.loc_unknown1 = fs.Position;
        tex.unknown1 = br.ReadInt32();
        tex.loc_totalTextureBytes = fs.Position;
        tex.totalTextureBytes = br.ReadInt32(); 
        tex.loc_unknown2 = fs.Position;
        tex.unknown2 = br.ReadInt32(); 
        tex.loc_blockHeaderStart = fs.Position;
        tex.id = texs.Count;

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

        if (logTexDataMake){
          Console.WriteLine(String.Format("tex {0} @ {1}, NBLOCKS = {2}, NBYTE = {3}, BLOCKHEADER = {4}, LOC:TEXNBYTE = {5}",
            tex.id,
            "0x" + tex.loc.ToString("X8"),
            tex.blocks.Count,
            "0x" + tex.totalTextureBytes.ToString("X8"),
            "0x" + tex.loc_blockHeaderStart.ToString("X8"),
            "0x" + tex.loc_totalTextureBytes.ToString("X8")
          ));
        }

        long offset = 0;
        for (int i = 0; i < tex.blocks.Count; i++){
          tex.blocks[i].loc_texBlockStart = tex.loc_blockHeaderStart + tex.blocks.Count*8 + offset;
          offset += tex.blocks[i].compressedSize;
        }
        TexBlock last = tex.blocks[tex.blocks.Count-1];
        long endoftexA = last.loc_texBlockStart + last.compressedSize;
        long endoftexB = tex.loc_blockHeaderStart + tex.blocks.Count*8 + tex.totalTextureBytes;

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

        if (logTexDataMake) 
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
      int ddsW = 512;
      int ddsH = 512;

      const int dxt1 = 0x31545844; //DXT1
      const int dxt2 = 0x35545844; //DXT5

      if(tex.blocks.Count >= 1 && tex.blocks[0].originalSize >= 0x0800) { //if texture too small this switch statement does not work
        Console.WriteLine(String.Format("Dumping Texture {0}", tex.id));

        int ddsSize = tex.blocks[0].originalSize;
        int ddsFormat = -1;
        switch (tex.blocks.Count) {
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
        
        string ddsFormatName = ddsFormat == dxt1 ? "DXT1" : "DXT5";
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

        for (int i = 0; i < tex.blocks.Count; ++i) {
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
      GC.Collect();
    }

    //Does not update tfcInfo
    private static void replaceTexture(TFCInfo tfcinfo, FileStream fs, LZOCompressor lzo, int id, string replacementDDSPath) {
      Tex tex = tfcinfo.texs[id];
      if (tex.id != id)
        throw new Exception("RedundantDataException, tex id is wrong");

      Console.WriteLine("Replacing tex " + tex.id +  " with " + replacementDDSPath);

      fs.Seek(tex.loc, SeekOrigin.Begin);
      Console.Write("Replace pre :");
      printpeek(fs, 32);

      long loc_texDataStart = tex.blocks[0].loc_texBlockStart;

      foreach(TexBlock block in tex.blocks){ //overwrite whole source data with 0, this is maybe unneccesary but makes the output easier to diagnose
        fs.Seek(block.loc_texBlockStart, SeekOrigin.Begin);
        fs.Write(new byte[block.compressedSize], 0, block.compressedSize); 
      }

      //TODO? check if dds header is correct
      FileStream fsImg = new FileStream(replacementDDSPath, FileMode.Open);
      //printpeek(fsImg,32);
      fsImg.Seek(0x80, SeekOrigin.Begin);  //strip dds header
      //printpeek(fsImg,32);
      int imgLen = checked((int)fsImg.Length)-0x80;
      long texWriteLoc = loc_texDataStart;
      int nWrittenBytes = 0;

      foreach (TexBlock block in tex.blocks) {
        byte[] readBlock = new byte[block.originalSize];
        fsImg.Read(readBlock, 0, block.originalSize);

        /*
        byte[] readBlock2 = File.ReadAllBytes(replacementDDSPath).Skip(0x80).ToArray();
        if(!Enumerable.SequenceEqual(readBlock, readBlock2)){
          throw new Exception("AAAAAAAAAAAAAAAAA");
        } else {}*/

        //Console.WriteLine(readBlock[readBlock.Length-1] + " pos:" + fsImg.Position.ToString("X8") + " siz:" + readBlock.Length.ToString("X8"));

        byte[] lzo999compressed = lzo.Compress(readBlock);
        int newSize = lzo999compressed.Length;

        if (checkCompression){  
          byte[] recompress = lzo.Decompress(lzo999compressed, readBlock.Length);
          if (Enumerable.SequenceEqual(readBlock, recompress)) {
            Console.WriteLine("Compression works");
          } else{
            throw new Exception("Internal Compression Failure");
          }
        }
        
        fs.Seek(block.loc_compressedSize, SeekOrigin.Begin);
        fs.Write(BitConverter.GetBytes(newSize), 0, 4);

        // originalsize should (probably) not be tampered with. This would only be usefull if there is way to override resolution
        //fs.Seek(block.loc_originalSize, SeekOrigin.Begin); 
        //fs.Write(BitConverter.GetBytes(block.originalSize), 0, 4); 

        fs.Seek(texWriteLoc, SeekOrigin.Begin);
        fs.Write(lzo999compressed, 0, newSize);
        texWriteLoc += newSize;
        nWrittenBytes += newSize;

        /*Console.WriteLine(string.Format("uncompressed = 0x{0}: bytes: {1} -> {2}",
          block.originalSize.ToString("X8"),
          block.compressedSize.ToString("X8"),
          newSize.ToString("X8")
        ));*/
      }
      fsImg.Close();

      if (nWrittenBytes > tex.totalTextureBytes) {
        throw new Exception("replacement texture can not be sufficently compressed. Reducing noise in the texture should fix this, (Output is corrupted)"); 
      }
      fs.Seek(tex.loc_totalTextureBytes, SeekOrigin.Begin); 
      fs.Write(BitConverter.GetBytes(nWrittenBytes), 0, 4);

      fs.Seek(tex.loc, SeekOrigin.Begin);
      Console.Write("Replace post:");
      printpeek(fs, 32);

      Console.WriteLine(string.Format(
        "Replaced tex {0} a' {1}, totalTextureBytes: 0x{2} -> 0x{3}\n", 
        tex.id,
        tex.blocks.Count(),
        tex.totalTextureBytes.ToString("X8"), 
        nWrittenBytes.ToString("X8")
      ));
    }
    
    // ====================================================== //
    private static void Main(string[] args) {   
      Argument rootArgument = new Argument<string>(
        name: "inFile",
        description: "TFC file to work with"
      );
      //rootArgument.Arity = ArgumentArity.ExactlyOne;

      Option dumpOption = new Option<String>(
        aliases: new string[] { "--dump", "-d" },
        description: "Comma-separated number(range)s | * | ./filepath.txt"
      );
      dumpOption.Arity = ArgumentArity.ExactlyOne;

      Option replaceOption = new Option<String>(
        aliases: new string[] { "--replace", "-r" },
        description: "Comma-separated \"id:replacement.dds\" | ./filepath.txt"
      );
      replaceOption.Arity = ArgumentArity.ExactlyOne;

      RootCommand rootCommand = new RootCommand{
        rootArgument, dumpOption, replaceOption
      };
      //rootCommand.Description = "TODO write description";
      rootCommand.SetHandler((string inFile , string dump, string replace) =>{
        Console.WriteLine($"The value for inFile is: {inFile}");
        Console.WriteLine($"The value for --dump is: {dump}");
        Console.WriteLine($"The value for --replace is: {replace}");

        xfile = inFile;
        xdump = dump;
        xreplace = replace;
      }, rootArgument, dumpOption, replaceOption);

      rootCommand.Invoke(args);

      

      Dictionary<int, string> id2replacement = new Dictionary<int, string>(); 

      setDumpRange(xdump);
      addReplacements(id2replacement, xreplace);

      if(xdump == null && id2replacement.Count() == 0){
        throw new ArgumentException("No method supplied");
      }
      makeHappen(xfile, id2replacement);
    }

    private static void addReplacements(Dictionary<int, string> id2replacement, string? str){
      if (str == null || str.Length == 0) return;
      
      foreach(string s in indirectParse(str)){
        string[] sx = s.Split(":", 2);
        if(sx.Length != 2) 
          throw new ArgumentException($"Replacement \"{s}\" can not be parsed");
        int id = int.Parse(sx[0]);
        string path = sx[1].Trim('\"');
        if(!File.Exists(path))
          throw new ArgumentException($"Replacement texture \"{path}\" does not exist.");
        id2replacement.Add(id, path);
      }
    }
    private static void setDumpRange(string? str){
      if (str == null || str.Length == 0) return;

      dumpRange = new HashSet<int>();

      foreach(string s in indirectParse(str)){
        if (s == "*") {
          dumpAll = true;
        } else if (s.Contains("-")){
          string[] strNums = s.Split("-");
          if(strNums.Length != 2) 
            throw new ArgumentException($"Dump range can not be parsed {s}");
          int lowRange = int.Parse(strNums[0]);
          int highRange = int.Parse(strNums[1]);
          foreach (int idx in Enumerable.Range(lowRange, highRange-lowRange+1)){
            dumpRange.Add(idx);
          }
        } else {
          dumpRange.Add(int.Parse(s));
        }
      }
    }
    private static string[] indirectParse(string str){
      if(str.StartsWith("./")){
        string fpath = new string(str.Skip(2).ToArray());
        string[] content = File.ReadAllLines(fpath);
        Console.WriteLine($"Using {fpath} as argument");
        return File.ReadAllLines(fpath);
      } else {
        return str.Split(",");
      }
    }

    private static void makeHappen(string inFile, Dictionary<int,string> id2replacement){
      Console.WriteLine("OPENING FILE");

      TFCInfo tf = getTFCInfo(inFile);
      FileStream fsIn = new FileStream(inFile, FileMode.Open);
      LZOCompressor lzo = new LZOCompressor();
      //tf.texs.RemoveRange(200, tf.texs.Count()-200-1);

      foreach (Tex tex in tf.texs) {        
        if(isInDumpRange(tex.id) == false) continue;
        dumpTex(tex, fsIn, lzo, inFile);
      }
      fsIn.Close();
      
      if(id2replacement.Count() > 0){
        string outFile = "out_tfc/"+Path.GetFileName(inFile);
        File.Copy(inFile, outFile, true);
        FileStream fsOut = new FileStream(outFile, FileMode.Open);
        /*foreach(KeyValuePair<int, string> kv in id2replacement){
          replaceTexture(tf, fsOut, lzo, kv.Key, kv.Value);
          //GC.Collect();
        }*/
        replaceTexture(tf, fsOut, lzo, 121, "example/Test_1024xDXT1.dds");
        replaceTexture(tf, fsOut, lzo, 122, "example/Test_512xDXT1.dds");
        replaceTexture(tf, fsOut, lzo, 123, "example/Test_256xDXT1.dds");
        replaceTexture(tf, fsOut, lzo, 124, "example/Test_128xDXT1.dds");
        //replaceTexture(tf, fsw, lzox, 126, "example/Io_default_specular_512xDXT1.dds");
        fsOut.Close();

        Console.WriteLine("DONE, NO VERIFYING");
      }
      
      /*
      TFCInfo tfVerify = getTFCInfo(outFile);
      FileStream fsVerify = new FileStream(inFile, FileMode.Open);
      foreach (Tex tex in tfVerify.texs) {
        dumpTex(tex, fsVerify, lzo, outFile);
      }
      fsVerify.Close();*/

      Console.WriteLine("DONE");
    }
  }
}

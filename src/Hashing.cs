using CoenM.ImageHash.HashAlgorithms;
using Img = SixLabors.ImageSharp;
using SysImg = System.Drawing;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace PaladinsTfc
{
  class Hashing
  {
    public enum ImgType {PNG, DDS};

    public class HashItem {   
      public string path;
      public ulong hash; 
      public HashItem(string path, ulong hash){
        this.path = path;
        this.hash = hash;
      }
    }  

    public static void hashDir(string dirpath, ImgType imgType){
      string typeName = null;

      if (imgType == ImgType.PNG) typeName = "png";
      else if (imgType == ImgType.DDS) typeName = "dds";
      else throw new Exception ("can only open dds and png files");

      Regex rx = new Regex(@"[^a-zA-Z0-9_\-]",RegexOptions.Compiled | RegexOptions.IgnoreCase);
      string dirpathSafe = "TextureHashes_"+rx.Replace(dirpath, "_");
      string searchPattern = "*."+typeName;
      string outFile = $"out/{dirpathSafe}_{typeName}.json";
      DifferenceHash hasher = new DifferenceHash();
      
      string[] imgPaths = Directory.GetFiles(dirpath, searchPattern, SearchOption.AllDirectories);
      Console.WriteLine($"found {imgPaths.Length} files in: {dirpath} using {imgType} \n writing hashes to: {outFile}");
      int c = 0;
      var hashItems = new ConcurrentBag<HashItem>();
      Parallel.ForEach (imgPaths, path => {
        var img = getImg(path, imgType);
        var relPath = Path.GetRelativePath(dirpath, path);
        ulong hash = hasher.Hash(img);
        hashItems.Add(new HashItem(relPath,hash));

        if (c % 1000 == 0) Console.WriteLine($"hashed {c}/{imgPaths.Length} ..{relPath}..");
        c++;
      });
      Console.WriteLine($"successfully hashed all files");

      string jsData = JsonConvert.SerializeObject(hashItems.ToArray(), Formatting.Indented);
      System.IO.Directory.CreateDirectory(Path.GetDirectoryName(outFile));
      File.WriteAllText(outFile, jsData);
    }
    public static Img.Image<Img.PixelFormats.Rgba32> getImg(string path, ImgType imgType) {
      if (imgType == ImgType.PNG){
        return Img.Image.Load<Img.PixelFormats.Rgba32>(path);
      }
      return null;
    }
  }
}
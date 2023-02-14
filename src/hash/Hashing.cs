using CoenM.ImageHash.HashAlgorithms;
using Img = SixLabors.ImageSharp;
using SysImg = System.Drawing;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Pfim;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

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

    public static ImgType imgTypeFromString(string s) {
      switch (s.ToUpper()) {
        case "PNG":
          return ImgType.PNG;
        case "DDS":
          return ImgType.DDS;
        default:
          throw new ArgumentException($".{s} can't be opened (can only open dds and png files)");
      }
    }

    public static void hashDir(string dirpath, ImgType imgType, string outpath, string filter) {
      string typeName;

      if (imgType == ImgType.PNG) {
        typeName = "png";
      } else if (imgType == ImgType.DDS) {
        typeName = "dds";
      } else throw new ArgumentException("can only open dds and png files");

      Regex rx = new Regex(@"[^a-zA-Z0-9_\-]",RegexOptions.Compiled | RegexOptions.IgnoreCase);
      //string dirpathSafe = "TextureHashes_"+rx.Replace(dirpath, "_");
      string searchPattern = $"{filter}.{typeName}";
      string fileName = rx.Replace($"TextureHashes_{typeName.ToUpper()}_{filter}", "_");
      string outFile = $"{outpath}/hashes/{fileName}.json";
      DifferenceHash hasher = new DifferenceHash();
      
      string[] imgPaths = Directory.GetFiles(dirpath, searchPattern, SearchOption.AllDirectories);
      Console.WriteLine($"found {imgPaths.Length} files in: {dirpath} {imgType} images using {searchPattern} \n writing hashes to: {outFile}");

      if (imgPaths.Length == 0) {
        Console.WriteLine($"Nothing to be hashec, nothing matches {searchPattern}");
        return;
      }

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
      Console.WriteLine($"successfully hashed all files to {outFile}");

      string jsData = JsonConvert.SerializeObject(hashItems.ToArray(), Formatting.Indented);
      System.IO.Directory.CreateDirectory(Path.GetDirectoryName(outFile));
      File.WriteAllText(outFile, jsData);
    }
    public static Img.Image<Img.PixelFormats.Rgba32>? getImg(string path, ImgType imgType) {
      if (imgType == ImgType.PNG){
        return Img.Image.Load<Img.PixelFormats.Rgba32>(path);
      } else if (imgType == ImgType.DDS) {
        MemoryStream pngStream = new MemoryStream();
        using (var image = Pfimage.FromFile(path)) {
          var handle = GCHandle.Alloc(image.Data, GCHandleType.Pinned); 
          try {
            var data = Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0);
            var bitmap = new Bitmap(image.Width, image.Height, image.Stride, PixelFormat.Format32bppArgb, data);
            bitmap.Save(pngStream, System.Drawing.Imaging.ImageFormat.Png);
          } finally {
            handle.Free();
          }
          var img = Img.Image.Load<Img.PixelFormats.Rgba32>(pngStream);
          pngStream.Close();
          return img;
        }
      }
      return null;
    }
  }
}
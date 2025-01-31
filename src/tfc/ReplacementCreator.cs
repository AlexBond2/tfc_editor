using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace PaladinsTfc {
  class ReplacementCreator {
    public enum Encoding { DXT1, DXT5, VUV8 };
    MagickImage img;
    public ReplacementCreator(string filePath) {
      img = new MagickImage(filePath);
      img.Format = MagickFormat.Png8;
      img.Transparent(MagickColor.FromRgb(0, 0, 0));
    }

    public static MagickFormat asMagicEncoding(Encoding en) {
      switch (en) {
        case Encoding.DXT1:
          return MagickFormat.Dxt1;
        case Encoding.DXT5:
          return MagickFormat.Dxt5;
        default:
          throw new ArgumentException($"Format \"{en}\" can not be understood");
      }
    }

    public Stream Serialize(int width, Encoding encoding) {
      var magicEnc = asMagicEncoding(encoding);
      using (MagickImage im = new MagickImage(img)) { 
        int newW = width;
        int newH = ((int)im.Height * newW) / (int)im.Width;
        im.Resize((uint)newW, (uint)newH);
        im.Format = magicEnc;
        im.Settings.SetDefine(magicEnc, "mipmaps", "0");
        im.Write("tempshit.dds");
      }
      return File.OpenRead("tempshit.dds");
    }
  }
}

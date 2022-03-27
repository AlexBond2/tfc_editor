using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Lzo64 {
  public class LZOCompressor {
    private const string LzoDll32Bit = "lzo2.dll";
    private const string LzoDll64Bit = "lzo2_64.dll";
    private static TraceSwitch _traceSwitch = new TraceSwitch("Simplicit.Net.Lzo", "Switch for tracing of the LZOCompressor-Class");
    private byte[] _workMemory = new byte[0x20000];
    private bool _calculated;
    private bool _is64bit;

    [DllImport("lzo2_64.dll")]
    private static extern int __lzo_init_v2(uint v,int s1,int s2,int s3,int s4,int s5,int s6,int s7,int s8,int s9);

    [DllImport("lzo2_64.dll")]
    private static extern IntPtr lzo_version_string();

    [DllImport("lzo2_64.dll")]
    private static extern string lzo_version_date();

    [DllImport("lzo2_64.dll")]
    private static extern int lzo1x_1_compress(byte[] src,int src_len, byte[] dst,ref int dst_len,byte[] wrkmem);

    [DllImport("lzo2_64.dll")]
    private static extern int lzo1x_999_compress(byte[] src, int src_len, byte[] dst, ref int dst_len, byte[] wrkmem);

    [DllImport("lzo2_64.dll")]
    private static extern int lzo1x_decompress(byte[] src,int src_len,byte[] dst,ref int dst_len,byte[] wrkmem);

    [DllImport("lzo2.dll", EntryPoint = "lzo_version_string")]
    private static extern IntPtr lzo_version_string32();

    [DllImport("lzo2.dll", EntryPoint = "lzo1x_1_compress", CallingConvention = CallingConvention.Cdecl)]
    private static extern int lzo1x_999_compress32(byte[] src,int src_len, byte[] dst,ref int dst_len,byte[] wrkmem);

    [DllImport("lzo2.dll", EntryPoint = "lzo1x_decompress", CallingConvention = CallingConvention.Cdecl)]
    private static extern int lzo1x_decompress32(byte[] src,int src_len,byte[] dst,ref int dst_len,byte[] wrkmem);

    [DllImport("lzo2.dll", EntryPoint = "__lzo_init_v2", CallingConvention = CallingConvention.Cdecl)]
    private static extern int __lzo_init_v2_32(uint v,int s1,int s2,int s3,int s4,int s5,int s6,int s7,int s8,int s9);

    public LZOCompressor(){
      if ((!this.Is64Bit() ? LZOCompressor.__lzo_init_v2_32(1U, -1, -1, -1, -1, -1, -1, -1, -1, -1) : LZOCompressor.__lzo_init_v2(1U, -1, -1, -1, -1, -1, -1, -1, -1, -1)) != 0)
        throw new Exception("Initialization of LZO-Compressor failed !");
    }

    public string Version => Marshal.PtrToStringAnsi(!this.Is64Bit() ? LZOCompressor.lzo_version_string32() : LZOCompressor.lzo_version_string());

    public string VersionDate => LZOCompressor.lzo_version_date();

    private bool Is64Bit(){
      if (!this._calculated){
        this._is64bit = IntPtr.Size == 8;
        this._calculated = true;
      }
      return this._is64bit;
    }

    public byte[] Compress(byte[] src){
      byte[] buf = new byte[src.Length + src.Length / 16 + 64 + 3];
      byte[] destinationArray;
      //byte[] buffer = new byte[src.Length + src.Length / 64 + 16 + 3 + 4];
      int dst_len = 0;
      if (this.Is64Bit()) {
        LZOCompressor.lzo1x_999_compress(src, src.Length, buf, ref dst_len, this._workMemory);
      }
      else{
        LZOCompressor.lzo1x_999_compress32(src, src.Length, buf, ref dst_len, this._workMemory);
      }
      destinationArray = new byte[dst_len];
      /*Console.WriteLine("dstlen:"+destinationArray.Length.ToString("X8"));
      Console.WriteLine("buf[0]:"+buf[0]);
      Console.WriteLine("buf:"+ (buf == null));
      Console.WriteLine("buf:" + buf);*/
      Array.Copy(buf, 0, destinationArray, 0, dst_len);
      return destinationArray;
    }

    public byte[] Decompress(byte[] src, int origlen){
      byte[] dst = new byte[origlen];
      int dst_len = origlen;
      if (this.Is64Bit())
        LZOCompressor.lzo1x_decompress(src, src.Length, dst, ref dst_len, this._workMemory);
      else
        LZOCompressor.lzo1x_decompress32(src, src.Length, dst, ref dst_len, this._workMemory);
      return dst;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Lzo64.LZOCompressor
// Assembly: DC_Universe_tfc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D4DEAE37-F391-4295-B0F5-7929F2EB741E
// Assembly location: D:\model\_Stealer\_paladins\_program\paladins_tfc.exe

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Lzo64
{
  public class LZOCompressor
  {
    //https://wallaceturner.azurewebsites.net/lzo-for-c
    private const string LzoDll32Bit = "lzo2.dll";
    private const string LzoDll64Bit = "lzo2_64.dll";
    private static TraceSwitch _traceSwitch = new TraceSwitch("Simplicit.Net.Lzo", "Switch for tracing of the LZOCompressor-Class");
    private byte[] _workMemory = new byte[0x20000];
    private bool _calculated;
    private bool _is64bit;

    [DllImport("lzo2_64.dll")]
    private static extern int __lzo_init_v2(
      uint v,
      int s1,
      int s2,
      int s3,
      int s4,
      int s5,
      int s6,
      int s7,
      int s8,
      int s9);

    [DllImport("lzo2_64.dll")]
    private static extern IntPtr lzo_version_string();

    [DllImport("lzo2_64.dll")]
    private static extern string lzo_version_date();

    [DllImport("lzo2_64.dll")]
    private static extern int lzo1x_1_compress(byte[] src,int src_len,byte[] dst,ref int dst_len,byte[] wrkmem);

    [DllImport("lzo2_64.dll")]
    private static extern int lzo1x_999_compress(byte[] src,int src_len,byte[] dst,ref int dst_len,byte[] wrkmem);

    [DllImport("lzo2_64.dll")]
    private static extern int lzo1x_decompress(byte[] src,int src_len,byte[] dst,ref int dst_len,byte[] wrkmem);

    [DllImport("lzo2.dll", EntryPoint = "lzo_version_string")]
    private static extern IntPtr lzo_version_string32();

    [DllImport("lzo2.dll", EntryPoint = "lzo1x_1_compress", CallingConvention = CallingConvention.Cdecl)]
    private static extern int lzo1x_1_compress32(byte[] src,int src_len,byte[] dst,ref int dst_len,byte[] wrkmem);

    [DllImport("lzo2.dll", EntryPoint = "lzo1x_decompress", CallingConvention = CallingConvention.Cdecl)]
    private static extern int lzo1x_decompress32(byte[] src,int src_len,byte[] dst,ref int dst_len,byte[] wrkmem);

    [DllImport("lzo2.dll", EntryPoint = "__lzo_init_v2", CallingConvention = CallingConvention.Cdecl)]
    private static extern int __lzo_init_v2_32(uint v,int s1,int s2,int s3,int s4,int s5,int s6,int s7,int s8,int s9);

    public LZOCompressor() {
      if ((!this.Is64Bit() ? LZOCompressor.__lzo_init_v2_32(1U, -1, -1, -1, -1, -1, -1, -1, -1, -1) : LZOCompressor.__lzo_init_v2(1U, -1, -1, -1, -1, -1, -1, -1, -1, -1)) != 0)
        throw new Exception("Initialization of LZO-Compressor failed !");
    }

    public string Version => Marshal.PtrToStringAnsi(!this.Is64Bit() ? LZOCompressor.lzo_version_string32() : LZOCompressor.lzo_version_string());

    public string VersionDate => LZOCompressor.lzo_version_date();

    public byte[] Compress(byte[] src) {
      if (LZOCompressor._traceSwitch.TraceVerbose)
        Trace.WriteLine(string.Format("LZOCompressor: trying to compress {0}", (object) src.Length));
      byte[] buffer = new byte[src.Length + src.Length / 16 + 64 + 3];
      //byte[] buffer = new byte[src.Length + src.Length / 64 + 16 + 3 + 4];
      int dst_len = 0;
      if (this.Is64Bit()){
        LZOCompressor.lzo1x_999_compress(src, src.Length, buffer, ref dst_len, this._workMemory);
      } else {
        LZOCompressor.lzo1x_1_compress32(src, src.Length, buffer, ref dst_len, this._workMemory);
      }
      if (LZOCompressor._traceSwitch.TraceVerbose)
        Trace.WriteLine(string.Format("LZOCompressor: compressed {0} to {1} bytes", (object) src.Length, (object) dst_len));
        
      byte[] destinationArray = new byte[dst_len];
      for (int i = 0; i < dst_len; i++){
        destinationArray[i] = buffer[i];
      }
      //Array.Copy(buffer, destinationArray, dst_len);
      return destinationArray;
    }

    private bool Is64Bit() {
      if (!this._calculated) {
        this._is64bit = IntPtr.Size == 8;
        this._calculated = true;
      }
      return this._is64bit;
    }

    public byte[] Decompress(byte[] src, int origlen) {
      if (LZOCompressor._traceSwitch.TraceVerbose)
        Trace.WriteLine(string.Format("LZOCompressor: trying to decompress {0}", src.Length));
      byte[] dst = new byte[origlen];
      int dst_len = origlen;
      if (this.Is64Bit())
        LZOCompressor.lzo1x_decompress(src, src.Length, dst, ref dst_len, this._workMemory);
      else
        LZOCompressor.lzo1x_decompress32(src, src.Length, dst, ref dst_len, this._workMemory);
      if (LZOCompressor._traceSwitch.TraceVerbose)
        Trace.WriteLine(string.Format("LZOCompressor: decompressed {0} to {1} bytes", (object) src.Length, (object) origlen));
      return dst;
    }
  }
}

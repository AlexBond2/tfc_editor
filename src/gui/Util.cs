using System.ComponentModel;
using System.Data;

namespace PaladinsTfc
{
  static class Util {
    public static void dumpObject(object obj) {
      Console.WriteLine("[{0}] {{", obj);
      foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj)) {
        string name = descriptor.Name;
        object value = descriptor.GetValue(obj);
        Console.WriteLine("  {0}={1}", name, value);
      }
      Console.WriteLine("}");
    }
  }
}
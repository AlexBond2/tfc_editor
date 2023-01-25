using System.ComponentModel;
using System.Data;

namespace PaladinsTfcExtend
{
  public static class Extentions {
    public static string Truncate(this string value, int maxChars) {
      return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
    }
    public static void cursorToRight(this TextBox textBox) {
      textBox.SelectionStart = textBox.Text.Length;
      textBox.SelectionLength = 0;
    }
    public static void dumpObject(object obj){
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
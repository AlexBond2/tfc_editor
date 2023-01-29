using System.ComponentModel;
using System.Data;
using Newtonsoft.Json;

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
    public static void invokeSomeway(Control control, MethodInvoker mi) {
      if (control.InvokeRequired) {
        control.BeginInvoke(mi);
      } else {
        mi.Invoke();
      }
    }
  }
  public class PersitantData {
    private const string configurationPath = "configuration.json";
    public string inputDirectory { get; set; }
    public string outputDirectory { get; set; }
    public string hashesTFC { get; set; }
    public string hashesCooked { get; set; }
    public string cookedReferenceDirectory { get; set; }
    private PersitantData() {
    }
    public static PersitantData load() {
      string fileContent;
      try {
        fileContent = File.ReadAllText(configurationPath);
      } catch (Exception e) {
        return new PersitantData();        
      }
      return JsonConvert.DeserializeObject<PersitantData>(fileContent);
    }
    public void write() {
      string jsData = JsonConvert.SerializeObject(this, Formatting.Indented);
      File.WriteAllText(configurationPath, jsData);
    }
  }
}
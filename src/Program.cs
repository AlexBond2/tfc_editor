using System.CommandLine;
using System.Windows.Forms;
using System.Drawing;

namespace PaladinsTfc
{
  class Program
  {
    // ====================================================== //
    [STAThread]
    private static void Main(string[] args) {   
      if (args.Length == 0)
        gui();
      else 
        commandline(args);
    }

    private static void gui(){
      //Application.EnableVisualStyles();
      Application.Run(new Gui());
    }
    private static void commandline(string[] args){
      RootCommand rootCommand = new RootCommand();

      Command openCommand = getOpenCommand();
      Command hashCommand = getHashCommand();

      rootCommand.AddCommand(openCommand);
      rootCommand.AddCommand(hashCommand);
      rootCommand.Invoke(args);      
    }

    private static Command getHashCommand(){
      Command hashCommand = new Command ("hash", "command for hashing images");

      Argument folderArg = new Argument<string>(
        name: "in directory",
        description: "files to work with"
      ); 

      Command hashUmodelCommand = new Command ("umodel", "hash umodel exported images"){folderArg};
      hashUmodelCommand.SetHandler((string inFolder) => {
        Hashing.hashDir(inFolder, Hashing.ImgType.PNG);
      }, folderArg);

      Command hashExportedCommand = new Command ("exported", "hash exported dds images"){folderArg};
      hashExportedCommand.SetHandler((string inFolder) => {
        Hashing.hashDir(inFolder, Hashing.ImgType.DDS);
      }, folderArg);

      hashCommand.AddCommand(hashExportedCommand);
      hashCommand.AddCommand(hashUmodelCommand);

      return hashCommand;
    }

    private static Command getOpenCommand() {      
      Argument openArgument = new Argument<string>(
        name: "in file",
        description: "TFC file to work with"
      ); //rootArgument.Arity = ArgumentArity.ExactlyOne;

      Option dumpOption = new Option<String>(
        aliases: new string[] { "--dump", "-d" },
        description: "dumps the selected ids to dds files\nComma-separated number(range)s | * | ./filepath.txt"
      ); dumpOption.Arity = ArgumentArity.ExactlyOne;

      Option dumpHashOption = new Option<bool>(
        aliases: new string[] { "--no-image", "-x" },
        description: "Flag: do not write textures to disk, only the hash json"
      ); dumpOption.Arity = ArgumentArity.ExactlyOne;

      Option replaceOption = new Option<String>(
        aliases: new string[] { "--replace", "-r" },
        description: "Comma-separated \"id:replacement.dds\" | ./filepath.txt"
      ); replaceOption.Arity = ArgumentArity.ExactlyOne;

      Command openCommand = new Command("open", "for tfc file operations") {
        openArgument, dumpOption, replaceOption
      }; openCommand.Description = "TODO write description";
      
      var rootCommand = new RootCommand();

      openCommand.SetHandler((string inTfcFile , string dumpRangeStr, string replaceStr, bool onlyDumpHash) => {
        Console.WriteLine($"The value for inFile is: {inTfcFile}");
        Console.WriteLine($"The value for --dump is: {dumpRangeStr}");
        Console.WriteLine($"The value for --replace is: {replaceStr}");
        Console.WriteLine($"The value for --only-dump-hash is: {onlyDumpHash}");
        
        if (inTfcFile == null | (dumpRangeStr == null & replaceStr == null)) throw new ArgumentException("No operation supplied");
        Dictionary<int, string> id2replacement = getReplacements(replaceStr);
        HashSet<int> dumpRange = getDumpRange(dumpRangeStr);
        TexHandling.run(inTfcFile, id2replacement, dumpRange);
      }, openArgument, dumpOption, replaceOption, dumpHashOption);
      return openCommand;
    }

    private static Dictionary<int, string> getReplacements(string? str){
      if (str == null || str.Length == 0) return null;
      
      Dictionary<int, string> id2replacement = new Dictionary<int, string>();
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
      return id2replacement;
    }

    private static HashSet<int> getDumpRange(string? str){
      if (str == null || str.Length == 0) return new HashSet<int>();

      HashSet<int> dumpRange = new HashSet<int>();

      foreach(string s in indirectParse(str)){
        if (s == "*") {
          dumpRange = null;
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

      return dumpRange;
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
  }
}

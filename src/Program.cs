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
    public static void commandline(string[] args){
      RootCommand rootCommand = new RootCommand();

      Command openCommand = getOpenCommand();
      Command hashCommand = getHashCommand();

      rootCommand.AddCommand(openCommand);
      rootCommand.AddCommand(hashCommand);
      rootCommand.Invoke(args);      
    }

    private static Command getHashCommand(){
      Command hashCommand = new Command ("hash", "Command for hashing directories of images");

      Argument folderArg = new Argument<string>(
        name: "in directory",
        description: "path/to/directory containing files that should be hashed"
      );

      Option outDirectoryOption = new Option<string>(
        aliases: new string[] { "--output-directory", "-o" },
        description: "Directory to place hash jsons.\n <path/to/folder>"
      ); outDirectoryOption.Arity = ArgumentArity.ExactlyOne;
      outDirectoryOption.SetDefaultValue("./out");

      Option filterOption = new Option<string>(
        aliases: new string[] { "--filter", "-f" },
        description: "Ignore all tfc that doesn't contain filter.\n <string>"
      ); filterOption.Arity = ArgumentArity.ExactlyOne;
      filterOption.SetDefaultValue("*");

      Command hashUmodelCommand = new Command ("cooked", "generate hash file for umodel exported (png) images"){
        folderArg, outDirectoryOption, filterOption};
      hashUmodelCommand.SetHandler((string inFolder, string outDir, string filter) => {
        Hashing.hashDir(inFolder, Hashing.ImgType.PNG, outDir, filter);
      }, folderArg, outDirectoryOption, filterOption);

      Command hashExportedCommand = new Command ("tfc", "generate hash file for dumped dds images") {
        folderArg, outDirectoryOption, filterOption};
      hashExportedCommand.SetHandler((string inFolder, string outDir, string filter) => {
        Hashing.hashDir(inFolder, Hashing.ImgType.DDS, outDir, filter);
      }, folderArg, outDirectoryOption, filterOption);

      hashCommand.AddCommand(hashExportedCommand);
      hashCommand.AddCommand(hashUmodelCommand);

      return hashCommand;
    }

    private static Command getOpenCommand() {      
      Argument openArgument = new Argument<string>(
        name: "in file",
        description: "TFC file to work with <path/to/file.tfc>"
      ); //rootArgument.Arity = ArgumentArity.ExactlyOne;

      Option dumpOption = new Option<string>(
        aliases: new string[] { "--dump", "-d" },
        description: "Dumps the selected ids to dds files\n [(<id> | <id>-<id> | * )] | ./<args.txt>"
      ); dumpOption.Arity = ArgumentArity.ExactlyOne;

      Option replaceOption = new Option<string>(
        aliases: new string[] { "--replace", "-r" },
        description: "Creates a copy of the tfc file but with textures replaced depending on id\n [(<id>:<path/to/replacement.dds>\")] | ./<args.txt>"
      ); replaceOption.Arity = ArgumentArity.ExactlyOne;

      Option outDirectoryOption = new Option<string>(
        aliases: new string[] { "--output-directory", "-o" },
        description: "Directory to place dumps and/or tfc files.\n <path/to/folder>"
      ); outDirectoryOption.Arity = ArgumentArity.ExactlyOne;
      outDirectoryOption.SetDefaultValue("./out"); 

      Option compressionModeOption = new Option<string>(
        aliases: new string[] { "--compression-mode", "-m" },
        description: "Compression mode\n tfc | zlib"
      ); compressionModeOption.Arity = ArgumentArity.ExactlyOne;
      compressionModeOption.SetDefaultValue("tfc");

      Command openCommand = new Command("open", "Parses tfc file and then does [options]") {
        openArgument, dumpOption, replaceOption, outDirectoryOption, compressionModeOption
      }; 
      
      var rootCommand = new RootCommand();

      openCommand.SetHandler((string inTfcFile , string dumpRangeStr, string replaceStr, string directoryStr, string compressionMode) => {
        Console.WriteLine($"The value for inFile is: {inTfcFile}");
        Console.WriteLine($"The value for --dump is: {dumpRangeStr}");
        Console.WriteLine($"The value for --replace is: {replaceStr}");
        Console.WriteLine($"The value for --output-directory is: {directoryStr}");
        Console.WriteLine($"The value for --compression-mode is: {compressionMode}");

        if (inTfcFile == null) {
          throw new ArgumentException("No file supplied");
        }
        if (dumpRangeStr == null & replaceStr == null) {
          throw new ArgumentException("Neither dump or replace operation supplied");
        }
        Dictionary<int, string> id2replacement = getReplacements(replaceStr);
        HashSet<int> dumpRange = getDumpRange(dumpRangeStr);
        TexHandling.run(inTfcFile, directoryStr, id2replacement, dumpRange, compressionMode);
      }, openArgument, dumpOption, replaceOption, outDirectoryOption, compressionModeOption);
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

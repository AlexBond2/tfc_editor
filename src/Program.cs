﻿using System.CommandLine;
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
      Command hashCommand = new Command ("hash", "Command for hashing directories of images");

      Argument folderArg = new Argument<string>(
        name: "in directory",
        description: "path/to/directory containing files that should be hashed"
      );

      Command hashUmodelCommand = new Command ("cooked", "generate hash file for umodel exported (png) images"){folderArg};
      hashUmodelCommand.SetHandler((string inFolder) => {
        Hashing.hashDir(inFolder, Hashing.ImgType.PNG);
      }, folderArg);

      Command hashExportedCommand = new Command ("tfc", "generate hash file for dumped dds images") {folderArg};
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

      Option directoryOption = new Option<string>(
        aliases: new string[] { "--output-directory", "-o" },
        description: "Directory to place dumps and/or tfc files.\n <path/to/folder>"
      ); directoryOption.Arity = ArgumentArity.ExactlyOne;
      directoryOption.SetDefaultValue("./out");

      Command openCommand = new Command("open", "Parses tfc file and then does [options]") {
        openArgument, dumpOption, replaceOption, directoryOption
      }; 
      
      var rootCommand = new RootCommand();

      openCommand.SetHandler((string inTfcFile , string dumpRangeStr, string replaceStr, string directoryStr) => {
        Console.WriteLine($"The value for inFile is: {inTfcFile}");
        Console.WriteLine($"The value for --dump is: {dumpRangeStr}");
        Console.WriteLine($"The value for --replace is: {replaceStr}");
        Console.WriteLine($"The value for --output-directory is: {directoryStr}");

        if (inTfcFile == null) {
          throw new ArgumentException("No file supplied");
        }
        if (dumpRangeStr == null & replaceStr == null) {
          throw new ArgumentException("Neither dump or replace operation supplied");
        }
        Dictionary<int, string> id2replacement = getReplacements(replaceStr);
        HashSet<int> dumpRange = getDumpRange(dumpRangeStr);
        TexHandling.run(inTfcFile, id2replacement, dumpRange);
      }, openArgument, dumpOption, replaceOption, directoryOption);
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

using Lzo64;
using System.CommandLine;

namespace PaladinsTfc
{
  class Program
  {
    private static string xfile;
    private static string xdump;
    private static string xreplace;

    // ====================================================== //
    private static void Main(string[] args) {   
      Argument rootArgument = new Argument<string>(
        name: "inFile",
        description: "TFC file to work with"
      );
      //rootArgument.Arity = ArgumentArity.ExactlyOne;

      Option dumpOption = new Option<String>(
        aliases: new string[] { "--dump", "-d" },
        description: "Comma-separated number(range)s | * | ./filepath.txt"
      );
      dumpOption.Arity = ArgumentArity.ExactlyOne;

      Option replaceOption = new Option<String>(
        aliases: new string[] { "--replace", "-r" },
        description: "Comma-separated \"id:replacement.dds\" | ./filepath.txt"
      );
      replaceOption.Arity = ArgumentArity.ExactlyOne;

      RootCommand rootCommand = new RootCommand{
        rootArgument, dumpOption, replaceOption
      };
      //rootCommand.Description = "TODO write description";
      rootCommand.SetHandler((string inFile , string dump, string replace) =>{
        //Console.WriteLine($"The value for inFile is: {inFile}");
        //Console.WriteLine($"The value for --dump is: {dump}");
        //Console.WriteLine($"The value for --replace is: {replace}");
        xfile = inFile;
        xdump = dump;
        xreplace = replace;
      }, rootArgument, dumpOption, replaceOption);

      rootCommand.Invoke(args);
      Dictionary<int, string> id2replacement = getReplacements(xreplace);
      HashSet<int> dumpRange = getDumpRange(xdump);
      
      if(xdump == null && id2replacement.Count() == 0){
        throw new ArgumentException("No method supplied");
      }
      TexHandling.run(xfile, id2replacement, dumpRange);
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
      if (str == null || str.Length == 0) return null;

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

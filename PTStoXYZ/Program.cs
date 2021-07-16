using CommandLineParser.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTStoXYZ
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var parser = new CommandLineParser.CommandLineParser();
            var settings = new ProgramCommandLineSettings();
            parser.ExtractArgumentAttributes(settings);

            try
            {
                parser.ParseCommandLine(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Command line arguments failed to parse: {ex.Message}");
            }
            try
            {
                if (!parser.ParsingSucceeded)
                {
                    parser.ShowUsage();
                    Console.ReadKey();
                    return;
                }

                if (settings.Help)
                { parser.ShowUsage(); return; }

                if (settings.ShowArguments)
                    parser.ShowParsedArguments();

                var appModel = new AppModel(settings.InputFile, settings.OutputFile, settings.Debug, settings.OpenFile);
                appModel.ConvertFile();

                Console.WriteLine("Converting done.");
            }
            catch (Exception ex)
            {
                if (parser.ParsingSucceeded)
                    Console.WriteLine($"Oops![{settings.InputFile}]\r\n ==> \"{ex.Message}\"\r\n");
                else
                    Console.WriteLine($"Oops!\r\n ==> \"{ex.Message}\"\r\n");
            }
        }

        internal class ProgramCommandLineSettings
        {
            // https://github.com/j-maly/CommandLineParser

            #region Public Properties

            [SwitchArgument('d', "debug", false, Description = "Set whether to show debug information or not. When omitted equals FALSE.")]
            public bool Debug { get; set; }

            [SwitchArgument('a', "arguments", false, Description = "Set whether to show debug information about arguments parsed on command-line. When omitted equals FALSE.")]
            public bool ShowArguments { get; set; }

            [SwitchArgument('h', "help", false, Description = "See command-line arguments help. When omitted equals FALSE.")]
            public bool Help { get; set; }

            [SwitchArgument('e', "excel", false, Description = "When this is set, after conversion the file will be opened with the default program associated with it.")]
            public bool OpenFile { get; set; }

            [ValueArgument(typeof(string), 'i', "input", Description = "Set the path to the input file you would like to use.", Optional = false)]
            public string InputFile { get; set; }

            [ValueArgument(typeof(string), 'o', "output", Description = "Set the path to the output file.", Optional = false)]
            public string OutputFile { get; set; }

            #endregion Public Properties
        }
    }
}
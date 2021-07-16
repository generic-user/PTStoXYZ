using System;
using System.IO;
using System.Linq;

namespace PTStoXYZ
{
    internal class AppModel
    {
        private readonly string inputFile;
        private readonly string outputFile;
        private readonly bool debug;
        private readonly bool openFile;

        public AppModel(string inputFile, string outputFile, bool debug, bool openFile)
        {
            this.inputFile = inputFile;
            this.outputFile = outputFile;
            this.debug = debug;
            this.openFile = openFile;
        }

        internal void ConvertFile()
        {
            // Skip first line
            using (var reader = new StreamReader(inputFile))
            {
                bool skipFirst = true;
                using (var writer = new StreamWriter(outputFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!skipFirst)

                            writer.WriteLine(ConvertLine(line));
                        else
                            skipFirst = false;
                    }
                }
            }
        }

        private string ConvertLine(string line)
        {
            var array = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (array.Count() != 7)
                throw new InvalidDataException("The format of the data was not as expected.");
            return $"{array[0]} {array[1]} {array[2]}";
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Logger.File.Internal
{
    public class FileWriter : IFileWriter
    {
        private int fileNumber = 1;

        private const string FileName = "log.txt";

        private int rollingFileSize = 5 * 1024; // 5Kb

        private readonly List<string> lines = new List<string>();

        public void Dispose()
        {
            this.Flush();
        }

        public void Flush()
        {
            CheckSize();
            System.IO.File.AppendAllLines(FileName, lines);
            lines.Clear();
        }

        private void CheckSize()
        {
            FileInfo info = new FileInfo(FileName);
            if(info.Exists)
            {
                if(info.Length > rollingFileSize)
                {
                    
                    info.MoveTo($"log.{fileNumber}.txt");

                    fileNumber++;

                }
            }
        }

        private string GetFileName()
        {
            return $"log.{fileNumber}.txt";
        }


        public void WriteLine(string message)
        {
            lines.Add(message);
        }
    }
}

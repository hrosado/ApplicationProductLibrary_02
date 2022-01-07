using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;

namespace Apl_Console_App
{
    class Program
    {
        [Serializable]
        private class FileItems
        {
            public int Id { get; set; }
            public string FileName { get; set; }
            public string FileSize { get; set; }
            public DateTime TimeStamp { get; set; }


        }
        static void Main(string[] args)
        {
            // rootPath directory to index
            // Homeroot
            string rootPath = @"C:\Users\CW2_Rosado\Documents\Repos\OEWIO2021\Content\OEWIO_PDFs\TestDir";
            string writeToPath = @"C:\Users\CW2_Rosado\Documents\Repos\ApplicationProductLibrary_02\Apl_Console_App\Data\files.csv";
            // Remoteroot
            //string rootPath = @"\\hqcuilms.area52.afnoapps.usaf.mil\E\DLL_Reengineering\Dependencies_x64_Release\";

            bool directoryExists = Directory.Exists(rootPath);
            bool fileExist = File.Exists(writeToPath);

            if (directoryExists)
            {
                if (fileExist)
                {
                    File.Delete(writeToPath);
                }
                Console.WriteLine("The directory exists.");
                string[] files = Directory.GetFiles(rootPath, "*.*", SearchOption.TopDirectoryOnly);
                List<string> fileItems = new List<string>();
                foreach (var file in files)
                {
                    var info = new FileInfo(file);
                    fileItems.Add(($"{ Path.GetFileName(file) },{ info.LastWriteTime },{info.Length } bytes"));
                }

                var list = new List<FileItems>();
                int count = 0;
                File.AppendAllText(writeToPath, $"{ "Id"},{  "FileName" },{ "TimeStamp"},{ "FileSize" } " + Environment.NewLine);
                foreach (var line in fileItems)
                {
                    string[] entries = line.Split(',');
                    FileItems newFileItem = new FileItems();

                    count++;

                    newFileItem.Id = count;
                    newFileItem.FileName = entries[0];
                    newFileItem.TimeStamp = Convert.ToDateTime(entries[1]);
                    newFileItem.FileSize = entries[2];

                    list.Add(newFileItem);

                    File.AppendAllText(writeToPath, $"{ newFileItem.Id},{  newFileItem.FileName },{ newFileItem.TimeStamp },{ newFileItem.FileSize } " + Environment.NewLine);

                }
                foreach (var li in list)
                {
                    Console.WriteLine($"{ li.Id},{  li.FileName },{ li.TimeStamp },{ li.FileSize } ");
                }
                //string strResultJson = JsonConvert.SerializeObject(list, Formatting.Indented);
                // Location to write JSON to
                //File.WriteAllText(@"C:\Users\CW2_Rosado\Documents\Repos\ApplicationProductLibrary_02\Apl_Console_App\Data\files.csv", list);

            }
            else
            {
                // Provide warning
                // Remote folder not accesible and dir cannot be updated.
                Console.WriteLine("The directory DOES NOT exist.");
            }

        }

    }
}

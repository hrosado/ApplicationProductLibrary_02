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
            //string rootPath = @"C:\Users\CW2_Rosado\Documents\Repos\OEWIO2021\Content\OEWIO_PDFs\TestDir";

            // Remoteroot
            //string rootPath = @"\\hqcuilms\E\202111_LMS_Contents_Folder_Backup\date_only_certificates";
            // string rootPath = @"\\hqcuilms.area52.afnoapps.usaf.mil\E\DLL_Reengineering\Dependencies_x64_Release\";
            //string rootPath = @"\\hqcuilms.area52.afnoapps.usaf.mil\E\202111_LMS_Contents_Folder_Backup\Test_db\";

            // string rootPath = @"C:\Users\CW2_Rosado\Documents\Repos\OEWIO2021\Content\OEWIO_PDFs\TestDir";

            // Remoteroot
            string rootPath = @"\\hqcuilms.area52.afnoapps.usaf.mil\E\DLL_Reengineering\Dependencies_x64_Release\";


            bool directoryExists = Directory.Exists(rootPath);
            int readCount = 0;
            if (directoryExists)
            {
                int ReadCount = 0;
                Console.WriteLine("The directory exists.");
                string[] files = Directory.GetFiles(rootPath, "*.*", SearchOption.TopDirectoryOnly);
                List<string> fileItems = new List<string>();
                foreach (var file in files)
                {
                    readCount++;
                    Console.WriteLine("Reading files ..." + readCount);
                    var info = new FileInfo(file);
                    fileItems.Add(($"{ Path.GetFileName(file) },{ info.LastWriteTime },{info.Length } bytes"));
                    ReadCount++;

                }

                var list = new List<FileItems>();
                int writeCount = 0;
                foreach (var line in fileItems)
                {
                    Console.WriteLine("Writing files ... " + writeCount);

                    string[] entries = line.Split(',');
                    FileItems newFileItem = new FileItems();

                    writeCount++;

                    Console.WriteLine("Writing files ..." + writeCount);

                    newFileItem.Id = writeCount;
                    newFileItem.FileName = entries[0];
                    newFileItem.TimeStamp = Convert.ToDateTime(entries[1]);
                    newFileItem.FileSize = entries[2];

                    list.Add(newFileItem);

                }
                foreach (var li in list)
                {
                    Console.WriteLine($"{ li.Id},{  li.FileName },{ li.TimeStamp },{ li.FileSize } ");
                }
                string strResultJson = JsonConvert.SerializeObject(list, Formatting.Indented);
                // Location to write JSON to

                //File.WriteAllText(@"C:\Users\CW2_Rosado\Documents\Repos\ApplicationProductLibrary_02\Apl_Console_App\Data\files.json", strResultJson);

                // File.WriteAllText(@"C:\Users\CW2_Rosado\Documents\Repos\ApplicationProductLibrary_02\Apl_Console_App\Data\files.json", strResultJson);
                // Work to write JSON to


                //File.WriteAllText(@"C:\Users\CW2_Rosado\Documents\Repos\ApplicationProductLibrary_02\Apl_Console_App\Data\files.json", strResultJson);
                // work

                File.WriteAllText(@"C:\Users\1260021520E\Documents\09_APL\ApplicationProductLibrary_02\Apl_Console_App\Data\files.json", strResultJson);

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

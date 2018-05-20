using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.bin.Utilities
{
    class FileSystemInteraction
    {
        public static void deleteFile (String filePath)
        {
            if (System.IO.File.Exists(filePath)) // Delete the file if it exists
            {
                File.Delete(filePath);
            }
        }

        public static void writeToTextFile (String filePath, String[] lines)
        {
            deleteFile(filePath);
            File.WriteAllLines(@filePath, lines);
        }
    }
}

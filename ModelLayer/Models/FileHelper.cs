using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class FileHelper
    {
        private static readonly string BaseDirectory = @"F:\NoteImages"; // Replace with your actual base directory

        public static string GetFilePath(string fileName)
        {
            string directoryPath = BaseDirectory;

            // Check if the directory exists; if not, create it
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Combine the base directory and the provided file name
            return Path.Combine(directoryPath, fileName);
        }

    }

}

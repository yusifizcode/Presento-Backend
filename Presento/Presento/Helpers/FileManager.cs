using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Presento.Helpers
{
    public static class FileManager
    {
        public static string Save(string root,string folder,IFormFile file)
        {
            var newFileName = Guid.NewGuid() + (file.FileName.Length > 64 ? file.FileName.Substring(file.FileName.Length - 64, 64) : file.FileName);

            var path = Path.Combine(root,folder,newFileName);

            using(FileStream fs = new FileStream(path,FileMode.Create))
            {
                file.CopyTo(fs);
            }

            return newFileName;
        }

        public static bool Delete(string root,string folder,string filename)
        {
            var path = Path.Combine(root, folder, filename);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }
    }
}

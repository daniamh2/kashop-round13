using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KASHOP.bll.Service
{
    public class FileService:IFileService
    {

        public async Task<string>? UploadAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() +
                    Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
                using (var stream = File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                return fileName;
            }
            return null;
        }

        public void Delete(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
            if (File.Exists(path)) 
                File.Delete(path);
        }
    }
}

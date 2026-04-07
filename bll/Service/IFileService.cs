using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KASHOP.bll.Service
{
    public interface IFileService
    {
        Task <string?> UploadAsync(IFormFile file);
        void Delete(string fileName);
    }
}

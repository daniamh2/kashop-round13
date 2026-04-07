using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KASHOP.dal.DTO.Request
{
    public class BrandRequest
    {
        public IFormFile Logo { get; set; }
        public List<BrandTranslationRequest> Translations { get; set; }
    }
}

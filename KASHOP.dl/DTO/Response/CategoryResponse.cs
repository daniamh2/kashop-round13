using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP.dal.Models;

namespace KASHOP.dal.DTO.Response
{
    public class CategoryResponse
    {
        public List<CategoryTranslationResponse> Translations { get; set; }

    }
}

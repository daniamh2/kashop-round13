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
        public int cat_id { get; set; }

        public  String CreatedUser { get; set; }

        //public List<CategoryTranslationResponse> Translations { get; set; }
        public string Name { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.dal.DTO.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public  String CreatedUser { get; set; }
        public string Name { get; set; }
        public string MainImage { get; set; }

        //public List<ProductTranslationResponse> Translations { get; set; }



    }
}

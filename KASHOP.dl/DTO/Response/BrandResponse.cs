using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP.dal.Models;

namespace KASHOP.dal.DTO.Response
{
    public class BrandResponse
    {
        public int Id { get; set; }
        public String CreatedUser { get; set; }
        public string Logo { get; set; }
        public String Name { get; set; }


    }
}

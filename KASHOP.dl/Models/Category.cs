using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.dal.Models
{
    public class Category
    {
        public int Id { get; set; }
        public List<CategoryTranslation> Translations { get; set; }

    }
}

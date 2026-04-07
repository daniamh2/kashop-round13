using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP.dal.DTO.Response;
using KASHOP.dal.Models;
using Mapster;

namespace KASHOP.bll.Mapping
{
    public static class MapsterConfig
    {
        public static void MapsterConfigRegister()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(dest => dest.cat_id, source => source.Id)
                .Map(dest => dest.CreatedUser, source => source.CreatedBy.UserName)
                .Map(dest => dest.Name, source => source.Translations.Where(t => t.Language == CultureInfo.CurrentCulture.Name)
                .Select(t => t.Name).FirstOrDefault());
            
            
            TypeAdapterConfig<Product, ProductResponse>.NewConfig()
                .Map(dest => dest.CreatedUser, source => source.CreatedBy.UserName)
                .Map(dest => dest.Name, source => source.Translations.Where(t => t.Language == CultureInfo.CurrentCulture.Name)
                .Select(t => t.Name).FirstOrDefault())
                .Map(dest=>dest.MainImage,source=>$"https://localhost:7151/images/{source.MainImage}");

        }

    }
}

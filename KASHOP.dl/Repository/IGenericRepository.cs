using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP.dal.Models;

namespace KASHOP.dal.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(string[]? includes = null);
        Task<T> CreateAsync(T category);


    }
}

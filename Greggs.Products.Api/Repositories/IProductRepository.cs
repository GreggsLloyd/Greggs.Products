using Greggs.Products.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greggs.Products.Api.Repositories  
{
    public interface IProductRepository
    {
        Task<IEnumerable<LatestProduct>> List(int pageStart, int pageSize);
    }
}
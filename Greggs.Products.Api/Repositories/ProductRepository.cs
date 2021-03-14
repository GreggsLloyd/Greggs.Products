using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greggs.Products.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDataAccess<Product> _products;

        public ProductRepository(IDataAccess<Product> products)
        {
            _products = products;
        }

        public async Task<IEnumerable<LatestProduct>> List(int pageStart, int pageSize)
        {
            var products = _products.List(pageStart, pageSize);

            var latestProducts = new List<LatestProduct>();

            foreach (var product in products)
            {
                latestProducts.Add(new LatestProduct
                {
                    Name = product.Name,
                    PriceInPounds = product.PriceInPounds
                });
            }

            var results = latestProducts.OrderByDescending(o => o.DateAdded).ToList();

            return await Task.FromResult(results); 
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Greggs.Products.Api.Interfaces;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.DataAccess
{
    /// <summary>
    /// DISCLAIMER: This is only here to help enable the purpose of this exercise, this doesn't reflect the way we work!
    /// </summary>
    public class ProductAccess : IDataAccess<Product>
    {
        private static readonly IEnumerable<Product> ProductDatabase = new List<Product>()
        {
            new Product {Name = "Sausage Roll", PriceInPounds = 1m},
            new Product {Name = "Vegan Sausage Roll", PriceInPounds = 1.1m},
            new Product {Name = "Steak Bake", PriceInPounds = 1.2m},
            new Product {Name = "Yum Yum", PriceInPounds = 0.7m},
            new Product {Name = "Pink Jammie", PriceInPounds = 0.5m},
            new Product {Name = "Mexican Baguette", PriceInPounds = 2.1m},
            new Product {Name = "Bacon Sandwich", PriceInPounds = 1.95m},
            new Product {Name = "Coca Cola", PriceInPounds = 1.2m}
        };


        /// <summary>
        /// Async method to wrap the non-async List() call.
        ///
        /// In a real database scenario, we would want to pass in the cancellation token to ensure abandoned requests to the
        /// API will abort requests to the database - reducing unwanted load on the database server
        /// </summary>
        /// <param name="pageStart">The oage number to start at</param>
        /// <param name="pageSize">The number of products in a page</param>
        /// <param name="token">Async Task cancellation token</param>
        /// <returns>A paged subselection of the products in the database</returns>
        public async Task<IEnumerable<Product>> ListAsync(int? pageStart, int? pageSize, CancellationToken token)
        {
            return await Task
                .Run(() => List(pageStart, pageSize), token)
                .ConfigureAwait(false);
        }


        /// <summary>
        /// Returns a list of products from the dummy database list.
        /// </summary>
        /// <param name="pageStart">The oage number to start at</param>
        /// <param name="pageSize">The number of products in a page</param>
        /// <returns>A paged subselection of the products in the database</returns>
        public IEnumerable<Product> List(int? pageStart, int? pageSize)
        {
            // TODO: This needs to be fixed as it actually starts at ITEM <pageStart> and retrieve <pageSize> items, rather than starting at page <pageStart> (<pageStart> * <pageSize>) and retrieving <pageSize> items

            var queryable = ProductDatabase.AsQueryable();

            if (pageStart.HasValue)
            {
                queryable = queryable.Skip(pageStart.Value);
            }

            if (pageSize.HasValue)
                queryable = queryable.Take(pageSize.Value);

            return queryable.ToList();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Interfaces;
using Greggs.Products.Api.Models;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace Greggs.Products.UnitTests
{
    public class ProductTests
    {
        private readonly IDataAccess<Product> _productDataAccess;
        private readonly CancellationToken _cancellationToken;

        /// <summary>
        /// These tests assume we are using the mock ProductAccess class with 8 items in the catalog
        /// </summary>
        public ProductTests()
        {
            _productDataAccess = new ProductAccess();
            _cancellationToken = new CancellationToken(false);
        }


        /// <summary>
        /// Verifies that a wide enough request returns all 8 items
        /// </summary>
        [Fact]
        public async void RequestingTheFullCatalogShouldReturnEightItems()
        {
            
            IEnumerable<Product> items = await _productDataAccess.ListAsync(0, int.MaxValue, _cancellationToken).ConfigureAwait(true);

            Assert.Equal(8, items.Count());
        }

        /// <summary>
        /// Verifies that requesting the first page of a five item page returns five items
        /// </summary>
        [Fact]
        public async void RequestingPageOneOfFiveItemsShouldReturnFiveItems()
        {
            IEnumerable<Product> items = await _productDataAccess.ListAsync(0, 5, _cancellationToken).ConfigureAwait(true);

            Assert.Equal(5, items.Count());
        }

        /// <summary>
        /// Verifies that requesting the second page of a five item page returns three items
        ///
        /// TODO: This test currently fails as the DataAccess class as implemented actually start at ITEM <pageStart> and retrieve <pageSize> items,
        /// rather than starting at page <pageStart> (<pageStart> * <pageSize>) and retrieving <pageSize> items
        /// </summary>
        [Fact]
        public async void RequestingPageTwoOfFiveItemsShouldReturnThreeItems()
        {
            IEnumerable<Product> items = await _productDataAccess.ListAsync(1, 5, _cancellationToken).ConfigureAwait(true);

            Assert.Equal(3, items.Count());
        }

        /// <summary>
        /// Verifies that requesting the second page of a three item page returns zero items
        ///
        /// TODO: This test currently fails as the DataAccess class as implemented actually start at ITEM <pageStart> and retrieve <pageSize> items,
        /// rather than starting at page <pageStart> (<pageStart> * <pageSize>) and retrieving <pageSize> items
        /// </summary>
        [Fact]
        public async void RequestingPageThreeOfFiveItemsShouldReturnZeroItems()
        {
            IEnumerable<Product> items = await _productDataAccess.ListAsync(2, 5, _cancellationToken).ConfigureAwait(true);

            Assert.Equal(0, items?.Count() ?? 0);
        }

    }
}

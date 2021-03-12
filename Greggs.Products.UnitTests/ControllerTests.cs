using System.Threading;
using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.Currency;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Interfaces;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Greggs.Products.UnitTests
{
    public class ProductControllerTests
    {
        private readonly ICurrencyConverter _currencyConverter;
        private readonly IDataAccess<Product> _productDataAccess;
        private readonly ProductController _productController;

        public ProductControllerTests()
        {
            _currencyConverter = new CurrencyConverter(new StaticCurrencyCache());
            _productDataAccess = new ProductAccess();

            _productController = new ProductController(null, _productDataAccess, _currencyConverter);
        }

        [Fact]
        public async void RequestingAListInPoundsShouldReturnOkResponse()
        {
            CancellationToken token = new CancellationToken(false);

            IActionResult response = await _productController.Get(token).ConfigureAwait(true);
            OkObjectResult okResult = response as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal((int?)200, okResult.StatusCode );
        }

        [Fact]
        public async void RequestingAListInEurosShouldReturnOkResponse()
        {
            CancellationToken token = new CancellationToken(false);

            IActionResult response = await _productController.Get(token, currencySymbol:"EUR").ConfigureAwait(true);
            OkObjectResult okResult = response as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal((int?)200, okResult.StatusCode);
        }

        [Fact]
        public async void RequestingAListInJpyShouldReturnErrorResponse()
        {
            CancellationToken token = new CancellationToken(false);

            IActionResult response = await _productController.Get(token, currencySymbol: "JPY").ConfigureAwait(true);
            BadRequestObjectResult badResult = response as BadRequestObjectResult;

            Assert.NotNull(badResult);
            Assert.Equal((int?)400, badResult.StatusCode);
            Assert.Contains("is not a supported currency", badResult?.Value.ToString());
        }
    }
}

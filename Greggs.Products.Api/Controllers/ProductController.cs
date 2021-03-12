using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Greggs.Products.Api.Currency;
using Greggs.Products.Api.Interfaces;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers
{
    /// <summary>
    /// Products API
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
 
        private readonly ILogger<ProductController> _logger;
        private readonly IDataAccess<Product> _productDataAccess;
        private readonly ICurrencyConverter _currencyConverter;

        /// <summary></summary>
        public ProductController(ILogger<ProductController> logger, IDataAccess<Product> productDataAccess, ICurrencyConverter currencyConverter)
        {
            _logger = logger;
            _productDataAccess = productDataAccess;
            _currencyConverter = currencyConverter;
        }

        /// <summary>
        /// Returns an array of products, optionally paging the results and adjusting the prices to another currency.
        /// </summary>
        /// <param name="token">Async cancellation token</param>
        /// <param name="pageStart">Starting page number</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="currencySymbol">Pricing currency symbol specified in ISO4127 notation</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type=typeof(Product[]))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(CancellationToken token, int pageStart = 0, int pageSize = 5, string currencySymbol = "GBP")
        {
            try
            {
                if (!Enum.TryParse(currencySymbol, out SupportedCurrency currency))
                    throw new ArgumentException($"{currencySymbol} is not a supported currency");

                Product[] products =
                    (await _productDataAccess.ListAsync(pageStart, pageSize, token).ConfigureAwait(false)).ToArray();

                if (currency != SupportedCurrency.GBP)
                {
                    products
                        .AsParallel()
                        .ForAll(p => p.PriceInPounds = _currencyConverter.ConvertTo(p.PriceInPounds, currency));
                }

                return Ok(products);
            }
            catch (TaskCanceledException)
            {
                _logger?.LogDebug("Request aborted");
                return null;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"An error occurred getting products - pageStart {pageStart}, pageSize {pageSize}, currency {currencySymbol}");
                return BadRequest(new { error = ex.Message});
            }
        }
    }
}

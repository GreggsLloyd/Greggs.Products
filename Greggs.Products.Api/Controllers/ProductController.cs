using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private static readonly string[] Products = new[]
        {
            "Sausage Roll", "Vegan Sausage Roll", "Steak Bake","Yum Yum", "Pink Jammie"
        };

        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;

        public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet]
        public IEnumerable<Product> Get(int pageStart = 0, int pageSize = 5)
        {
            if (pageSize > Products.Length)
                pageSize = Products.Length;

            var rng = new Random();
            return Enumerable.Range(1, pageSize).Select(index => new Product
            {
                PriceInPounds = rng.Next(0, 10),
                Name = Products[rng.Next(Products.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// Get the latest products and also show price in Euros. 
        /// This can be called by both a Greggs Fanatic and Greggs Entrepreneur. 
        /// TODO: Could seperate out calls to different end point service methods to return different models.
        /// </summary>
        /// <param name="pageStart"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("latest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLatest(int pageStart = 0, int pageSize = 5)
        {
            if (pageStart < 0)
            {
                return BadRequest($"{nameof(pageStart)} is out of range, please specify a valid value.");
            }

            if (pageSize == 0 || pageSize < 0)
            {
                return BadRequest($"{nameof(pageSize)} is out of range, please specify a valid value.");
            }

            var model = await _productRepository.List(pageStart, pageSize);

            if (model == null || !model.Any())
            {
                return NotFound("No data was found!");
            }

            return Ok(model);
        }
    }
}

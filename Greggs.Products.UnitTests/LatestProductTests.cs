using Greggs.Products.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Greggs.Products.UnitTests
{
    public class LatestProductTests
    {
        private decimal euroExchangeRate = 1.11M;

        [Fact]
        public void CanGetPriceInEuros()
        {
            var sut = new LatestProduct { PriceInPounds = 7.86M };

            Assert.True(sut.PriceInEuros == 7.86M * euroExchangeRate);
        }

        //TODO One option is to use MOQ for mocking the api controller endpoints
        [Fact]
        public void CanGetLatestProducts()
        {
            throw new NotImplementedException("NotImplementedException :-(");

        }

    }
}

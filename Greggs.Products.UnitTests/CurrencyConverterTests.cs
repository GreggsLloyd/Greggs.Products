using Greggs.Products.Api.Currency;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Interfaces;
using Xunit;

namespace Greggs.Products.UnitTests
{
    public class CurrencyConverterTests
    {
        /// <summary>
        /// Setup the currency converter with the StaticCurrencyCache that contains currency rates that we know
        /// </summary>
        private readonly ICurrencyConverter _currencyConverter;

        public CurrencyConverterTests()
        {
            _currencyConverter = new CurrencyConverter(new StaticCurrencyCache());
        }

        /// <summary>
        /// Conversion to pounds should return the same value as it is the base currency
        /// </summary>
        [Fact]
        public void ConversionToPoundsShouldReturnSameValue()
        {
            decimal priceInPounds = 1.0m;
            decimal convertedRate = _currencyConverter.ConvertTo(priceInPounds, SupportedCurrency.GBP);

            Assert.Equal(priceInPounds, convertedRate);
        }

        /// <summary>
        /// Conversion to euro should return the same value as multiplying it by 1.1, which is the conversion
        /// rate for euros hardcoded in the static currency rates
        /// </summary>
        [Fact]
        public void ConversionToEuroShouldReturnCorrectValue()
        {
            decimal priceInPounds = 1.0m;
            decimal convertedRate = _currencyConverter.ConvertTo(priceInPounds, SupportedCurrency.EUR);

            Assert.Equal((priceInPounds * 1.1m), convertedRate);
        }

    }
}

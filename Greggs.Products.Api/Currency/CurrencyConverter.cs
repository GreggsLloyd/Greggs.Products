using Greggs.Products.Api.Interfaces;

namespace Greggs.Products.Api.Currency
{
    /// <summary>
    /// Implementation of a currency conversion library
    /// </summary>
    public class CurrencyConverter: ICurrencyConverter
    {
        /// <summary>
        /// An instance of a cache for currency conversion rates
        /// </summary>
        private readonly ICurrencyCache _currencyCache;
        
        /// <summary>
        /// Standard constructor, value will be injected via DI under normal usage.
        /// </summary>
        /// <param name="currencyCache">Implementation of the <see cref="ICurrencyCache"/> interface</param>
        public CurrencyConverter(ICurrencyCache currencyCache)
        {
            _currencyCache = currencyCache;
        }

        /// <summary>
        /// Converts a provided decimal value 
        /// </summary>
        /// <param name="priceInPounds">The current price in GBP</param>
        /// <param name="targetCurrency">The currency to convert to</param>
        /// <returns>The current price adjusted to the target currency</returns>
        public decimal ConvertTo(decimal priceInPounds, SupportedCurrency targetCurrency)
        {
            string currencySymbol = targetCurrency.ToString();
            decimal conversionRate = _currencyCache.GetCurrencyRate(currencySymbol);

            return priceInPounds * conversionRate;
        }
    }
}
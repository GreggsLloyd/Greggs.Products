using Greggs.Products.Api.Currency;

namespace Greggs.Products.Api.Interfaces
{
    /// <summary>
    /// Currency conversion abstraction
    /// </summary>
    public interface ICurrencyConverter
    {
        /// <summary>
        /// Converts a provided decimal value 
        /// </summary>
        /// <param name="priceInPounds">The current price in GBP</param>
        /// <param name="targetCurrency">The currency to convert to</param>
        /// <returns>The current price adjusted to the target currency</returns>
        decimal ConvertTo(decimal priceInPounds, SupportedCurrency targetCurrency);
    }
}

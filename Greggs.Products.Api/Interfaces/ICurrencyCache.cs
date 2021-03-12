namespace Greggs.Products.Api.Interfaces
{
    /// <summary>
    /// Provides a method of retrieving and manipulating currency rates
    /// </summary>
    public interface ICurrencyCache
    {

        /// <summary>
        /// Gets a rate from the cache specified by the ISO4127 notation currency symbol
        /// </summary>
        /// <param name="currencySymbol">A ISO4127 notation currency symbol</param>
        /// <returns>The current exchange rate for the specified currency symbol</returns>
        decimal GetCurrencyRate(string currencySymbol);


        // TODO: Other members for updating/refreshing the "cache" would go here
        /// <summary>
        /// Updates a currency exchange rate value in the cache
        /// </summary>
        /// <param name="currencySymbol">A ISO4127 notation currency symbol</param>
        /// <param name="newValue">The new exchange rate</param>
        /// <returns>True if successfully updated</returns>
        bool UpdateCurrencyRate(string currencySymbol, decimal newValue);
    }
}
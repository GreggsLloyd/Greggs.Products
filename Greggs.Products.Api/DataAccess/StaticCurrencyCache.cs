using System;
using System.Collections.Generic;
using System.Data;
using Greggs.Products.Api.Interfaces;

namespace Greggs.Products.Api.DataAccess
{
    /// <summary>
    /// A static implementation of a <see cref="ICurrencyCache"/> containing only GBP and EUR rates
    /// </summary>
    public class StaticCurrencyCache : ICurrencyCache
    {
        // TODO: These values should come from a database or something like a Redis in-memory cache
        private readonly Dictionary<string, decimal> _staticRateDictionary = new Dictionary<string, decimal>()
        {
            {"GBP", 1.0m},
            {"EUR", 1.1m}
        };

        /// <summary>
        /// Gets a rate from the cache specified by the ISO4127 notation currency symbol
        /// </summary>
        /// <param name="currencySymbol">A ISO4127 notation currency symbol</param>
        /// <returns>The current exchange rate for the specified currency symbol</returns>
        public decimal GetCurrencyRate(string currencySymbol)
        {
            return !_staticRateDictionary.ContainsKey(currencySymbol)
                ? throw new DataException("Currency symbol not supported")
                : _staticRateDictionary[currencySymbol];
        }

        /// <summary>
        /// Updates a currency exchange rate value in the cache
        /// </summary>
        /// <param name="currencySymbol">A ISO4127 notation currency symbol</param>
        /// <param name="newValue">The new exchange rate</param>
        /// <returns>True if successfully updated</returns>
        public bool UpdateCurrencyRate(string currencySymbol, decimal newValue)
        {
            // not supported on this implementation
            throw new NotSupportedException("Static currency cache cannot update values");
        }
    }
}
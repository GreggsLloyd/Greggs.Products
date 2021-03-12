using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;
#pragma warning disable 1591

namespace Greggs.Products.Api.Currency
{
    /// <summary>
    /// Contains all of the currency Symbols (in ISO4127 notation) that are currently supported
    /// </summary>
    public enum SupportedCurrency
    {
        GBP,
        EUR
    }
}
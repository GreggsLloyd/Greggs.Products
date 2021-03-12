using System;

namespace Greggs.Products.Api.Models
{
    public class LatestProduct : Product
    {
        public DateTime DateAdded { get; private set; }
        public decimal PriceInEuros
        {
            get { return GetPriceInEuros(PriceInPounds); }
        }

        public LatestProduct()
        {
            var getRandomDate = RandomDayFunc();
            DateAdded = getRandomDate();
        }

        private decimal GetPriceInEuros(decimal priceInPounds)
        {
            const decimal euroExchangeRate = 1.11M;

            return priceInPounds * euroExchangeRate;
        }



        Func<DateTime> RandomDayFunc()
        {
            DateTime start = new DateTime(1995, 1, 1);
            Random gen = new Random();
            int range = ((TimeSpan)(DateTime.Today - start)).Days;
            return () => start.AddDays(gen.Next(range));
        }
    }
}

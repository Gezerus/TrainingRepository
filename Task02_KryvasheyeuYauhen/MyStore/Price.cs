using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore
{
    /// <summary>
    /// support class for money
    /// </summary>
    public class Price
    {
        private int _rubles ;
        private int _coins ;

        public int Rubles
        {
            get
            {
                return _rubles;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("The rubles should be greater than zero");
                _rubles = value;
            }
        }

        public int Coins
        {
            get
            {
                return _coins;
            }
            set
            {
                if (value < 0 || value > 99)
                    throw new ArgumentOutOfRangeException("The rubles should be greater than zero");
                _coins = value;
            }
        }

        public Price()
        {        }
        public Price(int rubles, int coins)
        {
            Rubles = rubles;
            Coins = coins;
        }

        public Price(Price price)
        {
            Rubles = price.Rubles;
            Coins = price.Coins;
        }

        public static implicit operator int (Price price)
        {
            return price.Rubles * 100 + price.Coins;
        }

        public static implicit operator double(Price price)
        {
            return (double)(int)price / 100;
        }



    }
}

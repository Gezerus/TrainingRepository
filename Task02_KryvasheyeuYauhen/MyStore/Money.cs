using System;

namespace MyStore
{
    /// <summary>
    /// support class for money
    /// </summary>
    public class Money
    {
        private int _rubles;
        private int _coins;

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

        public Money()
        { }
        public Money(int rubles, int coins)
        {
            Rubles = rubles;
            Coins = coins;
        }

        public Money(Money money)
        {
            Rubles = money.Rubles;
            Coins = money.Coins;
        }
        /// <summary>
        /// Converts money to int
        /// </summary>
        /// <param name="money"></param>
        public static implicit operator int(Money money)
        {
            return money.Rubles * 100 + money.Coins;
        }

        /// <summary>
        /// Converts money to double
        /// </summary>
        /// <param name="price"></param>
        public static implicit operator double(Money price)
        {
            return (double)(int)price / 100;
        }

        /// <summary>
        /// Converts int to Money
        /// </summary>
        /// <param name="intPrice"></param>
        public static implicit operator Money(int intPrice)
        {
            int rubles = intPrice / 100;
            int coins = intPrice % 100;

            return new Money(rubles, coins);
        }

        /// <summary>
        /// Converts double to money
        /// </summary>
        /// <param name="doublePrice"></param>
        public static explicit operator Money(double doublePrice)
        {
            int rubles = (int)doublePrice;
            int coins = (int)((doublePrice - rubles) * 100);

            return new Money(rubles, coins);
        }

        /// <summary>
        /// adds two money
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns>addition result</returns>
        public static Money operator + (Money m1, Money m2)
        {
            int rubles = m1.Rubles + m2.Rubles;
            int coins = m1.Coins + m2.Coins;
            if(coins > 99)
            {
                rubles += 1;
                coins -= 100;
            }
            return new Money(rubles, coins);
        }

        public override bool Equals(object obj)
        {
            if (obj is Money && obj != null)
            {
                Money temp = (Money)obj;
                if (temp._rubles == this._rubles && temp._coins == this._coins)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (_rubles << 2) ^ _coins;
        }
    }
}

using System;


namespace MyStore
{
    /// <summary>
    /// describes a product
    /// </summary>
    public abstract class Product
    {
        public string Name { get; set; }
        public Money Price { get; set; }

        /// <summary>
        /// initializes a product
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cost"></param>
        public Product(string name, Money cost)
        {
            Name = name;
            Price = cost;
        }
    }

    




}

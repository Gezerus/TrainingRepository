using System;


namespace MyStore
{
    /// <summary>
    /// describes a product
    /// </summary>
    public class Product
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public Money Price { get; set; }

        public Product(string type, string name, Money cost)
        {
            Type = type;
            Name = name;
            Price = cost;
        }

        public Product(Product product)
        {
            Type = product.Type;
            Name = product.Name;
            Price = product.Price;
        }

        public static Product operator + (Product p1, Product p2)
        {

            if (p1.Type != p2.Type)
                throw new ArgumentOutOfRangeException("Products should be of the same type");

            string name = p1.Name + " - " + p2.Name;

            var cost = (p1.Price + p2.Price) / 2;

            return new Product(p1.Type, name, cost);
        }

        public static explicit operator int(Product product)
        {
            return product.Price;
        }

        public static explicit operator double(Product product)
        {
            return product.Price;
        }


    }

    




}

using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQExtensionMethodsExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            //Extension MEthods

            List<Product> products = new List<Product>()
            {
                new Product() { Id = 1, Price = 35 },
                new Product() { Id = 2, Price = 150 },
                new Product() { Id = 3, Price = 650 },
                new Product() { Id = 4, Price = 150 },
                new Product() { Id = 5, Price = 15 },
                new Product() { Id = 6, Price = 35 },
                new Product() { Id = 7, Price = 650 },
                new Product() { Id = 8, Price = 78 },
                new Product() { Id = 9, Price = 35 },
                new Product() { Id = 10, Price = 78 },
            };

            Console.WriteLine($"Average: {products.Average(p => p.Price)}");
            Console.WriteLine($"Median: {products.Median(p => p.Price)}");
            Console.WriteLine($"Mode: {products.Mode(p => p.Price)}");
            Console.WriteLine($"Less Common: {products.UnMode(p => p.Price)}");
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public int Price { get; set; }
    }
}

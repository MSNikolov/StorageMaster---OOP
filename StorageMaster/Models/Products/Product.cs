using StorageMaster.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Models.Products
{
    public abstract class Product : IProduct
    {
        private double price;
        private double weight;

        public Product(double price, double weight)
        {
            this.Price = price;
            this.weight = weight;
        }

        public double Price
        {
            get => this.price;
            
            private set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException("Price cannot be negative!");
                }
                this.price = value;
            }
        }

        public double Weight => this.weight;
    }
}

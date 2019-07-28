using StorageMaster.Models.Contracts;
using StorageMaster.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageMaster.Models.Vehicles
{
    public abstract class Vehicle : IVehicle
    {
        private int capacity;
        private List<Product> trunk;

        public Vehicle (int capacity)
        {
            this.capacity = capacity;
            this.trunk = new List<Product>();
        }

        public int Capacity => this.capacity;

        public IReadOnlyCollection<Product> Trunk => this.trunk;

        public bool IsFull => this.trunk.Sum(p => p.Weight) >= this.capacity;

        public bool IsEmpty => this.trunk.Count == 0;

        public void LoadProduct(Product product)
        {
            if (this.IsFull==true)
            {
                throw new InvalidOperationException("Vehicle is full!");
            }

            this.trunk.Add(product);
        }

        public Product Unload()
        {
            if (this.IsEmpty==true)
            {
                throw new InvalidOperationException("No products left in vehicle!");
            }

            var result = this.trunk.Last();

            this.trunk.Remove(result);

            return result;
        }
    }
}

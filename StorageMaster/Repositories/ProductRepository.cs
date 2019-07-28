using StorageMaster.Models.Contracts;
using StorageMaster.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private List<IProduct> products;

        public ProductRepository()
        {
            this.products = new List<IProduct>();
        }

        public IReadOnlyCollection<IProduct> Products => this.products;

        public void Add(IProduct product)
        {
            this.products.Add(product);
        }

        public void Remove(IProduct product)
        {
            this.products.Remove(product);
        }
    }
}

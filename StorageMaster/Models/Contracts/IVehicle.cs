using StorageMaster.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Models.Contracts
{
    public interface IVehicle
    {
        int Capacity { get; }

        IReadOnlyCollection <Product> Trunk { get; }

        bool IsFull { get; }

        bool IsEmpty { get; }

        void LoadProduct(Product product);

        Product Unload();
    }
}

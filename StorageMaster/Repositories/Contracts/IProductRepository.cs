using StorageMaster.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Repositories.Contracts
{
    public interface IProductRepository
    {
        IReadOnlyCollection<IProduct> Products { get; }

        void Add(IProduct product);

        void Remove(IProduct product);
    }
}

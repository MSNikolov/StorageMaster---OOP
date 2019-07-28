using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Models.Contracts
{
    public interface IProduct
    {
        double Price { get; }

        double Weight { get; }
    }
}

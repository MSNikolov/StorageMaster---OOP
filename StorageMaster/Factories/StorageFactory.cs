using StorageMaster.Factories.Contracts;
using StorageMaster.Models.Contracts;
using StorageMaster.Models.Storages;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Factories
{
    public class StorageFactory : IStorageFactory
    {
        public IStorage CreateStorage(string type, string name)
        {
            switch (type)
            {
                case "AutomatedWarehouse":
                    return new AutomatedWarehouse(name);
                case "DistributionCenter":
                    return new DistributionCenter(name);
                case "Warehouse":
                    return new Warehouse(name);
                default: throw new InvalidOperationException("Invalid storage type!");
            }
        }
    }
}

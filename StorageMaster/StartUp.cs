using StorageMaster.Core;
using StorageMaster.Factories;
using StorageMaster.I_O;
using StorageMaster.Models.Storages;
using StorageMaster.Repositories;
using System;

namespace StorageMaster
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var storageFac = new StorageFactory();

            var storageRepo = new StorageRepository();

            var productFac = new ProductFactory();

            var productRepo = new ProductRepository();

            var master = new Core.StorageMaster(productFac, productRepo, storageFac, storageRepo);

            var reader = new Reader();

            var writer = new Writer();

            var engine = new Engine(master, reader, writer);

            engine.Run();
        }
    }
}

using StorageMaster.Models.Contracts;
using StorageMaster.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private List<IStorage> storages;

        public StorageRepository()
        {
            this.storages = new List<IStorage>();
        }

        public IReadOnlyCollection<IStorage> Storages => this.storages;

        public void Add(IStorage storage)
        {
            this.storages.Add(storage);
        }

        
    }
}

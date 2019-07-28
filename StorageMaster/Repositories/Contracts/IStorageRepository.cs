using StorageMaster.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Repositories.Contracts
{
    public interface IStorageRepository
    {
        IReadOnlyCollection<IStorage> Storages { get; }

        void Add(IStorage storage);
    }
}

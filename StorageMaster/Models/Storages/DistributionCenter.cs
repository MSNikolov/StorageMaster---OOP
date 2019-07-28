using System;
using System.Collections.Generic;
using System.Text;
using StorageMaster.Models.Contracts;
using StorageMaster.Models.Vehicles;

namespace StorageMaster.Models.Storages
{
    public class DistributionCenter : Storage
    {
        public DistributionCenter(string name) : base(name, 2, 5, new Vehicle[3] { new Van(), new Van(), new Van()})
        {
        }
    }
}

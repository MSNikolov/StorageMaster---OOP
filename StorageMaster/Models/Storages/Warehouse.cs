using System;
using System.Collections.Generic;
using System.Text;
using StorageMaster.Models.Contracts;
using StorageMaster.Models.Vehicles;

namespace StorageMaster.Models.Storages
{
    public class Warehouse : Storage
    {
        public Warehouse(string name) : base(name, 10, 10, new Vehicle[3] { new Semi(), new Semi(), new Semi()})
        {
        }
    }
}

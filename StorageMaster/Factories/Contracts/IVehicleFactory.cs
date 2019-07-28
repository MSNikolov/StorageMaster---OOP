using StorageMaster.Models.Contracts;
using StorageMaster.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Factories.Contracts
{
    public interface IVehicleFactory
    {
        Vehicle CreateVehicle(string type);
    }
}

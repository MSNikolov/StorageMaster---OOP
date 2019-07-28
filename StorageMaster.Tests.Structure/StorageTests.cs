using NUnit.Framework;
using StorageMaster.Models.Products;
using StorageMaster.Models.Storages;
using StorageMaster.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageMaster.Tests.Structure
{
    [TestFixture]
    public class StorageTests
    {
        private Storage stor;
        [SetUp]
        public void Setup()
        {
            this.stor = new Warehouse("Record");
        }
        [Test]
        public void IsCapacityCorrect()
        {
            Assert.AreEqual(10, this.stor.Capacity);
        }

        [Test]
        public void AreGarageSlotsCorrect()
        {
            Assert.AreEqual(10, this.stor.GarageSlots);
        }

        [Test]
        public void IsInitialGarageCorrect()
        {
            Assert.AreEqual(3, this.stor.Garage.Where(x => x != null).Count());
        }

        [Test]
        public void AreInitialProductsZero()
        {
            Assert.AreEqual(0, this.stor.Products.Count);
        }

        [Test]
        public void DoesGetVehicleReturn()
        {
            var res = this.stor.GetVehicle(1);

            Assert.AreEqual(typeof(Semi), res.GetType());
        }

        [Test]
        public void DoesGetVehicleThrowWhenBiggerThanGarage()
        {
            Assert.Throws<InvalidOperationException>(() => this.stor.GetVehicle(11));
        }

        [Test]
        public void DoesGetVehicleThrowWhenSpotIsEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => this.stor.GetVehicle(3));
        }

        [Test]
        public void DoesSendVehicleReturn()
        {
            Assert.AreEqual(3, this.stor.SendVehicleTo(1, new DistributionCenter("Malkonare")));
        }

        [Test]
        public void DoesSendVehicleThrow()
        {
            var dest = new AutomatedWarehouse("Basisklado");

            this.stor.SendVehicleTo(1, dest);

            Assert.Throws<InvalidOperationException>(() => this.stor.SendVehicleTo(0, dest));
        }

        [Test]
        public void DoesSendThrowWhenWrongSlot()
        {
            Assert.Throws<InvalidOperationException>(() => this.stor.SendVehicleTo(4, new Warehouse("Putkinhan")));
        }

        [Test]
        public void DoesUnloadWork()
        {
            this.stor.GetVehicle(0).LoadProduct(new Ram(6.90));

            Assert.AreEqual(1, this.stor.UnloadVehicle(0));
        }

        [Test]
        public void DoesUnloadThrowWhenFull()
        {
            for (int i = 0; i < 10; i++)
            {
                this.stor.GetVehicle(0).LoadProduct(new HardDrive(10.40));
            }
            this.stor.GetVehicle(1).LoadProduct(new Ram(8.0));

            this.stor.UnloadVehicle(0);

            Assert.Throws<InvalidOperationException>(() => this.stor.UnloadVehicle(1));
        }
    }
}

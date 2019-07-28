using NUnit.Framework;
using StorageMaster.Factories;
using StorageMaster.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Tests.Structure
{
    [TestFixture]
    class FactoryTests
    {
        [Test]
        public void TestProductFactory()
        {
            var fac = new ProductFactory();

            var res = fac.CreateProduct("Ram", 9.80);

            Assert.AreEqual(typeof(Ram), res.GetType());

            Assert.AreEqual(9.80, res.Price);
        }

        [Test]
        public void TestProductFactoryForInvalidType()
        {
            var fac = new ProductFactory();

            Assert.Throws<InvalidOperationException>(() => fac.CreateProduct("Monitor", 8.9));
        }

        [Test]
        public void TestStorageFactory()
        {
            var fac = new StorageFactory();

            var store = fac.CreateStorage("Warehouse", "Kurax");

            Assert.AreEqual(10, store.Capacity);
        }

        [Test]
        public void TestIfStorageFactortThrows()
        {
            var fac = new StorageFactory();

            Assert.Throws<InvalidOperationException>(() => fac.CreateStorage("Metro", "Billa"));
        }

        [Test]
        public void TestVehicleFactory()
        {
            var fac = new VehicleFactory();

            var veh = fac.CreateVehicle("Semi");

            Assert.AreEqual(10, veh.Capacity);
        }

        [Test]
        public void TestIfVehFactoryThrows()
        {
            var fac = new VehicleFactory();

            Assert.Throws<InvalidOperationException>(() => fac.CreateVehicle("Kamion"));
        }
    }
}

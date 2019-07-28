using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using StorageMaster.Core;
using StorageMaster.Factories;
using StorageMaster.Repositories;

namespace StorageMaster.Core
{
    [TestFixture]
    class StorageMasterTests
    {
        private StorageMaster master;
        [Test]
        public void ATestStorageStatus()
        {
            this.master = new StorageMaster(new ProductFactory(), new ProductRepository(), new StorageFactory(), new StorageRepository());

            this.master.RegisterStorage("Warehouse", "OMG");

            this.master.SelectVehicle("OMG", 1);

            this.master.AddProduct("HardDrive", 8.60);

            this.master.AddProduct("HardDrive", 7.60);

            this.master.AddProduct("HardDrive", 1.50);

            this.master.RegisterStorage("Warehouse", "Fantastico");

            this.master.LoadVehicle(new string[3] { "HardDrive", "HardDrive", "HardDrive" });

            this.master.SendVehicleTo("OMG", 1, "Fantastico");

            this.master.UnloadVehicle("Fantastico", 3);

            Assert.AreEqual($"Stock (3/10): [HardDrive (3)]\r\nGarage: [Semi|Semi|Semi|Semi|empty|empty|empty|empty|empty|empty]", this.master.GetStorageStatus("Fantastico"));
        }

        [Test]
        public void TestConstructorAndSummaryWhenEmpty()
        {
            this.master = new StorageMaster(new ProductFactory(), new ProductRepository(), new StorageFactory(), new StorageRepository());
            Assert.AreEqual("", this.master.GetSummary());
        }

        [Test]
        public void TestAddProduct()
        {
            this.master = new StorageMaster(new ProductFactory(), new ProductRepository(), new StorageFactory(), new StorageRepository());
            Assert.AreEqual("Added Ram to pool", this.master.AddProduct("Ram", 8.60));
        }

        [Test]
        public void TestRegisterStorage()
        {
            this.master = new StorageMaster(new ProductFactory(), new ProductRepository(), new StorageFactory(), new StorageRepository());
            Assert.AreEqual("Registered OMG", this.master.RegisterStorage("Warehouse", "OMG"));
        }

        [Test]
        public void TestSelectVehicle()
        {
            this.master = new StorageMaster(new ProductFactory(), new ProductRepository(), new StorageFactory(), new StorageRepository());
            this.master.RegisterStorage("Warehouse", "OMG");

            Assert.AreEqual("Selected Semi", this.master.SelectVehicle("OMG", 1));
        }

        [Test]
        public void TestLoading()
        {
            this.master = new StorageMaster(new ProductFactory(), new ProductRepository(), new StorageFactory(), new StorageRepository());
            this.master.RegisterStorage("Warehouse", "OMG");

            this.master.SelectVehicle("OMG", 1);

            this.master.AddProduct("Ram", 8.60);

            this.master.AddProduct("Ram", 7.60);

            Assert.AreEqual("Loaded 2/2 products into Semi", this.master.LoadVehicle(new string[2] { "Ram", "Ram" }));
        }

        [Test]
        public void TestLoadingWithMoreStuffThrows()
        {
            this.master = new StorageMaster(new ProductFactory(), new ProductRepository(), new StorageFactory(), new StorageRepository());
            this.master.RegisterStorage("Warehouse", "OMG");

            this.master.SelectVehicle("OMG", 1);

            this.master.AddProduct("Ram", 8.60);

            this.master.AddProduct("Ram", 7.60);

            Assert.Throws<InvalidOperationException>(()=> this.master.LoadVehicle(new string[3] { "Ram", "Ram", "Ram" }));
        }

        [Test]
        public void TestLoadingWhenVehicleIsFull()
        {
            this.master = new StorageMaster(new ProductFactory(), new ProductRepository(), new StorageFactory(), new StorageRepository());
            this.master.RegisterStorage("DistributionCenter", "OMG");

            this.master.SelectVehicle("OMG", 1);

            this.master.AddProduct("HardDrive", 8.60);

            this.master.AddProduct("HardDrive", 7.60);

            this.master.AddProduct("HardDrive", 1.50);

            Assert.AreEqual("Loaded 2/3 products into Van", this.master.LoadVehicle(new string[3] { "HardDrive", "HardDrive", "HardDrive" }));
        }

        [Test]
        public void TestSendVehicle()
        {
            this.master = new StorageMaster(new ProductFactory(), new ProductRepository(), new StorageFactory(), new StorageRepository());
            this.master.RegisterStorage("DistributionCenter", "OMG");

            this.master.SelectVehicle("OMG", 1);

            this.master.AddProduct("HardDrive", 8.60);

            this.master.AddProduct("HardDrive", 7.60);

            this.master.AddProduct("HardDrive", 1.50);

            this.master.RegisterStorage("Warehouse", "Fantastico");

            Assert.AreEqual("Sent Van to Fantastico (slot 3)", this.master.SendVehicleTo("OMG", 1, "Fantastico"));
        }

        [Test]
        public void SendVehicleShouldThrowWhenWrongStorage()
        {
            this.master = new StorageMaster(new ProductFactory(), new ProductRepository(), new StorageFactory(), new StorageRepository());
            this.master.RegisterStorage("DistributionCenter", "OMG");

            this.master.SelectVehicle("OMG", 1);

            this.master.AddProduct("HardDrive", 8.60);

            this.master.AddProduct("HardDrive", 7.60);

            this.master.AddProduct("HardDrive", 1.50);

            this.master.RegisterStorage("Warehouse", "Fantastico");

            Assert.Throws<InvalidOperationException>(() => this.master.SendVehicleTo("Kurax", 1, "Fantastico"));

            Assert.Throws<InvalidOperationException>(() => this.master.SendVehicleTo("OMG", 1, "Kurax"));
        }

        [Test]
        public void ATestUnloadVehicle()
        {
            this.master = new StorageMaster(new ProductFactory(), new ProductRepository(), new StorageFactory(), new StorageRepository());
            this.master.RegisterStorage("Warehouse", "OMG");

            this.master.SelectVehicle("OMG", 1);

            this.master.AddProduct("HardDrive", 8.60);

            this.master.AddProduct("HardDrive", 7.60);

            this.master.AddProduct("HardDrive", 1.50);

            this.master.RegisterStorage("Warehouse", "Fantastico");

            this.master.LoadVehicle(new string[3] { "HardDrive", "HardDrive", "HardDrive" });

            this.master.SendVehicleTo("OMG", 1, "Fantastico");

            Assert.AreEqual("Unloaded 3/3 products at Fantastico", this.master.UnloadVehicle("Fantastico", 3));
        }
    }
}

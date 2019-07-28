using NUnit.Framework;
using StorageMaster.Models.Products;
using StorageMaster.Models.Vehicles;
using System;

namespace StorageMaster.Tests
{
    [TestFixture]
    public class Tests
    {
        private Vehicle vehicle;
        [SetUp]
        public void Setup()
        {
            this.vehicle = new Van();
        }

        [Test]
        public void IsCapacityCorrect()
        {
            Assert.AreEqual(2, this.vehicle.Capacity);
        }

        [Test]
        public void DoesItHaveAnEpmtyTruck()
        {
            Assert.AreEqual(true, this.vehicle.IsEmpty);
        }

        [Test]
        public void DoesItLoadCorrectly()
        {
            var ram = new Ram(87.90);

            this.vehicle.LoadProduct(ram);

            Assert.AreEqual(1, this.vehicle.Trunk.Count);
        }

        [Test]
        public void DoesItGetFull ()
        {
            var ram = new Ram(90.90);
            var hd = new HardDrive(14.50);
            var hdd = new HardDrive(13.40);
            this.vehicle.LoadProduct(ram);
            this.vehicle.LoadProduct(hd);
            this.vehicle.LoadProduct(hdd);
            Assert.AreEqual(true, this.vehicle.IsFull);
        }

        [Test]
        public void DoesItThrowWhenFull()
        {
            var ram = new Ram(90.90);
            var hd = new HardDrive(14.50);
            var hdd = new HardDrive(13.40);
            this.vehicle.LoadProduct(ram);
            this.vehicle.LoadProduct(hd);
            this.vehicle.LoadProduct(hdd);
            Assert.Throws<InvalidOperationException>(() => this.vehicle.LoadProduct(new Gpu(10.90)));
        }

        [Test]
        public void DoesItUnload()
        {
            var ram = new Ram(10.30);

            this.vehicle.LoadProduct(ram);

            Assert.AreSame(ram, this.vehicle.Unload());
        }

        [Test]
        public void DoesItThrowWhenUnloadEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => this.vehicle.Unload());
        }
    }
}
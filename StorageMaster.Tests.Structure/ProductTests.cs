using NUnit.Framework;
using StorageMaster.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Tests.Structure
{
    [TestFixture]
    class ProductTests
    {
        private Product product;
        [Test]
        public void DoesConstructorWork()
        {
            this.product = new Ram(9.50);

            Assert.AreEqual(0.1, this.product.Weight);
            Assert.AreEqual(9.5, this.product.Price);
        }

        [Test]
        public void DoesNegativePriceThrow()
        {
            Assert.Throws<InvalidOperationException>(() => this.product = new Ram(-5.6));
        }
    }
}

using StorageMaster.Core.Contracts;
using StorageMaster.Factories;
using StorageMaster.Factories.Contracts;
using StorageMaster.Models.Contracts;
using StorageMaster.Models.Products;
using StorageMaster.Models.Storages;
using StorageMaster.Repositories;
using StorageMaster.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageMaster.Core
{
    public class StorageMaster: IStorageMaster
    {
        private IProductFactory productFactory;
        private IProductRepository productRepository;
        private IStorageFactory storageFactory;
        private IStorageRepository storageRepository;
        private IVehicle currentVehicle;

        public StorageMaster(IProductFactory productFactory, IProductRepository productRepository, IStorageFactory storageFactory, IStorageRepository storageRepository)
        {
            this.productFactory = productFactory;
            this.productRepository = productRepository;
            this.storageFactory = storageFactory;
            this.storageRepository = storageRepository;
            this.currentVehicle = null;
        }

        public string AddProduct(string type, double price)
        {
            var product = this.productFactory.CreateProduct(type, price);

            this.productRepository.Add(product);

            return $"Added {type} to pool";
        }

        public string RegisterStorage(string type, string name)
        {
            var store = this.storageFactory.CreateStorage(type, name);

            this.storageRepository.Add(store);

            return $"Registered {name}";
        }

        public string SelectVehicle(string storageName, int garageSlot)
        {
            this.currentVehicle = this.storageRepository.Storages.First(s => s.Name == storageName).GetVehicle(garageSlot);

            return $"Selected {currentVehicle.GetType().Name}";
        }

        public string LoadVehicle(IEnumerable<string> productNames)
        {
            var loadedProductsCount = 0;

            foreach (var item in productNames)
            {
                if (!this.productRepository.Products.Any(p => p.GetType().Name == item))
                {
                    throw new InvalidOperationException($"{item} is out of stock!");
                }

                var product = (Product)this.productRepository.Products.Last(p => p.GetType().Name == item);

                this.productRepository.Remove(product);

                this.currentVehicle.LoadProduct(product);

                loadedProductsCount++;

                if (this.currentVehicle.IsFull)
                {
                    break;
                }
            }

            return $"Loaded {loadedProductsCount}/{productNames.Count()} products into {currentVehicle.GetType().Name}";
        }

        public string SendVehicleTo(string sourceName, int sourceGarageSlot, string destinationName)
        {
            if (!this.storageRepository.Storages.Any(s => s.Name == sourceName))
            {
                throw new InvalidOperationException("Invalid source storage!");
            }

            if (!this.storageRepository.Storages.Any(s => s.Name == destinationName))
            {
                throw new InvalidOperationException("Invalid destination storage!");
            }

            var vehicle = this.storageRepository.Storages.First(s => s.Name == sourceName).GetVehicle(sourceGarageSlot);

            var vehicleType = vehicle.GetType().Name;

            this.storageRepository.Storages.First(s => s.Name == sourceName).SendVehicleTo(sourceGarageSlot, (Storage)this.storageRepository.Storages.First(s => s.Name == destinationName));

            var destinationGarage = this.storageRepository.Storages.First(s => s.Name == destinationName).Garage.ToList();

            var destinationGarageSlot = 0;

            for (int i = 0; i < destinationGarage.Count; i++)
            {
                if (destinationGarage[i] == vehicle)
                {
                    destinationGarageSlot = i;

                    break;
                }
            }

            return $"Sent {vehicleType} to {destinationName} (slot {destinationGarageSlot})";
        }

        public string UnloadVehicle(string storageName, int garageSlot)
        {
            var vehicle = this.storageRepository.Storages.First(s => s.Name == storageName).GetVehicle(garageSlot);

            var productsInVehicle = vehicle.Trunk.Count();

            this.storageRepository.Storages.First(s => s.Name == storageName).UnloadVehicle(garageSlot);

            var unloadedProductsCount = productsInVehicle - vehicle.Trunk.Count;

            return $"Unloaded {unloadedProductsCount}/{productsInVehicle} products at {storageName}";
        }

        public string GetStorageStatus(string storageName)
        {
            var storage = this.storageRepository.Storages.First(s => s.Name == storageName);

            var products = new Dictionary<string, List<IProduct>>();

            double sumOfWeights = 0;

            foreach (var item in storage.Products)
            {
                if (!products.ContainsKey(item.GetType().Name))
                {
                    products.Add(item.GetType().Name, new List<IProduct>());
                }

                products[item.GetType().Name].Add(item);

                sumOfWeights += item.Weight;
            }

            products = products.OrderByDescending(p => p.Value.Count).ThenBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value);

            var productString = new List<string>();

            foreach (var item in products)
            {
                productString.Add(item.Key + $" ({item.Value.Count})");
            }

            var vehicles = new List<string>();

            foreach (var vehicle in storage.Garage)
            {
                if (vehicle == null)
                {
                    vehicles.Add("empty");
                }

                else
                {
                    vehicles.Add(vehicle.GetType().Name);
                }
            }

            var vehicleString = string.Join('|', vehicles);

            var sb = new StringBuilder();

            sb.AppendLine($"Stock ({sumOfWeights}/{storage.Capacity}): [{string.Join(", ", productString)}]");

            sb.AppendLine($"Garage: [{vehicleString}]");

            return sb.ToString().Trim();
        }

        public string GetSummary()
        {
            var storages = this.storageRepository.Storages.OrderByDescending(s => s.Products.Sum(p => p.Price));

            var sb = new StringBuilder();

            foreach (var storage in storages)
            {
                sb.AppendLine($"{storage.Name}:");
                sb.AppendLine($"Storage worth: ${storage.Products.Sum(p => p.Price):F2}");
            }

            return sb.ToString().Trim();
        }

    }
}

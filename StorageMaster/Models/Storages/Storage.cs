using StorageMaster.Factories;
using StorageMaster.Factories.Contracts;
using StorageMaster.Models.Contracts;
using StorageMaster.Models.Products;
using StorageMaster.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageMaster.Models.Storages
{
    public abstract class Storage : IStorage
    {
        private string name;
        private int capacity;
        private int garageSlots;
        private Vehicle[] garage;
        private List<Product> products;

        public Storage (string name, int capacity, int garageSlots, IEnumerable<Vehicle> vehicles)
        {
            this.name = name;
            this.capacity = capacity;
            this.garageSlots = garageSlots;
            this.garage = new Vehicle[this.garageSlots];
            var garageList = new List<Vehicle>();
            this.products = new List<Product>();
            foreach (var veh in vehicles)
            {
                garageList.Add(veh);
            }
            for (int i = 0; i < garageList.Count; i++)
            {
                this.garage[i] = garageList[i];
            }
        }

        public string Name => this.name;

        public int Capacity => this.capacity;

        public int GarageSlots => this.garageSlots;

        public bool IsFull => this.Products.Sum(p => p.Weight) >= this.Capacity;

        public IReadOnlyCollection<Vehicle> Garage => this.garage;

        public IReadOnlyCollection<Product> Products => this.products;

        public Vehicle GetVehicle(int garageSlot)
        {
            if (garageSlot >= this.GarageSlots)
            {
                throw new InvalidOperationException("Invalid garage slot!");
            }

            if (this.garage[garageSlot] == null)
            {
                throw new InvalidOperationException("No vehicle in this garage slot!");
            }

            return this.garage[garageSlot];
        }

        public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
        {
            var vehicle = this.GetVehicle(garageSlot);

            if (!deliveryLocation.Garage.Any(v => v == null))
            {
                throw new InvalidOperationException("No room in garage!");
            }

            this.garage[garageSlot] = null;

            var field = typeof(Storage).GetField("garage", System.Reflection.BindingFlags.Instance| System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

            var receiver = (IVehicle[])field.GetValue(deliveryLocation);

            var free = 0;

            for (int i = 0; i < receiver.Length; i++)
            {
                if (receiver.GetValue(i) == null)
                {
                    free = i;
                    break;
                }
            }

            receiver.SetValue(vehicle, free);

            return free;
        }

        public int UnloadVehicle(int garageSlot)
        {
            if (this.IsFull)
            {
                throw new InvalidOperationException("Storage is full!");
            }

            var vehicle = this.GetVehicle(garageSlot);

            var result = 0;

            while (!vehicle.IsEmpty && !this.IsFull)
            {
                this.products.Add(vehicle.Unload());

                result++;
            }

            return result;
        }
        
    }
}

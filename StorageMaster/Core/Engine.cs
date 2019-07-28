using StorageMaster.Core.Contracts;
using StorageMaster.I_O.Contracts;
using StorageMaster.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Core
{
    public class Engine : IEngine
    {
        IStorageMaster master;
        IReader reader;
        IWriter writer;

        public Engine (IStorageMaster master, IReader reader, IWriter writer)
        {
            this.master = master;
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            var commands = new List<string>();

            var input = reader.Read();

            while (input != "END")
            {
                commands.Add(input);

                input = reader.Read();
            }

            foreach (var com in commands)
            {
                var command = com.Split();

                try
                {
                    switch (command[0])
                    {
                        case "AddProduct":
                            this.writer.Write(this.master.AddProduct(command[1], double.Parse(command[2])));
                            break;
                        case "RegisterStorage":
                            this.writer.Write(this.master.RegisterStorage(command[1], command[2]));
                            break;
                        case "SelectVehicle":
                            this.writer.Write(this.master.SelectVehicle(command[1], int.Parse(command[2])));
                            break;
                        case "LoadVehicle":
                            var products = new List<string>();

                            for (int i = 1; i < command.Length; i++)
                            {
                                products.Add(command[i]);
                            }

                            var productsToLoad = products.ToArray();

                            writer.Write(this.master.LoadVehicle(productsToLoad));

                            break;

                        case "SendVehicleTo":
                            writer.Write(this.master.SendVehicleTo(command[1], int.Parse(command[2]), command[3]));
                            break;
                        case "UnloadVehicle":
                            writer.Write(this.master.UnloadVehicle(command[1], int.Parse(command[2])));
                            break;
                        case "GetStorageStatus":
                            writer.Write(this.master.GetStorageStatus(command[1]));
                            break;
                    }
                }

                catch (InvalidOperationException ex)
                {
                    writer.Write($"Error: {ex.Message}");
                }
            }

            writer.Write(this.master.GetSummary());
        }
    }
}

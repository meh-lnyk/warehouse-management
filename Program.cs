using System;
using System.Collections.Generic;

namespace WarehouseApp
{
    public abstract class WarehouseItem
    {
        public Guid Id { get; } = Guid.NewGuid();
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public double Weight { get; set; }
    }

    public class Box : WarehouseItem
    {
        public DateTime? ExpirationDate { get; set; }
        public DateTime? ManufactureDate { get; set; }
    }

    public class Pallet : WarehouseItem
    {
        public List<Box> Boxes { get; set; } = new();
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Warehouse app initialized.");
        }
    }
}

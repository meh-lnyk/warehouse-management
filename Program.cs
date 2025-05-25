using System;
using System.Collections.Generic;

namespace WarehouseApp
{
    class Program
    {
        static List<Pallet> GenerateSamplePallets()
        {
            var random = new Random();
            var pallets = new List<Pallet>();

            for (int i = 0; i < 3; i++)
            {
                var pallet = new Pallet();

                int boxCount = random.Next(1, 5);
                for (int j = 0; j < boxCount; j++)
                {
                    var box = new Box
                    {
                        Width = Math.Round(random.NextDouble() * 0.6 + 0.1, 2),
                        Height = Math.Round(random.NextDouble() * 0.5 + 0.1, 2),
                        Depth = Math.Round(random.NextDouble() * 0.6 + 0.1, 2),
                        Weight = Math.Round(random.NextDouble() * 10 + 1, 2),
                        ManufactureDate = DateTime.Today.AddDays(-random.Next(0, 150))
                    };
                    pallet.Boxes.Add(box);
                }

                pallets.Add(pallet);
            }

            return pallets;
        }

        static void DisplayPallets(List<Pallet> pallets)
        {
            Console.WriteLine("\nСписок паллет:");
            foreach (var pallet in pallets)
            {
                Console.WriteLine($"\nПаллета ID: {pallet.Id}");
                Console.WriteLine($"  Вес: {pallet.Weight} кг");
                Console.WriteLine($"  Объем: {pallet.Volume:F2} м³");
                Console.WriteLine($"  Срок годности: {(pallet.ExpirationDate.HasValue ? pallet.ExpirationDate.Value.ToString("dd.MM.yyyy") : "-")}");
                Console.WriteLine($"  Коробок: {pallet.Boxes.Count}");

                for (int i = 0; i < pallet.Boxes.Count; i++)
                {
                    var box = pallet.Boxes[i];
                    Console.WriteLine($"    Коробка {i + 1}: Размеры {box.Width}x{box.Height}x{box.Depth}, Вес {box.Weight} кг, Срок годности: {box.EffectiveExpirationDate:dd.MM.yyyy}");
                }
            }
        }

        static void Main()
        {
            Console.WriteLine("Добро пожаловать в систему управления складом!");
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Посмотреть все паллеты и их содержимое");
            Console.WriteLine("2. Редактировать коробки (добавить, изменить или удалить)");
            Console.WriteLine("0. Выход");

            Console.Write("Ваш выбор: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    var pallets = GenerateSamplePallets();
                    DisplayPallets(pallets);
                    break;
                case "2":
                    Console.WriteLine("[Здесь будет логика редактирования коробок]");
                    break;
                case "0":
                    Console.WriteLine("Завершение работы...");
                    break;
                default:
                    Console.WriteLine("Неизвестный выбор. Попробуйте снова.");
                    break;
            }
        }
    }
}

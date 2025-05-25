using System;
using System.Collections.Generic;
using System.Linq;

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

        static void EditBoxes(List<Pallet> pallets)
        {
            Console.WriteLine("\nРедактирование коробок:");
            Console.WriteLine("1. Добавить коробку");
            Console.WriteLine("2. Удалить коробку");
            Console.WriteLine("0. Назад");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Выберите паллету (введите номер от 1 до " + pallets.Count + "):");
                    for (int i = 0; i < pallets.Count; i++)
                        Console.WriteLine($"{i + 1}. ID: {pallets[i].Id}");

                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= pallets.Count)
                    {
                        var pallet = pallets[index - 1];
                        var newBox = new Box();

                        Console.Write("Ширина: "); newBox.Width = double.Parse(Console.ReadLine());
                        Console.Write("Высота: "); newBox.Height = double.Parse(Console.ReadLine());
                        Console.Write("Глубина: "); newBox.Depth = double.Parse(Console.ReadLine());
                        Console.Write("Вес: "); newBox.Weight = double.Parse(Console.ReadLine());

                        Console.Write("Дата производства (гггг-мм-дд): ");
                        if (DateTime.TryParse(Console.ReadLine(), out var date))
                            newBox.ManufactureDate = date;

                        pallet.Boxes.Add(newBox);
                        Console.WriteLine("Коробка добавлена.");
                    }
                    else Console.WriteLine("Неверный выбор паллеты.");
                    break;

                case "2":
                    Console.WriteLine("Выберите паллету:");
                    for (int i = 0; i < pallets.Count; i++)
                        Console.WriteLine($"{i + 1}. ID: {pallets[i].Id}");

                    if (int.TryParse(Console.ReadLine(), out int delIndex) && delIndex > 0 && delIndex <= pallets.Count)
                    {
                        var pallet = pallets[delIndex - 1];
                        for (int i = 0; i < pallet.Boxes.Count; i++)
                            Console.WriteLine($"{i + 1}. Коробка {i + 1}, вес: {pallet.Boxes[i].Weight} кг");

                        Console.Write("Введите номер коробки для удаления: ");
                        if (int.TryParse(Console.ReadLine(), out int boxIndex) && boxIndex > 0 && boxIndex <= pallet.Boxes.Count)
                        {
                            pallet.Boxes.RemoveAt(boxIndex - 1);
                            Console.WriteLine("Коробка удалена.");
                        }
                        else Console.WriteLine("Неверный номер коробки.");
                    }
                    else Console.WriteLine("Неверный выбор паллеты.");
                    break;

                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }

        static void Main()
        {
            var pallets = GenerateSamplePallets();

            while (true)
            {
                Console.WriteLine("\nДобро пожаловать в систему управления складом!");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Посмотреть все паллеты и их содержимое");
                Console.WriteLine("2. Редактировать коробки (добавить или удалить)");
                Console.WriteLine("0. Выход");

                Console.Write("Ваш выбор: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        DisplayPallets(pallets);
                        break;
                    case "2":
                        EditBoxes(pallets);
                        break;
                    case "0":
                        Console.WriteLine("Завершение работы...");
                        return;
                    default:
                        Console.WriteLine("Неизвестный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace WarehouseApp
{
    class Program
    {
        static void DisplayPallets(List<Pallet> pallets)
        {
            Console.WriteLine("\nСписок паллет:");
            foreach (var pallet in pallets)
            {
                Console.WriteLine($"\nПаллета ID: {pallet.Id.ToString()[..8]}");
                Console.WriteLine($"  Вес: {pallet.Weight} кг");
                Console.WriteLine($"  Объем: {pallet.Volume:F2} м³");
                Console.WriteLine($"  Срок годности: {(pallet.ExpirationDate.HasValue ? pallet.ExpirationDate.Value.ToString("dd.MM.yyyy") : "-")}");
                Console.WriteLine($"  Коробок: {pallet.Boxes.Count}");

                for (int i = 0; i < pallet.Boxes.Count; i++)
                {
                    var box = pallet.Boxes[i];
                    Console.WriteLine($"    Коробка {i + 1} (ID: {box.Id.ToString()[..8]}): Размеры {box.Width}x{box.Height}x{box.Depth}, Вес {box.Weight} кг, Срок годности: {box.EffectiveExpirationDate:dd.MM.yyyy}");
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
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    AddBoxToPallet(pallets);
                    break;
                case "2":
                    RemoveBoxFromPallet(pallets);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }

        static void AddBoxToPallet(List<Pallet> pallets)
        {
            Console.WriteLine("Выберите паллету (введите номер от 1 до " + pallets.Count + "):");
            for (int i = 0; i < pallets.Count; i++)
                Console.WriteLine($"{i + 1}. ID: {pallets[i].Id.ToString()[..8]}");

            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= pallets.Count)
            {
                var pallet = pallets[index - 1];

                try
                {
                    Console.Write("Ширина: ");
                    if (!double.TryParse(Console.ReadLine(), out double width)) throw new Exception("Неверный формат ширины.");

                    Console.Write("Высота: ");
                    if (!double.TryParse(Console.ReadLine(), out double height)) throw new Exception("Неверный формат высоты.");

                    Console.Write("Глубина: ");
                    if (!double.TryParse(Console.ReadLine(), out double depth)) throw new Exception("Неверный формат глубины.");

                    Console.Write("Вес: ");
                    if (!double.TryParse(Console.ReadLine(), out double weight)) throw new Exception("Неверный формат веса.");

                    Console.Write("Дата производства (гггг-мм-дд): ");
                    string dateInput = Console.ReadLine() ?? "";
                    if (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", null, DateTimeStyles.None, out var manufactureDate))
                        throw new Exception("Неверный формат даты. Используйте формат гггг-мм-дд.");

                    DateTime expirationDate = manufactureDate.AddDays(100);
                    if (expirationDate < DateTime.Today.Date)
                        throw new Exception("Эта коробка уже просрочена и не может быть добавлена.");

                    if (width > pallet.Width || depth > pallet.Depth)
                        throw new Exception("Размеры коробки превышают размеры паллеты по ширине или глубине.");

                    var newBox = new Box
                    {
                        Width = width,
                        Height = height,
                        Depth = depth,
                        Weight = weight,
                        ManufactureDate = manufactureDate
                    };

                    pallet.Boxes.Add(newBox);
                    Console.WriteLine("Коробка добавлена.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            else Console.WriteLine("Неверный выбор паллеты.");
        }

        static void RemoveBoxFromPallet(List<Pallet> pallets)
        {
            Console.WriteLine("Выберите паллету:");
            for (int i = 0; i < pallets.Count; i++)
                Console.WriteLine($"{i + 1}. ID: {pallets[i].Id.ToString()[..8]}");

            if (int.TryParse(Console.ReadLine(), out int delIndex) && delIndex > 0 && delIndex <= pallets.Count)
            {
                var pallet = pallets[delIndex - 1];
                for (int i = 0; i < pallet.Boxes.Count; i++)
                    Console.WriteLine($"{i + 1}. Коробка ID: {pallet.Boxes[i].Id.ToString()[..8]}, вес: {pallet.Boxes[i].Weight} кг");

                Console.Write("Введите номер коробки для удаления: ");
                if (int.TryParse(Console.ReadLine(), out int boxIndex) && boxIndex > 0 && boxIndex <= pallet.Boxes.Count)
                {
                    pallet.Boxes.RemoveAt(boxIndex - 1);
                    Console.WriteLine("Коробка удалена.");
                }
                else Console.WriteLine("Неверный номер коробки.");
            }
            else Console.WriteLine("Неверный выбор паллеты.");
        }

        static void GroupAndDisplayPalletsByExpiration(List<Pallet> pallets)
        {
            Console.WriteLine("\nГруппировка паллет по сроку годности (по возрастанию), внутри — сортировка по весу:");

            var grouped = pallets
                .Where(p => p.ExpirationDate.HasValue)
                .GroupBy(p => p.ExpirationDate.Value.Date)
                .OrderBy(g => g.Key);

            foreach (var group in grouped)
            {
                Console.WriteLine($"\nСрок годности: {group.Key:dd.MM.yyyy}");
                foreach (var pallet in group.OrderBy(p => p.Weight))
                {
                    Console.WriteLine($"  Паллета ID: {pallet.Id.ToString()[..8]} — Вес: {pallet.Weight} кг");
                }
            }
        }

        static List<Pallet> GenerateSamplePallets()
        {
            var random = new Random();
            var pallets = new List<Pallet>();

            for (int i = 0; i < 15; i++)
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
                        ManufactureDate = DateTime.Today.AddDays(-random.Next(0, 100))
                    };
                    pallet.Boxes.Add(box);
                }

                pallets.Add(pallet);
            }

            return pallets;
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
                Console.WriteLine("3. Группировать паллеты по сроку годности и отсортировать");
                Console.WriteLine("0. Выход");

                Console.Write("Ваш выбор: ");
                string input = Console.ReadLine() ?? "";

                switch (input)
                {
                    case "1":
                        DisplayPallets(pallets);
                        break;
                    case "2":
                        EditBoxes(pallets);
                        break;
                    case "3":
                        GroupAndDisplayPalletsByExpiration(pallets);
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

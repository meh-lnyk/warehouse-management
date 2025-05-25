using System;
using System.Collections.Generic;

namespace WarehouseApp
{
    class Program
    {
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
                    Console.WriteLine("[Здесь будет логика вывода паллет]");
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

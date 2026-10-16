using System;
using Model;

namespace Lab1_AIS
{
    /// <summary>
    /// Консольное представление маркетплейса гномиков.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Читать целое число, повторяя запрос при ошибке.
        /// </summary>
        static int ReadInteger()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input == null) throw new OperationCanceledException();
                int value;
                if (int.TryParse(input, out value)) return value;
                Console.Write("Введите целое число: ");
            }
        }

        /// <summary>
        /// Читать цену, повторяя запрос при ошибке.
        /// </summary>
        static decimal ReadDecimal()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input == null) throw new OperationCanceledException();
                decimal value;
                if (decimal.TryParse(input, out value)) return value;
                Console.Write("Введите число (дробная часть — по настройкам Windows): ");
            }
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main(string[] args)
        {
            Logic logic = new Logic();

            logic.AddProduct("Мятная травка", "Лечебная травка", "Бурбуля", 10, 50);
            logic.AddProduct("Ромашковый отвар", "Отвары", "Мудрик", 25, 30);
            logic.AddProduct("Чабрец сушёный", "Лечебная травка", "Пискун", 8, 100);

            while (true)
            {
                if (!Console.IsOutputRedirected) Console.Clear();
                Console.WriteLine("=== МАРКЕТПЛЕЙС ГНОМИКОВ ===");
                Console.WriteLine("1. Добавить товар");
                Console.WriteLine("2. Показать все товары");
                Console.WriteLine("3. Изменить товар");
                Console.WriteLine("4. Удалить товар");
                Console.WriteLine("5. Группировка по типу");
                Console.WriteLine("6. Купить товар");
                Console.WriteLine("7. Скидка на товар");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");

                string choice = Console.ReadLine();

                if (choice == null) break;
                try
                {
                    if (choice == "1") AddProduct(logic);
                    else if (choice == "2") ShowAll(logic);
                    else if (choice == "3") UpdateProduct(logic);
                    else if (choice == "4") DeleteProduct(logic);
                    else if (choice == "5") ShowGrouped(logic);
                    else if (choice == "6") BuyProduct(logic);
                    else if (choice == "7") ApplyDiscount(logic);
                    else if (choice == "0") break;
                    else Console.WriteLine("Выберите пункт от 0 до 7.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                Console.WriteLine("\nНажмите Enter...");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Запросить у пользователя данные и добавить товар.
        /// </summary>
        static void AddProduct(Logic logic)
        {
            Console.Write("Название: ");
            string name = Console.ReadLine();

            Console.WriteLine("Выберите тип:");
            Console.WriteLine("  1 — Лечебная травка");
            Console.WriteLine("  2 — Волшебный порошочек");
            Console.WriteLine("  3 — Отвары");
            Console.Write("Ваш выбор: ");
            string typeChoice = Console.ReadLine();
            string type = TypeFromChoice(typeChoice);

            Console.Write("Гномик-продавец: ");
            string seller = Console.ReadLine();
            Console.Write("Цена: ");
            decimal price = ReadDecimal();
            Console.Write("Количество: ");
            int stock = ReadInteger();

            logic.AddProduct(name, type, seller, price, stock);
            Console.WriteLine("Товар добавлен!");
        }

        /// <summary>
        /// Преобразовать номер выбора в название типа.
        /// </summary>
        static string TypeFromChoice(string choice)
        {
            if (choice == "1") return "Лечебная травка";
            if (choice == "2") return "Волшебный порошочек";
            if (choice == "3") return "Отвары";
            throw new ArgumentException("Выберите тип от 1 до 3.");
        }

        /// <summary>
        /// Вывести список всех товаров.
        /// </summary>
        static void ShowAll(Logic logic)
        {
            Console.WriteLine(logic.GetAll());
        }

        /// <summary>
        /// Изменить товар по названию.
        /// </summary>
        static void UpdateProduct(Logic logic)
        {
            Console.Write("Имя товара для изменения: ");
            string oldName = Console.ReadLine();
            Console.Write("Новое название: ");
            string newName = Console.ReadLine();

            Console.WriteLine("Выберите новый тип:");
            Console.WriteLine("  1 — Лечебная травка");
            Console.WriteLine("  2 — Волшебный порошочек");
            Console.WriteLine("  3 — Отвары");
            Console.Write("Ваш выбор: ");
            string typeChoice = Console.ReadLine();
            string newType = TypeFromChoice(typeChoice);

            Console.Write("Новый гномик: ");
            string newSeller = Console.ReadLine();
            Console.Write("Новая цена: ");
            decimal newPrice = ReadDecimal();
            Console.Write("Новое количество: ");
            int newStock = ReadInteger();

            bool ok = logic.UpdateProduct(oldName, newName, newType,
                                          newSeller, newPrice, newStock);
            Console.WriteLine(ok ? "Обновлено" : "Товар не найден");
        }

        /// <summary>
        /// Удалить товар по названию.
        /// </summary>
        static void DeleteProduct(Logic logic)
        {
            Console.Write("Имя товара для удаления: ");
            string name = Console.ReadLine();
            bool ok = logic.DeleteProduct(name);
            Console.WriteLine(ok ? "Удалено" : "Товар не найден");
        }

        /// <summary>
        /// Показать товары, сгруппированные по типу.
        /// </summary>
        static void ShowGrouped(Logic logic)
        {
            Console.WriteLine(logic.GroupByType());
        }

        /// <summary>
        /// Купить одну единицу товара, уменьшив остаток.
        /// </summary>
        static void BuyProduct(Logic logic)
        {
            Console.Write("Имя товара: ");
            string name = Console.ReadLine();
            Console.WriteLine(logic.BuyProduct(name));
        }

        /// <summary>
        /// Применить скидку к товару.
        /// </summary>
        static void ApplyDiscount(Logic logic)
        {
            Console.Write("Имя товара: ");
            string name = Console.ReadLine();
            Console.Write("Скидка в % (1–99): ");
            int percent = ReadInteger();
            Console.WriteLine(logic.ApplyDiscount(name, percent));
        }
    }
}


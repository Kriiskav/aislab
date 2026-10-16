using System;
using System.Collections.Generic;


namespace Model
{
    /// <summary>
    /// Класс бизнес-логики маркетплейса гномиков.
    /// </summary>
    public class Logic
    {
        /// <summary>
        /// Проверить данные товара перед добавлением или изменением.
        /// </summary>
        private void CheckProduct(string name, string type, string seller, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Введите название товара.");
            if (type != "Лечебная травка" && type != "Волшебный порошочек" && type != "Отвары")
                throw new ArgumentException("Выберите тип товара из списка.");
            if (string.IsNullOrWhiteSpace(seller))
                throw new ArgumentException("Введите имя продавца.");
            if (price < 0)
                throw new ArgumentException("Цена не может быть отрицательной.");
            if (stock < 0)
                throw new ArgumentException("Остаток не может быть отрицательным.");
        }

        /// <summary>
        /// Получить названия товаров для списка в форме.
        /// </summary>
        public List<string> GetNames()
        {
            List<string> names = new List<string>();
            foreach (Product product in products)
                names.Add(product.Name);
            return names;
        }

        /// <summary>
        /// Список всех товаров.
        /// </summary>
        private List<Product> products = new List<Product>();

        /// <summary>
        /// Добавить новый товар.
        /// </summary>
        public void AddProduct(string name, string type, string seller,
                               decimal price, int stock)
        {
            CheckProduct(name, type, seller, price, stock);
            if (FindByName(name) != null)
                throw new ArgumentException("Товар с таким названием уже есть.");

            Product product = new Product()
            {
                Name = name.Trim(),
                Type = type.Trim(),
                SellerGnome = seller.Trim(),
                Price = price,
                Stock = stock
            };
            products.Add(product);
        }

        /// <summary>
        /// Получить строку со всеми товарами.
        /// </summary>
        public string GetAll()
        {
            string s = string.Empty;
            foreach (Product p in products)
            {
                s += p.Name + " | " + p.Type + " | гномик: " + p.SellerGnome +
                     " | цена: " + p.Price + " | остаток: " + p.Stock + "\n";
            }
            return s;
        }

        /// <summary>
        /// Найти товар по названию.
        /// </summary>
        public Product FindByName(string name)
        {
            foreach (Product p in products)
            {
                if (string.Equals(p.Name, (name ?? "").Trim(), StringComparison.OrdinalIgnoreCase))
                    return p;
            }
            return null;
        }

        /// <summary>
        /// Изменить данные товара.
        /// </summary>
        public bool UpdateProduct(string oldName, string newName, string newType,
                                  string newSeller, decimal newPrice, int newStock)
        {
            Product p = FindByName(oldName);
            if (p == null) return false;

            CheckProduct(newName, newType, newSeller, newPrice, newStock);
            Product existing = FindByName(newName);
            if (existing != null && existing != p)
                throw new ArgumentException("Товар с таким названием уже есть.");

            p.Name = newName.Trim();
            p.Type = newType.Trim();
            p.SellerGnome = newSeller.Trim();
            p.Price = newPrice;
            p.Stock = newStock;
            return true;
        }

        /// <summary>
        /// Удалить товар по названию.
        /// </summary>
        public bool DeleteProduct(string name)
        {
            Product p = FindByName(name);
            if (p == null) return false;

            products.Remove(p);
            return true;
        }

        /// <summary>
        /// Сгруппировать товары по типу.
        /// </summary>
        public string GroupByType()
        {
            string s = string.Empty;

            List<string> types = new List<string>();
            foreach (Product p in products)
            {
                if (!types.Contains(p.Type))
                    types.Add(p.Type);
            }

            foreach (string t in types)
            {
                s += "=== " + t + " ===\n";
                foreach (Product p in products)
                {
                    if (p.Type == t)
                    {
                        s += "  " + p.Name + " (гномик: " + p.SellerGnome +
                             ", цена: " + p.Price + ")\n";
                    }
                }
                s += "\n";
            }

            return s;
        }

        /// <summary>
        /// Купить одну единицу товара, уменьшив остаток на складе.
        /// </summary>
        public string BuyProduct(string name)
        {
            Product p = FindByName(name);
            if (p == null) return "Товар не найден";

            if (p.Stock == 0) return "Товара нет в наличии";
            p.Stock--;
            return "Куплена одна единица товара \"" + p.Name + "\" у гномика " + p.SellerGnome +
                   " за " + p.Price + " з.м.";
        }

        /// <summary>
        /// Применить скидку к товару.
        /// </summary>
        public string ApplyDiscount(string name, int percent)
        {
            Product p = FindByName(name);
            if (p == null) return "Товар не найден";
            if (percent <= 0 || percent >= 100)
                return "Скидка должна быть от 1 до 99%";

            decimal oldPrice = p.Price;
            p.Price = Math.Round(p.Price - p.Price * (percent / 100m), 2);

            return "Скидка " + percent + "% на \"" + p.Name + "\": " +
                   oldPrice + " → " + p.Price + " з.м.";
        }
    }
}


namespace Model
{
    public interface IDomainObject
    {
        int Id { get; set; }
    }

    /// <summary>
    /// Сущность "Товар" — травка или отвар, который продаёт гномик.
    /// </summary>
    public class Product : IDomainObject
    {
        /// <summary>
        /// Уникальный идентификатор (первичный ключ).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название товара.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип товара: "Травка" или "Отвар".
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Имя гномика-продавца.
        /// </summary>
        public string SellerGnome { get; set; }

        /// <summary>
        /// Цена в золотых монетах.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Количество на складе.
        /// </summary>
        public int Stock { get; set; }
    }
}
namespace Model
{
    /// <summary>Чтение цены.</summary>
    public static class InputParser
    {
        /// <summary>Прочитать десятичное число.</summary>
        public static bool TryReadPrice(string text, out decimal value)
        {
            value = 0;
            if (text == null) return false;
            return decimal.TryParse(text.Trim().Replace(',', '.'),
                System.Globalization.NumberStyles.AllowLeadingSign |
                System.Globalization.NumberStyles.AllowDecimalPoint,
                System.Globalization.CultureInfo.InvariantCulture, out value);
        }
    }
}

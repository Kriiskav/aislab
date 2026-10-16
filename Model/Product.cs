namespace Model
{
    /// <summary>
    /// Сущность "Товар" — травка или отвар, который продаёт гномик.
    /// </summary>
    public class Product
    {
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

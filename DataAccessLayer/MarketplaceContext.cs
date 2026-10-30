using System.Data.Entity;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Контекст базы данных для Entity Framework, представляющий сессию работы с БД.
    /// </summary>
    public class MarketplaceContext : DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр контекста, используя строку подключения "MarketplaceDb".
        /// </summary>
        public MarketplaceContext() : base("name=MarketplaceDb") { }

        /// <summary>
        /// Набор сущностей "Товар".
        /// </summary>
        public DbSet<Product> Products { get; set; }
    }
}

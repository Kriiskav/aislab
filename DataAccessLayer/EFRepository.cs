using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Реализация репозитория с использованием Entity Framework.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public class EFRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly MarketplaceContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр репозитория EF.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public EFRepository(MarketplaceContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавить сущность в БД.
        /// </summary>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удалить сущность из БД.
        /// </summary>
        public void Delete(T entity)
        {
            T stored = _context.Set<T>().Find(entity.Id);
            if (stored == null) return;
            _context.Set<T>().Remove(stored);
            _context.SaveChanges();
        }

        /// <summary>
        /// Получить все сущности из БД.
        /// </summary>
        public IEnumerable<T> ReadAll()
        {
            return _context.Set<T>().AsNoTracking().OrderBy(p => p.Id).ToList();
        }

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        public T ReadById(int id)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Обновить сущность в БД.
        /// </summary>
        public void Update(T entity)
        {
            T stored = _context.Set<T>().Find(entity.Id);
            if (stored == null) return;
            _context.Entry(stored).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }
    }
}

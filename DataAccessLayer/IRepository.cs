using System.Collections.Generic;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс репозитория, обеспечивающий основные CRUD-операции.
    /// </summary>
    /// <typeparam name="T">Тип сущности, реализующий <see cref="IDomainObject"/>.</typeparam>
    public interface IRepository<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Добавить новую сущность.
        /// </summary>
        void Add(T entity);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// Получить все сущности.
        /// </summary>
        IEnumerable<T> ReadAll();

        /// <summary>
        /// Получить сущность по её идентификатору.
        /// </summary>
        T ReadById(int id);

        /// <summary>
        /// Обновить существующую сущность.
        /// </summary>
        void Update(T entity);
    }
}

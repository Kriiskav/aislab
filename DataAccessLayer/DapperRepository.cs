using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Реализация репозитория с использованием микро-ORM Dapper.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Строка подключения к базе данных.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Имя таблицы в базе данных.
        /// </summary>
        private readonly string _tableName;

        /// <summary>
        /// Инициализирует новый экземпляр репозитория Dapper.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        public DapperRepository(string connectionString)
        {
            _connectionString = connectionString;
            _tableName = typeof(T).Name + "s";
        }

        /// <summary>
        /// Добавить сущность в БД.
        /// </summary>
        public void Add(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = $"INSERT INTO {_tableName} (Name, Type, SellerGnome, Price, Stock) " +
                             "VALUES (@Name, @Type, @SellerGnome, @Price, @Stock); SELECT CAST(SCOPE_IDENTITY() AS int);";
                entity.Id = connection.QuerySingle<int>(sql, entity);
            }
        }

        /// <summary>
        /// Удалить сущность из БД.
        /// </summary>
        public void Delete(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = $"DELETE FROM {_tableName} WHERE Id = @Id";
                connection.Execute(sql, new { Id = entity.Id });
            }
        }

        /// <summary>
        /// Получить все сущности из БД.
        /// </summary>
        public IEnumerable<T> ReadAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = $"SELECT * FROM {_tableName}";
                return connection.Query<T>(sql).ToList();
            }
        }

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        public T ReadById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = $"SELECT * FROM {_tableName} WHERE Id = @Id";
                return connection.QueryFirstOrDefault<T>(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Обновить сущность в БД.
        /// </summary>
        public void Update(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = $"UPDATE {_tableName} SET Name = @Name, Type = @Type, " +
                             "SellerGnome = @SellerGnome, Price = @Price, Stock = @Stock " +
                             "WHERE Id = @Id";
                connection.Execute(sql, entity);
            }
        }
    }
}

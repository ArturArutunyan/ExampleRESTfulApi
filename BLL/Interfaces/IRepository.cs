using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    /// <summary>
    /// Контракт CRUD репозитория сущности
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип ключа сущности</typeparam>
    public interface IRepository<TEntity, TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Создать запись
        /// </summary>
        /// <param name="entity">
        /// Экземпляр класса
        /// </param>
        Task Create(TEntity entity);
        /// <summary>
        /// Получить запись по ключу
        /// </summary>
        /// <param name="id">
        /// Ключ записи в базе
        /// </param>
        /// <returns>
        /// Запись по ключу
        /// </returns>
        Task<TEntity> GetWhere(TKey key);
        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns>
        /// Все записи в базе
        /// </returns>
        Task<IEnumerable<TEntity>> GetAll();
        /// <summary>
        /// Обновить запись
        /// </summary>
        /// <param name="entity">
        /// Экземпляр класса
        /// </param>
        Task Update(TEntity entity);
        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="id">
        /// Ключ записи в базе
        /// </param>
        Task Delete(TKey key);
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        /// Получить запись
        /// </summary>
        /// <param name="expression">
        /// Предикат
        /// </param>
        /// <returns>
        /// Запись
        /// </returns>
        Task<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression);
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
        /// <param name="entity">
        /// Сущность в базе
        /// </param>
        Task Delete(TEntity entity);
    }
}

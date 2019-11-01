using System.Collections.Generic;

namespace BLL.Entities.Responses
{
    /// <summary>
    /// Обертка для массива данных. Отображает информацию о текущей странице и содержит стредства навигации.
    /// </summary>
    /// <typeparam name="TEntity">Используемая сущность</typeparam>
    public class PagedResponse<TEntity>
    {
        public IEnumerable<TEntity> Data { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }


        public PagedResponse() { }
        public PagedResponse(IEnumerable<TEntity> data)
        {
            Data = data;
        }
    }
}

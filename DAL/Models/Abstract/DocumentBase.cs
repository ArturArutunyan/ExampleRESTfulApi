using System.Collections.Generic;

namespace DAL.Models
{
    /// <summary>
    /// Абстрактный класс реализующий базовый функционал документа
    /// </summary>
    /// <typeparam name="TDocumentEntityRole">Класс связующий тип конкретного документа с ролью (many-to-many)</typeparam>
    public abstract class DocumentBase<TDocumentEntityRole> where TDocumentEntityRole : class, new()
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DocumentName { get; set; }

        public IEnumerable<TDocumentEntityRole> DocumentRoles{ get; set; }
    }
}

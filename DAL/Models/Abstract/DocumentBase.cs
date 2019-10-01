using System.Collections.Generic;

namespace DAL.Models
{
    /// <summary>
    /// Абстрактный класс реализующий базовый функционал документа
    /// </summary>
    public abstract class DocumentBase
    { 
        public int Id { get; set; }
        public string Title { get; set; }
        public string DocumentName { get; set; }
    }
}

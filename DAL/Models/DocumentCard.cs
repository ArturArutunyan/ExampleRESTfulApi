using System.Collections.Generic;

namespace DAL.Models
{
    public class DocumentCard : DocumentCardBase
    {
        public List<DocumentCardRoles> DocumentCardRoles { get; set; }
    }
}
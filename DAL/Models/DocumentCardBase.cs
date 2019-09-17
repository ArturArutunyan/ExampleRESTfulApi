using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public abstract class DocumentCardBase
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    /// <summary>
    /// Абстрактный класс реализующий базовый функционал документа
    /// </summary>
    public abstract class DocumentBase
    { 
        [Key]
        public Guid Guid { get; set; }

        [Required(ErrorMessage = "Поле Title обязательно для заполнения")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле DocumentName обязательно для заполнения")]
        public string DocumentName { get; set; }
    }
}

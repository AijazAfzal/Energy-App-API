using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    public class LanguageList
    {
        public IList<Language> Languages { get; set; } 
    }
    public class Language
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }  

        public string Name { get; set; } 
    }
}

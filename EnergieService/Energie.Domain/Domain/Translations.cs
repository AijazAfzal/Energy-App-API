using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class Translations
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string? NameKey { get; private set; }
        [Required]
        public string? Value { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class HelpCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set;}
        public string ImageUrl { get; set; }
        //public string? Delete { get; set; }

        public HelpCategory SetHelpCategory(string name,string description)
        {
            Name = name;
            Description = description;
            return this; 
            
        }
    }
}

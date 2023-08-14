using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class Category
    {
        [Key]
        public int Id { get; private set; }
        [Required] 
        public string Name { get; private set;}
        [Required]
        public string Description { get; private set; }
        [Required]
        public string CreatedBy { get; private set; }
        [Required]
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set;}
        public Category SetCategory(string name, string description)
        {
            Name = name;
            Description = description;
            CreatedBy = "Admin";
            CreatedOn = DateTime.Now;
            return this;
        }
        public Category UpdateCategory(string name, string description)
        {
            Name = name;
            Description = description;
            CreatedBy = "Admin";
            UpdatedOn = DateTime.Now;
            return this;
        }

    }
}

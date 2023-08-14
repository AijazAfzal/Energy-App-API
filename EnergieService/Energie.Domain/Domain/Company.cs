using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Energie.Domain.Domain
{
    [Index(nameof(Name), IsUnique = true)]
    public class Company
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; } 
        public string CreatedBy { get; private set; } 
        public DateTime CreatedOn { get; private set; }

        public string? ImageUrl { get; private set; }
        private Company(string name)
        {
            Name = name;
            CreatedBy = "SuperAdmin";
            CreatedOn = DateTime.Now;
        }
        public static Company Create(string companyName)
        {
            return new(companyName);
        }
    }
}

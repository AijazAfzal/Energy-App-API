using System.ComponentModel.DataAnnotations;

namespace Energie.Model.Request
{
    public record CreateCompanyRequest
    {
        [Required]
        public string CompanyName { get; set; }
    }
}

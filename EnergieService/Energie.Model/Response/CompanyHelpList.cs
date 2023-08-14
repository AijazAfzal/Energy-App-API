using System.ComponentModel.DataAnnotations;

namespace Energie.Model.Response
{
    public class CompanyHelpList
    {
        public List<CompanyHelp> CompanyHelpTips { get; set; }
    }
    public class CompanyHelp
    {
        [Key]
        public int Id { get; set; }
        public int HelpCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string OwnContribution { get; set; }
        public string Requestvia { get; set; }
        [Required]
        public string Conditions { get; set; }
    }
}

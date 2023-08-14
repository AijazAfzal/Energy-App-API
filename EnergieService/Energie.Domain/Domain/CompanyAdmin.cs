using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace Energie.Domain.Domain
{
    //Db Model
    [Index(nameof(Email), IsUnique = true)]
    public class CompanyAdmin
    {
        [Key]
        [Required]
        public int Id { get; private set; }
       
        [Required]
        [StringLength(50)]
        public string UserName { get; private set; }
        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; private set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; private set; }
        [Required]
        [StringLength(50)]
        public DateTime CreatedOn { get; private set; }

        [Required]
        [Display(Name = "Company")]
        public virtual int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }
        public CompanyAdmin SetCompanyAdmin(
             int companyName
            , string userName
            , string email
            )
        {

            CompanyId = companyName;
            UserName = userName;
            Email = email;
            CreatedBy = "SuperAdmin";
            CreatedOn = DateTime.Now;
            return this;
        }
    }

    //B2C User Model
    public class AddB2CCompanyAdmin
    {
        [Required]
        public int CompanyId { get; private set; }
        [Required]
        public string CompanyName { get; private set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; private set; }
        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; private set; }
        [Required]
        [StringLength(50)]
        public string? Password { get; private set; }
        

        public AddB2CCompanyAdmin SetCompanyAdmin(int companyId
            , string companyName
            , string userName
            , string email
            )
        {
            CompanyId = companyId;
            CompanyName = companyName;
            UserName = userName;
            Email = email;
            Password = GenerateNewPassword(4, 8, 6);
            return this;
        }
        public static string GenerateNewPassword(int lowercase, int uppercase, int numerics)
        {
            string lowers = "abcdefghijklmnopqrstuvwxyz";
            string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string number = "0123456789";

            Random random = new Random();

            string generated = "!";
            for (int i = 1; i <= lowercase; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    lowers[random.Next(lowers.Length - 1)].ToString()
                );

            for (int i = 1; i <= uppercase; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    uppers[random.Next(uppers.Length - 1)].ToString()
                );

            for (int i = 1; i <= numerics; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    number[random.Next(number.Length - 1)].ToString()
                );

            return generated.Replace("!", string.Empty);
        }

    }
}

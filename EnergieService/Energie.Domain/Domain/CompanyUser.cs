using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Energie.Domain.Domain
{
    //Db model
    [Index(nameof(Email), IsUnique = true)]
    public class CompanyUser
    {
        [Key]
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        [Display(Name = "Department")]
        public virtual int? DepartmentID { get; private set; } 
        [ForeignKey("DepartmentID")]
        public virtual Department? Department { get; set; }

        public virtual int? LanguageID { get; private set; } = 1;  
        [ForeignKey("LanguageID")]
        public virtual Language? Language { get; set; }

        public bool Is_Notification  { get; set; } = false;  

        public Boolean ShowOnboarding { get; private set; } 
        public DateTime CreatedOn { get; private set; }
        public CompanyUser SetCompanyUser(string name, string email, int departmentId)
        {
            UserName = name;
            Email = email;
            DepartmentID = departmentId;
            ShowOnboarding = true;
            CreatedOn = DateTime.Now;
            return this;
        }
        public CompanyUser UpdateCompanyUser(bool showOnboarding)
        {
            ShowOnboarding = showOnboarding; 
            return this;
        }

        public CompanyUser UpdateCompanyUserLanguage(int languageId)
        {
            LanguageID = languageId; 
            return this;  
            
        }

        public CompanyUser SetCompanyUserForUnitTest(int id, string name, string email, int departmentId,int languageID)
        { 
            Id = id;
            UserName = name;
            Email = email;
            DepartmentID = departmentId; 
            LanguageID = languageID; 
            ShowOnboarding = true;
            CreatedOn = DateTime.Now; 
            return this;
        }

    }
    // B2c Modal 
    public class B2CCompanyUser
    {
        [Key]
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        [Display(Name = "Department")]
        public string Password { get; private set; }
        public virtual int? DepartmentID { get; private set; }
        [ForeignKey("DepartmentID")]
        public virtual Department? Department { get; set; }

        public B2CCompanyUser SetCompanyUser(string name, string email, int departmentId)
        {
            UserName = name;
            Email = email;
            DepartmentID = departmentId;
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

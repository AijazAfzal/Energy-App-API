using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class Language
    {
        [Key]
        public int Id { get; private set; }

        public string Code { get; private set; } 

        public string Name { get; private set; }


        public Language SetLanguage(string code, string name)
        {
            Code = code;
            Name = name;
            return this;
        } 
    }

    
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    public class AddFeedbackRequest
    {
        [Required]
        [Range(1,5)]
        public int Rating { get;set; } 

        public string? description { get; set; }  
      
    }
}

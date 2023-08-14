using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class Feedback
    {
        public int ID {get; private set;}
        [Required]
        [Range(1,5)]
        public int Rating {get; private set;}
        public string? Description { get; private set;} 
        public DateTime FeedBackDate { get; private set;}
        [Required]
        [ForeignKey("CompanyUser")]
        [Display(Name = "CompanyUser")]
        public int CompanyUserId {get;private set;}
        public virtual CompanyUser? CompanyUser {get; set;}
        public Feedback SetFeedback(int rating,string description, int userid) 
        {
            Rating = rating;
            Description = description;
            FeedBackDate = DateTime.Now; 
            CompanyUserId = userid;
            return this;

        }

        public Feedback UpdateFeedback(int rating , string description)
        {
            Rating = rating;
            Description = description; 
            FeedBackDate = DateTime.Now;    
            return this; 
        }

    }
}

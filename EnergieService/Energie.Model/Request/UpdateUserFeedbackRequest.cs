using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    public class UpdateUserFeedbackRequest
    {
        public int Rating { get; set; }

        public string? description { get; set; } 
    }
}

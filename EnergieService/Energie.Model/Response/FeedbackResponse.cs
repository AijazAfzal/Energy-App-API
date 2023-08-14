using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Response
{
    public class FeedbackResponse
    {
        public int Rating { get; set; }

        public string?  description { get; set; } 
        public string Message { get;set; }

    }
}

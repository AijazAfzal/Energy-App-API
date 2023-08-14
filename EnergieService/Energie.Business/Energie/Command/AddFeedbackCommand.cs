using Energie.Model.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.Command
{
    public class AddFeedbackCommand : IRequest<FeedbackResponse>
    {

        public int Rating { get; set; }
        public string? description { get; set; } 
        public string UserEmail { get; set; }
    }
}

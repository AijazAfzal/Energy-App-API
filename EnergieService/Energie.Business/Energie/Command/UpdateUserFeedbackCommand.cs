using Energie.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.Command
{
    public class UpdateUserFeedbackCommand : IRequest<ResponseMessage>
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string? description { get; set; }  

        public string  UserEmail { get; set; } 
    }
}

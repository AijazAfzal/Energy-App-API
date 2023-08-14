using Energie.Model.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.Query.NotificationFlag
{
    public record NotificationResponseQuery : IRequest<NotificationResponseList>
    {
        public string UserEmail { get; set; }

        public string Language { get;set; }
    }
}

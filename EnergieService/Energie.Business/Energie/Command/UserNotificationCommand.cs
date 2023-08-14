using Energie.Model;
using Energie.Model.Response;
using MediatR;

namespace Energie.Business.Energie.Command
{
    public class UserNotificationCommand : IRequest<NotificationResponseList>
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
    }
}

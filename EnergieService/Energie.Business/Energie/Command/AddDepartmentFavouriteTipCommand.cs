using Energie.Model;
using MediatR;

namespace Energie.Business.Energie.Command
{
    public class AddDepartmentFavouriteTipCommand : IRequest<ResponseMessage>
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
    }
}

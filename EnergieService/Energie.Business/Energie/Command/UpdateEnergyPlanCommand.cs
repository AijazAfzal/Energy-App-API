using Energie.Model;
using MediatR;

namespace Energie.Business.Energie.Command
{
    public class UpdateEnergyPlanCommand : IRequest<ResponseMessage>
    {
        public int Id { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }  
        public bool IsReminder { get; set; }

        public DateTime Updateddate { get; set; }
        public string UserEmail { get; set; }
    }
}

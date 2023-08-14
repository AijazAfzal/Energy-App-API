using Energie.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Energie.Business.Energie.Command
{
    public class AddMonthlyEnergyScoreCommand : IRequest<ResponseMessage>

    {
        [Required(ErrorMessage = "EnergyScore Required")]
        [Range(1, 10)]
        public int EnergyScore { get; set; }
    }
}

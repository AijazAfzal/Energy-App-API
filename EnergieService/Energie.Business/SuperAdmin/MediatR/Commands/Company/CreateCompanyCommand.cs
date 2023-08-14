using Energie.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Energie.Business.SuperAdmin.MediatR.Commands.Company
{
    public record CreateCompanyCommand : IRequest<ApiResponse>
    {
      
        public string CompanyName { get; set; } 
    }
}

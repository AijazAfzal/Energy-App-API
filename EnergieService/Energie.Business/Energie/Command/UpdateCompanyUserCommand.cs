using Energie.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.Command
{
    public class UpdateCompanyUserLanguageCommand : IRequest<ResponseMessage>
    { 

        public int LanguageId { get; set; } 

        public string UserEmail { get; set; }  


    }
}

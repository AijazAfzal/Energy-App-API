using Energie.Business.Energie.Query;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Energie.Model.Request;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.QueryHandler
{
    public class GetUserDepartmentTipListQueryHandler : IRequestHandler<GetUserDepartmentTipListQuery, UserDepartmentTipList>
    {
        private readonly IUserDepartmentTipRepository _userDepartmentTipRepository;
        private readonly ILogger<GetUserDepartmentTipListQueryHandler> _logger;
        private readonly ICompanyUserRepository _companyUserRepository;
        public GetUserDepartmentTipListQueryHandler(IUserDepartmentTipRepository userDepartmentTipRepository,
             ILogger<GetUserDepartmentTipListQueryHandler> logger,
             ICompanyUserRepository companyUserRepository)
        {
            _userDepartmentTipRepository = userDepartmentTipRepository;
            _logger = logger;
            _companyUserRepository = companyUserRepository; 

        }
        public async Task<UserDepartmentTipList> Handle(GetUserDepartmentTipListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var message = new ResponseMessage();
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var tiplist= await _userDepartmentTipRepository.GetUserDepartmentTipListAsync((int)user.DepartmentID); 
                var list = tiplist.Select
                                   (x => new Model.Request.UserDepartmentTip
                                   {
                                       Id = x.Id,
                                       categoryId = x.EnergyAnalysisQuestionsId,
                                       SourceId = (int)x.EnergyAnalysisQuestions.EnergyAnalysisID,
                                       Name = x.EnergyAnalysisQuestions.Name,
                                       Description = x.Description
                                   }).ToList() ;
                return new UserDepartmentTipList { userDepartmentTips = list }; 
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error Occured In {{0}}", nameof(GetUserDepartmentTipListQueryHandler));
                _logger.LogError(ex, $"fout opgetreden in {{0}}", nameof(GetUserDepartmentTipListQueryHandler)); 
                throw;
            }
            



        }
    }
}

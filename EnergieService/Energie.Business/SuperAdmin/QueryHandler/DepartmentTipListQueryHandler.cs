using Energie.Business.SuperAdmin.Query;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.SuperAdmin.QueryHandler
{
    public class DepartmentTipListQueryHandler : IRequestHandler<DepartmentTipListQuery, DepartmentTipList>
    {
        private readonly IEnergyTipRepository _energyTipRepository;
        private readonly ILogger<DepartmentTipListQueryHandler> _logger;
        public DepartmentTipListQueryHandler(IEnergyTipRepository energyTipRepository
            , ILogger<DepartmentTipListQueryHandler> logger)
        {
            _energyTipRepository = energyTipRepository;
            _logger = logger;
        }

        public async Task<DepartmentTipList> Handle(DepartmentTipListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var departmentTipList = await _energyTipRepository.GetDepatrmentTipListAsync();
                var departmentTipLists = departmentTipList.Select
                                    (x => new Model.Request.DepartmentTip
                                    {
                                        Id = x.ID,
                                        EnergyAnalysisId = x.EnergyAnalysisQuestions.EnergyAnalysis.Id,
                                        EnergyAnalysisQuestionId = (int)x.EnergyAnalysisQuestionsId,
                                        Name = x.Name,
                                        Description = x.Description,
                                    }).ToList();
                return new DepartmentTipList { DepartmentTips = departmentTipLists };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error occured in {{0}}", nameof(DepartmentTipListQueryHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(DepartmentTipListQueryHandler));
                throw;
            }
        }
    }
}

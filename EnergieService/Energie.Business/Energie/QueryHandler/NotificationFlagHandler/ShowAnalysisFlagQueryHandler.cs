using Energie.Business.Energie.Query.NotificationFlag;
using Energie.Business.SuperAdmin.Helper;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.QueryHandler.NotificationFlagHandler
{
    public class ShowAnalysisFlagQueryHandler : IRequestHandler<ShowAnalysisFlagQuery, ShowAnalysisFlagResponse>
    {
        private readonly ILogger<ShowAnalysisFlagQueryHandler> _logger;
        private readonly IFlagRepository _flagRepository;
        public ShowAnalysisFlagQueryHandler(ILogger<ShowAnalysisFlagQueryHandler> logger
            , IFlagRepository flagRepository)
        {
            _logger = logger;
            _flagRepository = flagRepository;
        }

        public async Task<ShowAnalysisFlagResponse> Handle(ShowAnalysisFlagQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var showAnalysisFlagRequest = new ShowAnalysisFlagResponse();
                var analsisflag = await _flagRepository.GetEnergyAnalysisFlagAsync(request.UserEmail);
                if (analsisflag == null)
                {
                    showAnalysisFlagRequest.ShowAnalysisFlag = true;
                    return showAnalysisFlagRequest;
                }
                return showAnalysisFlagRequest;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {{0}}");
                throw;
            }

        }
    }
}

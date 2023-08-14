using Energie.Business.SuperAdmin.Query;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.SuperAdmin.QueryHandler
{
    public class GetTipByIdQueryHandler : IRequestHandler<GetTipByIdQuery, EnergyTip>
    {
        private readonly ILogger<GetTipByIdQueryHandler> _logger;
        private readonly IEnergyTipRepository _energyTipRepository;
        public GetTipByIdQueryHandler(ILogger<GetTipByIdQueryHandler> logger
            , IEnergyTipRepository energyTipRepository)
        {
            _logger = logger;
            _energyTipRepository = energyTipRepository;
        }
        public async Task<EnergyTip> Handle(GetTipByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var energytip = await _energyTipRepository.GetEnergyTipByIdAsync(request.id);
                var customizeEnergytip = new EnergyTip
                {
                    Id = energytip.Id,
                    Name = energytip.Name,
                    Description = energytip.Description,
                    CreatedDate = energytip.CreatedOn.Date
                };
                return customizeEnergytip;
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"Error occured in {{0}}", nameof(GetTipByIdQueryHandler));
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(GetTipByIdQueryHandler));
                throw;
            }
        }
    }
}

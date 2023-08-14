using Energie.Business.Energie.Command;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class AddLikeTipCommandHandler : IRequestHandler<AddLikeTipCommand, ResponseMessage>
    {
        private readonly ILikeTipRepository _likeTipRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly ILogger<AddLikeTipCommandHandler> _logger;
        public AddLikeTipCommandHandler(ILikeTipRepository likeTipRepository
            , ICompanyUserRepository companyUserRepository
            , ILogger<AddLikeTipCommandHandler> logger)
        {
            _companyUserRepository = companyUserRepository;
            _likeTipRepository = likeTipRepository;
            _logger = logger;
        }
        public async Task<ResponseMessage> Handle(AddLikeTipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new ResponseMessage();
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                if (user == null)
                    return default;

                var departmentTip = await _likeTipRepository.GetLikeTipAsync(user.Id, request.Id);
                if (departmentTip != null)
                {
                    await _likeTipRepository.RemoveLikeTipAsync(user.Id, request.Id);
                    response.IsSuccess = true;
                    response.Id = departmentTip.Id;
                    return response;
                }
                var settip = new LikeTip().SetLikeTip(user.Id, request.Id);
                await _likeTipRepository.AddLikeTipAsync(settip);
                response.IsSuccess = true;
                response.Id = settip.Id;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error occured {{0}}", nameof(AddLikeTipCommandHandler));
            
                throw;
            }
        }
    }
}

using Energie.Business.Energie.Command;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class DeleteDepartmentTipbyUserCommandHandler : IRequestHandler<DeleteDepartmentTipbyUserCommand, ResponseMessage>
    {
        private readonly IUserDepartmentTipRepository _userDepartmentTipRepository;
        private readonly ILogger<DeleteDepartmentTipbyUserCommandHandler> _logger;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public DeleteDepartmentTipbyUserCommandHandler(IUserDepartmentTipRepository userDepartmentTipRepository
            , ILogger<DeleteDepartmentTipbyUserCommandHandler> logger
            , ICompanyUserRepository companyUserRepository
            , IStringLocalizer<Resources.Resources> localizer)

        {
            _userDepartmentTipRepository = userDepartmentTipRepository;
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _localizer = localizer;

        }
        public async Task<ResponseMessage> Handle(DeleteDepartmentTipbyUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var responseMessage = new ResponseMessage();
                await _userDepartmentTipRepository.DeleteUserDepartmentTipAsync(request.Id);
                responseMessage.IsSuccess = true;
                responseMessage.Id = request.Id;
                responseMessage.Message = _localizer["Department_tip_removed"].Value; 
                return responseMessage;

            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured in {{0}}", nameof(DeleteDepartmentTipbyUserCommandHandler));
                throw;
            }
        }
    }
}

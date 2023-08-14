using Energie.Business.Energie.Command;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class AddDepartmentTipbyUserCommandHandler : IRequestHandler<AddDepartmentTipbyUserCommand, ResponseMessage>
    {
        private readonly IUserDepartmentTipRepository _userDepartmentTipRepository;
        private readonly ILogger<AddDepartmentTipbyUserCommandHandler> _logger;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public AddDepartmentTipbyUserCommandHandler(IUserDepartmentTipRepository userDepartmentTipRepository,
            ILogger<AddDepartmentTipbyUserCommandHandler> logger,
            ICompanyUserRepository companyUserRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _userDepartmentTipRepository = userDepartmentTipRepository;
            _logger = logger;
            _companyUserRepository = companyUserRepository;
            _localizer = localizer;
        }
        public async Task<ResponseMessage> Handle(AddDepartmentTipbyUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responseMessage = new ResponseMessage();
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var DepartmentTipbyUser = new UserDepartmentTip().SetUserDepartmentTip(request.categoryId, request.Description, (int)user.Id);
                await _userDepartmentTipRepository.AddUserDepartmentTipAsync(DepartmentTipbyUser);
                responseMessage.IsSuccess = true;              
                responseMessage.Message = _localizer["Department_tip_with_DepartmentTipbyUserID_is_added_by_the_user", DepartmentTipbyUser.Id].Value;
                responseMessage.Id = DepartmentTipbyUser.Id;
                return responseMessage;
            }
            catch (Exception ex)
            {              
                _logger.LogError(ex, $"Er is een fout opgetreden in {{0}}", nameof(AddDepartmentTipbyUserCommandHandler));
                throw; 
            }


        }
    }
}

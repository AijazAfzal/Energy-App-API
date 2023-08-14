using Energie.Business.Energie.Command;
using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Energie.Business.Energie.CommandHandler
{
    public class UpdateDepartmentTipbyUserCommandHandler : IRequestHandler<UpdateDepartmentTipbyUserCommand, ResponseMessage>
    {
        private readonly ILogger<UpdateDepartmentTipbyUserCommandHandler> _logger;
        private readonly IUserDepartmentTipRepository _userDepartmentTipRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public UpdateDepartmentTipbyUserCommandHandler(ILogger<UpdateDepartmentTipbyUserCommandHandler> logger, IUserDepartmentTipRepository userDepartmentTipRepository, IStringLocalizer<Resources.Resources> localizer)
        {
            _logger = logger;
            _userDepartmentTipRepository = userDepartmentTipRepository;
            _localizer = localizer;
        }

        public async Task<ResponseMessage> Handle(UpdateDepartmentTipbyUserCommand request, CancellationToken cancellationToken)
        {
            ResponseMessage message = new();
            var tiptobeupdated = await _userDepartmentTipRepository.GetUserDepartmentTipbyIdAsync(request.Id);
            if (tiptobeupdated != null)
            {
                tiptobeupdated.UpdateDepartmentTip(request.Description);
                await _userDepartmentTipRepository.UpdateUserDepartmentTipAsync(tiptobeupdated);
                message.IsSuccess = true;
                message.Id = tiptobeupdated.Id;     
                message.Message = _localizer["User_departmenttip_with_id_updated", tiptobeupdated.Id].Value;
                return message;
            }
            else
            {
                message.IsSuccess = false;
                message.Message = _localizer["Something_failed"].Value;
                return message;
            }

        }
    }
}

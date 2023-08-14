using Energie.Business.Energie.Command;
using Energie.Business.Energie.QueryHandler.NotificationFlagHandler;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.CommandHandler
{
    public class UserNotificationCommandHandler : IRequestHandler<UserNotificationCommand, NotificationResponseList>
    {
        private readonly ILogger<UserNotificationCommandHandler> _logger;
        private readonly IFlagRepository _repository;
        private readonly ICompanyUserRepository _companyUserRepository;
        public UserNotificationCommandHandler(IFlagRepository repository
            , ILogger<UserNotificationCommandHandler> logger
            , ICompanyUserRepository companyUserRepository)
        {
            _repository = repository;
            _logger = logger;
            _companyUserRepository = companyUserRepository;
        }
        public async Task<NotificationResponseList> Handle(UserNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                var user = await _companyUserRepository.GetCompanyUserAsync(request.UserEmail);
                var setNotification = new Notification().SetNotification(user.Id, request.Id);
                var response1 = await _repository.GetNotificationsForUserAsync(request.UserEmail);
                var resp = response1.Select(x => x.NotificationTypeId.ToString()).ToList();
                if(resp.Contains(setNotification.NotificationTypeId.ToString()))
                {
                    await _repository.RemoveUserNotification(setNotification);
                }
                else
                {
                    await _repository.SetUserNotification(setNotification);
                }

                var response = await _repository.GetNotificationsForUserAsync(request.UserEmail);
                var notificationTypes = await _repository.GetNotificationTypesAsync();
                var list = new List<NotificationResponse>();
                if (response.Count == 0)
                {
                    list = notificationTypes.Select(x => new NotificationResponse
                    {
                        Id = x.Id,
                        NotificationName = x.Notification_Name,
                    }).ToList();
                }
                else
                {
                    var res = response.Select(x => x.NotificationTypeId.ToString()).ToList();
                    foreach (var item in notificationTypes)
                    {
                        if (res.Contains(item.Id.ToString()))
                        {
                            var list1 = response.Where(x => x.NotificationTypeId == item.Id).Select(x => new NotificationResponse
                            {
                                Id = (int)x.NotificationTypeId,
                                NotificationName = x.NotificationType.Notification_Name,
                                IsSelected = true
                            }).ToList();
                            list.AddRange(list1);
                        }
                        else
                        {
                            var list2 = notificationTypes.Where(x => x.Id == item.Id).Select(x => new NotificationResponse
                            {
                                Id = x.Id,
                                NotificationName = x.Notification_Name,

                            }).ToList();
                            list.AddRange(list2);
                        }
                    }
                }
                return new NotificationResponseList { NotificationResponses= list };
             
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error occured in {{0}}", nameof(NotificationResponseQueryHandler));
                throw;
            }
        }
    }
}

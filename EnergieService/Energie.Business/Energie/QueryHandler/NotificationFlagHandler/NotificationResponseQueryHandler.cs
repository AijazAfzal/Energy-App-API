using Energie.Business.Energie.Query.NotificationFlag;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Model.Request;
using Energie.Model.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.QueryHandler.NotificationFlagHandler
{
    public class NotificationResponseQueryHandler : IRequestHandler<NotificationResponseQuery, NotificationResponseList>
    {
        private readonly IFlagRepository _repository;
        private readonly ILogger<NotificationResponseQueryHandler> _logger;
        private readonly ITranslationsRepository<NotificationType> _translationService;
        public NotificationResponseQueryHandler(IFlagRepository repository, ILogger<NotificationResponseQueryHandler> logger, ITranslationsRepository<NotificationType> translationService)
        {
            _repository = repository;
            _logger = logger;
            _translationService = translationService;
        }
        
        public async Task<NotificationResponseList> Handle(NotificationResponseQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _repository.GetNotificationsForUserAsync(request.UserEmail);
                var notificationTypes = await _repository.GetNotificationTypesAsync();
                if(request.Language == "en-US")
                {
                    var translatedNotification = await _translationService.GetTranslatedDataAsync<NotificationType>(request.Language);
                    notificationTypes = translatedNotification;
                }

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
                return new NotificationResponseList { NotificationResponses = list };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error occured in {{0}}", nameof(NotificationResponseQueryHandler));
                throw;
            }
        }
    }
}

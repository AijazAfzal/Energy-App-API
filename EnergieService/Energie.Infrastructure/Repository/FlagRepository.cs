using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Infrastructure.Repository
{
    public class FlagRepository : IFlagRepository
    {
        private readonly AppDbContext _appDbContext;
        public FlagRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<CompanyUser> GetUserOnboarding(string userEmail)
        {
            var companyUser = await _appDbContext.CompanyUser
                    .Where(x => x.Email == userEmail)
                    .FirstOrDefaultAsync();
            return companyUser;
        }
        public async Task<int> UpdateUserOnboarding(CompanyUser companyUser)
        {
            _appDbContext.CompanyUser.Update(companyUser);
            return await _appDbContext.SaveChangesAsync();
        }
        public async Task<MonthlyNotification> GetMonthlyNotificationAsync(MonthlyNotification monthlyNotification)
        {
            var notification = await _appDbContext.MonthlyNotifications
                                .Where(x => x.MonthId == monthlyNotification.MonthId
                                            && x.Year == monthlyNotification.Year
                                            && x.CompanyUserID == monthlyNotification.CompanyUserID)
                                .FirstOrDefaultAsync();
            return notification;
        }
        public async Task<MonthlyNotification> SetMonthlyNotificationAsync(MonthlyNotification monthlyNotification)
        {
            await _appDbContext.MonthlyNotifications.AddAsync(monthlyNotification);
            await _appDbContext.SaveChangesAsync();
            return monthlyNotification;
        }
        public async Task<UserEnergyAnalysis> GetEnergyAnalysisFlagAsync(string email)
        {
            var userEnergyAnalysis = await _appDbContext.UserEnergyAnalyses
                   .Where(x => x.CompanyUser.Email == email)
                   .FirstOrDefaultAsync();
            return userEnergyAnalysis;
        }
        public async Task<List<Notification>> GetNotificationsForUserAsync(string email)
        {
            return await _appDbContext.Notifications
                        .Where(x=> x.CompanyUser.Email == email)
                        .Include(x=> x.NotificationType)
                        .Include(x=> x.CompanyUser)
                        .ToListAsync();
        }
        public async Task<List<NotificationType>> GetNotificationTypesAsync()
        {
            return await _appDbContext.NotificationTypes.ToListAsync();
        }
        public async Task SetUserNotification(Notification notification)
        {
            await _appDbContext.Notifications.AddAsync(notification);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task RemoveUserNotification(Notification notification)
        {
            var notifications = await _appDbContext
                                    .Notifications
                                    .Where(x => x.NotificationTypeId == notification.NotificationTypeId && x.CompanyUserID == notification.CompanyUserID)
                                    .FirstOrDefaultAsync();
            _appDbContext.Notifications.Remove(notifications);
            await _appDbContext.SaveChangesAsync();
        }
    }
}

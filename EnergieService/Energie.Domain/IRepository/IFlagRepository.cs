using Energie.Domain.Domain;

namespace Energie.Domain.IRepository
{
    public interface IFlagRepository
    {
        Task<CompanyUser> GetUserOnboarding(string userEmail);
        Task<int> UpdateUserOnboarding(CompanyUser companyUser);
        Task<MonthlyNotification> GetMonthlyNotificationAsync(MonthlyNotification monthlyNotification);
        Task<MonthlyNotification> SetMonthlyNotificationAsync(MonthlyNotification monthlyNotification);
        Task<UserEnergyAnalysis> GetEnergyAnalysisFlagAsync(string email);
        Task<List<Notification>> GetNotificationsForUserAsync(string email);
        Task<List<NotificationType>> GetNotificationTypesAsync();
        Task SetUserNotification(Notification notification);
        Task RemoveUserNotification(Notification notification);
    }
}

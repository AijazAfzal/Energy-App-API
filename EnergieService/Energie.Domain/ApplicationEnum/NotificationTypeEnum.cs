using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.ApplicationEnum
{
    public enum NotificationTypeEnum
    {
        EnergyScoreNewMeasurementNotification = 1,
        EnergyAnalysisUpdateNotification = 2,
        EnergyTipAddedbyColleagueNotification = 3,
        EnergyPlanDeadlineNotification = 4,
    }
}

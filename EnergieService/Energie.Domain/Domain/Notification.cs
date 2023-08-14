using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Energie.Domain.Domain
{
    public class Notification
    {
        [Key]
        public int Id { get; private set; }

        [Display(Name = "CompanyUserID")]
        public virtual int? CompanyUserID { get; private set; }
        [ForeignKey("CompanyUserID")]
        public virtual CompanyUser? CompanyUser { get; set; }

        [Display(Name = "NotificationTypeId")]
        public virtual int? NotificationTypeId { get; private set; }
        [ForeignKey("NotificationTypeId")]
        public virtual NotificationType? NotificationType { get; set; }

        public DateTime CreatedOn { get; private set; } 

        public Notification SetNotification(int userId, int notificationTypeId)
        {
            CompanyUserID = userId;
            NotificationTypeId = notificationTypeId;
            CreatedOn = DateTime.Now;
            return this;
        }

    }
}

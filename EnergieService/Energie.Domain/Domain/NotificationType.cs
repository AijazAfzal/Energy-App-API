using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class NotificationType
    {
        [Key]
        public int Id { get; private set; }

        public string Notification_Name { get; private set; } 
    }
}

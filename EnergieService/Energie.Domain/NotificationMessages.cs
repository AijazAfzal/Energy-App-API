using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain
{
    public class NotificationMessageList
    {
        public List<NotificationMessages> NotificationMessages { get; set; }
    }
    public class NotificationMessages
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Response
{
    public class NotificationResponseList
    {
        public List<NotificationResponse> NotificationResponses { get; set;}
    }
    public class NotificationResponse
    {
        public int Id { get; set; }
        public string NotificationName { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}

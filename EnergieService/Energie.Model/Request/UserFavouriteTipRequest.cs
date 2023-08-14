using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    public class UserFavouriteTipRequestList
    {
        public List<UserFavouriteTipRequest> UserFavouriteTipsRequests { get; set; }
    } 
    public class UserFavouriteTipRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AddedDate { get; set; }
        public string TipBy { get; set; }
    }
}

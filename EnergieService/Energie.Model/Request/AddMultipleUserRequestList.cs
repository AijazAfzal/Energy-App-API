using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    public class AddMultipleUserRequestList
    {

        public List<AddMultipleUserRequest> AddMultipleUserRequests  { get; set; }
    }
    public class AddMultipleUserRequest
    {

        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int DepartmentId { get; set; }

    }
}

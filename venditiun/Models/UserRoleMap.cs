using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venditiun.Models
{
    public class UserRoleMap
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }
        
        public string RoleId { get; set; }

    }
}

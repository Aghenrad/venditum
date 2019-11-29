using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace venditiun.Models
{
    public class UserRoleMap
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        
        public int RoleId { get; set; }


        [ForeignKey("UserId")]
        public Task User { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venditiun.Models
{
    public class TaskUserMap
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }
        
        public string TaskId { get; set; }

    }
}

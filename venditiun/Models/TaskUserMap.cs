using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace venditiun.Models
{
    public class TaskUserMap
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        
        public int TaskId { get; set; }


        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("TaskId")]
        public Task Task { get; set; }

    }
}

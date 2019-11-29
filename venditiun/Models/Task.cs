using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace venditiun.Models
{
    public class Task
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Name { get; set; }
        
        public string Decription { get; set; }

        public int StatusId { get; set; }

        public int CreatedBy { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }


        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }

        [ForeignKey("UpdatedBy")]
        public User UpdatedByUser { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }


        [InverseProperty("Task")]
        public List<Job> Jobs { get; set; }

        [InverseProperty("Task")]
        public List<TaskUserMap> TaskUserMap { get; set; }
        
    }
}

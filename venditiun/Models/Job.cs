using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace venditiun.Models
{
    public class Job
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public int UserId { get; set; }
        
        public int Decription { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BeginDate { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public int StatusId { get; set; }

        public int CreatedBy { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }


        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }

        [ForeignKey("UpdatedBy")]
        public User UpdatedByUser { get; set; }

        [ForeignKey("TaskId")]
        public Task Task { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        [ForeignKey("UserId")]
        public Task User { get; set; }
        
    }
}

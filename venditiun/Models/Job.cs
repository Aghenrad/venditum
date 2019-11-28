using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venditiun.Models
{
    public class Job
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public string UserId { get; set; }
        
        public string Decription { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BeginDate { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public int CreatedBy { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }


        public Task Task { get; set; }
    }
}

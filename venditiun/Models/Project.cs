using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venditiun.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        
        public string ProjectName { get; set; }

        public int CreatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedTime { get; set; }

        public int UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime UpdatedTime { get; set; }


    }
}

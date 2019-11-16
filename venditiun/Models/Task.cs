using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public int Status { get; set; }

        public int CreatedBy { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime UpdatedDate { get; set; }

    }
}

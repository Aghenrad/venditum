using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venditiun.Models
{
    public class User
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Password { get; set; }

        public int PositionId { get; set; }

        public bool Active { get; set; }
        
        public int CreatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime UpdatedDate{ get; set; }

    }
}

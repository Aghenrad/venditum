using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace venditiun.Models
{
    public class Role
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public bool Privilege1 { get; set; }

        public bool Privilege2 { get; set; }


    }
}

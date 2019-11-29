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

        public string Login { get; set; }

        public string Password { get; set; }

        public string Firstname { get; set; }

        public string LastName { get; set; }

        public int PositionId { get; set; }

        public bool Active { get; set; }
        
    }
}

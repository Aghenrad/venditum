using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using venditiun.Models;

namespace venditum.Data
{
    public class VenditiunDbContext : IdentityDbContext
    {
        public VenditiunDbContext(DbContextOptions<VenditiunDbContext> options)
            : base(options)
        {
        }
        public DbSet<Project> Project { get; set; }
    }
}

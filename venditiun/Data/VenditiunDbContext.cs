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

        public DbSet<User> Users { get; set; }

        public DbSet<UserRoleMap> UserRoleMaps { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<TaskUserMap> TaskUserMaps { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Status> Statuses { get; set; }
    }

}

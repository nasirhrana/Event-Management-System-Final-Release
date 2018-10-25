using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EventManagementSystem.Models
{
    public class EMSDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<VisitorRegistration> VisitorRegistrations { get; set; }

    }
}
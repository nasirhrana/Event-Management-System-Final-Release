using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagementSystem.Models
{
    public class UserType
    {
        public int Id { get; set; }
        public string UserTypeName { get; set; }
        public List<User> Employees { get; set; } 
    }
}
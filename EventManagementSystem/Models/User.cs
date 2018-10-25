using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagementSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter  Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter  Email")]
        [EmailAddress]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        [Remote("IsEmpIdExist", "User", ErrorMessage = "This Email already exists!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Select  User Type ")]
        [DisplayName("User Type ID")]
        public int UserTypeId { get; set; }
        [Required(ErrorMessage = "Please Enter  Password")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 to 20 character")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Re-enter Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [DisplayName("Confirm Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        public UserType UserType { get; set; }
        public List<Event> Events { get; set; } 
    }
}
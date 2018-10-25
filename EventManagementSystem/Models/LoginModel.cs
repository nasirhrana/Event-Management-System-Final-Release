using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagementSystem.Models
{
    public class LoginModel
    {
        [Required (ErrorMessage = "Please Enter Your UserName")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Your Password")]
        public string Password { get; set; }
    }
}
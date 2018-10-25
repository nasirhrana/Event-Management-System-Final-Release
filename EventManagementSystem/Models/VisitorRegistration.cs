using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagementSystem.Models
{
    public class VisitorRegistration
    {
        [Key]
        public int VisitorId { get; set; }
        [Required(ErrorMessage = "Please Enter Visitor Name")]
        
        public string VisitorName { get; set; }
        [Required(ErrorMessage = "Please Enter Visitor Email")]
        [EmailAddress]
        [Remote("IsVisitorEmailExist", "Event", ErrorMessage = "This Email already exists!")]
        public string VisitorEmail { get; set; }
        [Required(ErrorMessage = "Please Enter Visitor Contact No")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact Number must be numeric")]
        [Remote("IsVisitorContactNoExist", "Event", ErrorMessage = "This Contact No already exists!")]
        public string VisitorContactNo { get; set; }
        [Required(ErrorMessage = "Please Select Visitor Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please Select Visitor's  Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please Select Event ID")]
        public int EventId { get; set; }
        [Required]
        public string TicketNo { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TimeOfRegistration { get; set; }
        public Event Event { get; set; }
        
    }
}
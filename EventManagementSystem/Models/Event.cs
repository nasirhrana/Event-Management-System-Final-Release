using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagementSystem.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please Enter Event Name")]
        public string EventName { get; set; }
        [Required(ErrorMessage = "Please Enter Event Venue")]
        public string EventVenue { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress]
        public string OrganizerEmail { get; set; }
        [Required(ErrorMessage = "Please Enter Contact Number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact Number must be numeric")]
        public string ContactNo { get; set; }
        
        [Required(ErrorMessage = "Please Enter Event Date")]
        [DataType(DataType.Date),DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        [Required]

        public string CreatedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime TimeOfCreation { get; set; }

        public User User { get; set; }
        public List<VisitorRegistration> VisitorRegistrations { get; set; }


    }
}
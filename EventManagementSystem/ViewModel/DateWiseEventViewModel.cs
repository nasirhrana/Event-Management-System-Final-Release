using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagementSystem.ViewModel
{
    public class DateWiseEventViewModel
    {
        public string EventName { get; set; }
        public string EventVenue { get; set; }
        public DateTime EventDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
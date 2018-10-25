using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagementSystem.ViewModel
{
    public class GenderWiseEventViewModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDate { get; set; }
        public int TotalVisitor { get; set; }
        public int ZeroToTwentyFiveCount { get; set; }
        public int TwentySixToFortyCount { get; set; }
        public int FortyAboveCount { get; set; }
    }
}
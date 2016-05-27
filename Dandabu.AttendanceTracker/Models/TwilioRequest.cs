using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dandabu.AttendanceTracker.Models
{
    public class TwilioRequest
    {
        public string MessageSid { get; set; }
        public string AccountSid { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogApplication.Messaging
{
    public class EmailMessage
    {
        public string Subject { get; set; }
        public string Recipient { get; set; }
        public string Body { get; set; }
    }
}
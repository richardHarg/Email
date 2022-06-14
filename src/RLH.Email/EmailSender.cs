using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Email
{
    public class EmailSender
    {
        internal EmailSender(string senderName,string emailAddress)
        {
            SenderName = senderName;
            EmailAddress = emailAddress;
        }

        /// <summary>
        /// Name of the address as it appears in the email  e.g. 'DoNotReply'
        /// </summary>
        public string SenderName { get; private set; }
        /// <summary>
        /// Email address of outbound sender
        /// </summary>
        public string EmailAddress { get; private set; }
       
    }
}

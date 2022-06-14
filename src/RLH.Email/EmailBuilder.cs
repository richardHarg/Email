using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Email
{
    public class EmailBuilder
    {
        public string HTMLBody { get; private set; }
        public EmailSender EmailSender { get; private set; }
        public string EmailReceiver { get; private set; }
        public string SubjectLine { get; private set; }
        public ICollection<EmailAttachment> EmailAttachments { get; private set; } = new List<EmailAttachment>();

        public void SetHTMLBody(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
            {
                throw new ArgumentException($"'{nameof(body)}' cannot be null or whitespace.", nameof(body));
            }
            HTMLBody = body;
        }
        public void SetEmailSender(string senderName,string emailAddress)
        {
            if (string.IsNullOrEmpty(senderName))
            {
                throw new ArgumentException($"'{nameof(senderName)}' cannot be null or empty.", nameof(senderName));
            }

            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentException($"'{nameof(emailAddress)}' cannot be null or empty.", nameof(emailAddress));
            }

            EmailSender = new EmailSender(senderName, emailAddress);

        }
        public void SetEmailReceiver(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new ArgumentException($"'{nameof(emailAddress)}' cannot be null or whitespace.", nameof(emailAddress));
            }

            EmailReceiver = emailAddress;
        }
        public void SetSubjectLine(string subjectLine)
        {
            if (string.IsNullOrWhiteSpace(subjectLine))
            {
                throw new ArgumentException($"'{nameof(subjectLine)}' cannot be null or whitespace.", nameof(subjectLine));
            }
            SubjectLine = subjectLine;
        }
        public void AddEmailAttachment(byte[] bytes, string fileName, string extension)
        {
            EmailAttachments.Add(new EmailAttachment(bytes, fileName, extension));
        }

    }
}

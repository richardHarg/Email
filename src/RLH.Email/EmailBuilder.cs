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
        public ICollection<EmailAttachment> EmailAttachments { get; private set; }

        public void SetHTMLBody(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
            {
                throw new ArgumentException($"'{nameof(body)}' cannot be null or whitespace.", nameof(body));
            }
            HTMLBody = body;
        }
        public void SetEmailSender(string senderName,string emailAddress, string smtpServer, int port, string userName, string password)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentException($"'{nameof(emailAddress)}' cannot be null or empty.", nameof(emailAddress));
            }

            if (string.IsNullOrEmpty(smtpServer))
            {
                throw new ArgumentException($"'{nameof(smtpServer)}' cannot be null or empty.", nameof(smtpServer));
            }

            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException($"'{nameof(userName)}' cannot be null or empty.", nameof(userName));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty.", nameof(password));
            }

            EmailSender = new EmailSender(senderName, emailAddress, smtpServer, port, userName, password);
        }
        public void SetEmailSender(string senderName, string emailAddress,EmailOptions emailOptions)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentException($"'{nameof(emailAddress)}' cannot be null or empty.", nameof(emailAddress));
            }

            if (emailOptions is null)
            {
                throw new ArgumentNullException(nameof(emailOptions));
            }

            EmailSender = new EmailSender(senderName, emailAddress, emailOptions.SmtpServer, emailOptions.Port, emailOptions.Username, emailOptions.Password);
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

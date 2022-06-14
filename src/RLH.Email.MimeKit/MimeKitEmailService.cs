using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Email.MimeKit
{
    public class MimeKitEmailService : IEmailService
    {
        private bool disposedValue;
        private readonly EmailOptions _options;

        public MimeKitEmailService(EmailOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<Result.Result> SendAsync(EmailBuilder emailBuilder)
        {
            if (emailBuilder is null)
            {
                throw new ArgumentNullException(nameof(emailBuilder));
            }

            var checkResult = CheckEmailBuilderValues(emailBuilder);

            if (checkResult.Errors.Any() == true)
            {
                return checkResult;
            }

            var newBuilder = new BodyBuilder();

            newBuilder.HtmlBody = emailBuilder.HTMLBody;

            foreach (EmailAttachment att in emailBuilder.EmailAttachments)
            {
                try
                {
                    // Add attachment
                    newBuilder.Attachments.Add($"{att.FileName}.{att.Extension}", att.Bytes, new ContentType("application", att.Extension));
                    // For some email clients to pickup and display the image in the body this content Id must be set/match that in the HTML
                    newBuilder.Attachments.Last(x => x.ContentDisposition.FileName == $"{att.FileName}.{att.Extension}").ContentId = att.FileName;
                }
                catch (Exception e)
                {
                    return Result.Result.Error(e.Message);
                }
            }

            return await SendMimeMessageAsync(BuildMimeMessage(emailBuilder.EmailSender.SenderName,emailBuilder.EmailSender.EmailAddress, emailBuilder.EmailReceiver, emailBuilder.SubjectLine, newBuilder));
        }





        private Result.Result CheckEmailBuilderValues(EmailBuilder builder)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(builder.SubjectLine) == true)
            {
                errors.Add("Subject line must not be blank");
            }
            if (string.IsNullOrWhiteSpace(builder.HTMLBody) == true)
            {
                errors.Add("HTML Body must not be blank");
            }
            if (string.IsNullOrWhiteSpace(builder.EmailReceiver) == true)
            {
                errors.Add("Email receiver must not be blank");
            }
            if (builder.EmailSender == null || string.IsNullOrWhiteSpace(builder.EmailSender.SenderName) == true || string.IsNullOrWhiteSpace(builder.EmailSender.EmailAddress) == true)
            {
                errors.Add("Email sender must exist and name/email must not be blank");
            }

            if (errors.Any())
            {
                return Result.Result.Error(errors);
            }
            else
            {
                return Result.Result.Success();
            }
        }







        private async Task<Result.Result> SendMimeMessageAsync(MimeMessage message)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    await smtpClient.ConnectAsync(_options.SmtpServer, _options.Port, true);
                    await smtpClient.AuthenticateAsync(_options.Username, _options.Password);
                    await smtpClient.SendAsync(message);
                    await smtpClient.DisconnectAsync(true);
                }
                return Result.Result.Success();
            }
            catch (Exception e)
            {
                return Result.Result.Error(e.Message);
            }
        }
        private MimeMessage BuildMimeMessage(string senderName, string senderEmail, string receiverEmail, string subject, BodyBuilder bodyBuilder)
        {
            var mime = new MimeMessage();
            mime.From.Add(new MailboxAddress(senderName, senderEmail));
            mime.To.Add(new MailboxAddress("reciever", receiverEmail));
            mime.Subject = subject;
            mime.Body = bodyBuilder.ToMessageBody();
            return mime;
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MimeKitEmailService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

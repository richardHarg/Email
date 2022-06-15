namespace RLH.Email.Tests
{
    public class EmailTemplateTests
    {
        [Fact]
        public void TemplateValuesPopulate_SetupTwoFactor()
        {
            var builder = new EmailBuilder();

            builder.SetSubjectLine("Two Factor Setup Instructions");
            builder.SetEmailSender("DoNotReply", "noreply@rlhweb.co.uk");
            builder.SetEmailReceiver("CustomerEmailAddress@rlhWeb.co.uk");

            var templateValues = new List<string>()
                        {
                            "CAPI",
                            "logoURL",
                            "CustomerEmailAddress@rlhWeb.co.uk",
                            "MANUALCODE",
                            "URL TO PORTAL"
                        };

            builder.SetHTMLBodyFromTemplate(@"Data\TwoFactorSetup.html", templateValues);

            Assert.NotNull(builder.HTMLBody);

        }
    }
}
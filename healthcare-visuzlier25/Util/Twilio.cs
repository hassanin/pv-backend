using SendGrid;
using SendGrid.Helpers.Mail;
namespace healthcare_visuzlier25.Util
{
    public class Twilio
    {
        public static async Task SendEmail()
        {
            var apiKey = "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";//Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("pv@yaqaza-services.com", "Yaqaza Services");
            var subject = "This will be the email to the hey2aet el dawa2";
            var to = new EmailAddress("hassanin@udel.edu", "Mohamed Hassanin");
            var plainTextContent = "and we will add attachment later";
            var htmlContent = "<strong>and we will add attachment later</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}

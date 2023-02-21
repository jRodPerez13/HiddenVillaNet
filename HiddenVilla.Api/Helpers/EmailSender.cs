using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace HiddenVilla.Api.Helpers;
public class EmailSender : IEmailSender
{
    private readonly MailJetSettings _mailJetSettings;
    private const string FROM_NAME = "Hidden Villa";

    public EmailSender(IOptions<MailJetSettings> mailjetSettings)
    {
        _mailJetSettings = mailjetSettings.Value;
    }

    public async Task SendEmailAsync(string emailAddress, string subject, string htmlMessage)
    {
        MailjetClient client = new MailjetClient(_mailJetSettings.PublicKey,
                _mailJetSettings.PrivateKey);
        MailjetRequest request = new MailjetRequest
        {
            Resource = Send.Resource,
        }
        .Property(Send.FromEmail, _mailJetSettings.Email)
        .Property(Send.FromName, FROM_NAME)
        .Property(Send.Subject, subject)
        .Property(Send.HtmlPart, htmlMessage)
        .Property(Send.Recipients, new JArray {
            new JObject {
                { "Email", emailAddress }
            }
        });

        MailjetResponse response;
        try
        {
            response = await client.PostAsync(request);
        }
        catch (Exception ex)
        {
            // Log error
            throw new Exception("Error sending email: " + ex.Message);
        }

        if (!response.IsSuccessStatusCode)
        {
            // Log error
            throw new Exception("Error sending email: " + response.StatusCode);
        }
    }
}

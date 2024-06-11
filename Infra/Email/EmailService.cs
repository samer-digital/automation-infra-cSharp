using System.Net;
using System.Net.Mail;

public class EmailService
{
    private readonly SmtpClient _client;
    private readonly string _from;

    public EmailService(string smtpHost, int smtpPort, string smtpUser, string smtpPass)
    {
        _client = new SmtpClient(smtpHost, smtpPort)
        {
            Credentials = new NetworkCredential(smtpUser, smtpPass),
            EnableSsl = true
        };
        _from = smtpUser;
    }

    public async Task SendMailAsync(string to, string subject, string text, string html)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_from),
            Subject = subject,
            Body = html,
            IsBodyHtml = true
        };
        mailMessage.To.Add(to);
        await _client.SendMailAsync(mailMessage);
    }
}
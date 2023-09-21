namespace CleanZone.Services;

public class EmailService
{
    public void SendEmail(string toEmail, string subject, string body)
    {
        var mail = "CleanZoneApp@gmail.com";
        var pass = "rmma szsv xcqj msjm";
        var smtpClient = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(mail, pass),
        };
        var mailMessage = new MailMessage
        {
            From = new MailAddress(mail),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(toEmail);
        smtpClient.Send(mailMessage);
    }
    public void EnviarEmails(List<DivisionViewModel> divisions)
    {
        foreach (var division in divisions.Where(d => d.IsClean == false))
        {
            var emailSubject = "Alerta de Limpeza";
            var emailBody = $"Olá, User da CelanZone!\n\nA divisão '{division.Name}' precisa de limpeza.";

            SendEmail(division.EmailSubject, emailSubject, emailBody);
        }
    }
}


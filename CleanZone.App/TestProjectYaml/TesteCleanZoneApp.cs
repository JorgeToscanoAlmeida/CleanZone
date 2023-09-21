namespace TestProject;
public class TesteCleanZoneApp
{
    //False
    [Fact]
    public void ShouldNotImportResidents()
    {
        var yamlContent = "---\r\nName: Divisão 1\r\nCleanTime: 30\r\nCleanInterval: 7\r\nLastClean: '2023-09-20T10:00:00'\r\nIsClean: false\r\nAreaId: 1\r\nArea:\r\n  Name: Sala de Estar\r\n  ResidenceID: 1\r\n  Residence:\r\n    Name: Minha Residência";
        var deserializer = new DeserializerBuilder().Build();
        var division = deserializer.Deserialize<Division>(yamlContent);

        Assert.NotNull(division);
        Assert.NotNull(division.Area);
        Assert.NotNull(division.Area.Residence);

        Assert.Equal("Divisão 1", division.Name);
        Assert.Equal(30, division.CleanTime);
        Assert.Equal(7, division.CleanInterval);
        Assert.True(division.IsClean); // False o erro Esta AQUI "Actual: False Expected: True"

        Assert.Equal("Sala de Estar", division.Area.Name);
        Assert.Equal(1, division.Area.ResidenceID);

        Assert.Equal("Minha Residência", division.Area.Residence.Name);
    }
    // True
    [Fact]
    public void ShouldSendEmail()
    {
        string toEmail = "vitima@gmail.com";
        string subject = "Alerta de Limpeza";
        string body = "Olá, User da CelanZone!";


        var mail = "CleanZoneApp@gmail.com";
        var mailMessage = new MailMessage
        {
            From = new MailAddress(mail),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(toEmail);

        Assert.Contains(toEmail, mailMessage.To.ToString());
        Assert.Equal(subject, mailMessage.Subject);
        Assert.Equal(body, mailMessage.Body);
    }
}
using Microsoft.AspNetCore.Identity;

namespace CleanZone.Data.Entities;

public class EmailLog
{
    public int Id { get; set; }
    public string DivisionID { get; set; }
    public string EmailContent { get; set; } 
    public DateTime SentAt { get; set; }
}

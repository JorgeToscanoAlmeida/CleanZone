namespace CleanZone.Data.Entities;

public class CleanLog
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public int DivisionId { get; set; }
    public Division Division { get; set; }

}

using System.ComponentModel.DataAnnotations.Schema;

namespace CleanZone.Data.Entities;

public class Division
{
    public int ID { get; set; }
    public string Name { get; set; }
    public DateTime CleanTime { get; set; }
    public DateTime CleanInterval { get; set; }
    [NotMapped]
    public List<DateTime>? CleanLog { get; set; }
    public int AreaId { get; set; }
    public Area Area { get; set; }

}

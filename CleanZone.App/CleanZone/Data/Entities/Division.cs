using System.ComponentModel.DataAnnotations.Schema;

namespace CleanZone.Data.Entities;

public class Division
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int CleanTime { get; set; }
    public int CleanInterval { get; set; }
    public DateTime LastClean { get; set; }
    public int AreaId { get; set; }
    public Area Area { get; set; }

}

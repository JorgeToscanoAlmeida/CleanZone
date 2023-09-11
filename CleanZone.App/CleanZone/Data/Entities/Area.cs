namespace CleanZone.Data.Entities;

public class Area
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ResidenciaID { get; set; }
    public Residence Residence { get; set; }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CleanZone.Data.Entities;

public class Residence
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserID { get; set; }
    public IdentityUser User { get; set; }

}

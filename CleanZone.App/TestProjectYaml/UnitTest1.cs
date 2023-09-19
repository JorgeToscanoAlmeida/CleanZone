using CleanZone;
using CleanZone.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace TestProjectYaml;
public class UnitTest1
{

    [Fact]
    public void ShouldImportBuildings()
    {
        var yamlContent = "..."; 
        var residence = new Residence(); 

        var result = residence.ImportFromYaml(yamlContent);

        Assert.NotNull(result);
    }

}
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanZone.Data.Entities;

public class Division
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int CleanTime { get; set; }
    public int CleanInterval { get; set; }
    public DateTime LastClean { get; set; }
    public bool IsClean { get; set; }
    public int AreaId { get; set; }
    public Area Area { get; set; }


    public void AddCleaning(DateTime date)
    {
        LastClean = date;
        IsClean = true;
    }
    public List<string> ShowCleaningStatus2(DateTime currentDate)
    {
        var barColors = new List<string>();

        var cleanInterval = CleanInterval;
        var lastCleanDate = LastClean;
        var daysSinceLastClean = (currentDate - lastCleanDate).Days;
        var cleanDueDate = lastCleanDate.AddDays(cleanInterval);
        //var daysOverdue = (currentDate - cleanDueDate).Days;

        if (currentDate > cleanDueDate.AddDays(2))
        {
            IsClean = false;
            barColors.Add("red");
        }
        else if (lastCleanDate < currentDate && currentDate >= cleanDueDate.AddDays(-1))
        {
            IsClean = true;
            barColors.Add("yellow");
        }
        else if (currentDate < cleanDueDate.AddDays(-1))
        {
            IsClean = true;
            barColors.Add("green");
        }
        /*
        for (int i = 0; i < daysSinceLastClean; i++)
        {
            if (barColors.Count >= 20)
                break;

            barColors.Add("|");
        }
        */
        return barColors;
    }
}

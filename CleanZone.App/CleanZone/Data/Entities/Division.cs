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
    


    public List<string> ShowCleaningStatus(DateTime currentDate)
    {
        var listalimpa = new List<string>();
        /*
        if (LastClean < currentDate)
        {
            listalimpa.Add("Sem registos de limpeza");
            return listalimpa;
        }
        */
        var cleanInterval = CleanInterval;
        var lastCleanDate = LastClean;
        var daysSinceLastClean = (currentDate - lastCleanDate).Days;
        var cleanDueDate = lastCleanDate.AddDays(cleanInterval);
        var daysOverdue = (currentDate - cleanDueDate).Days;

        if (currentDate > cleanDueDate.AddDays(2))
        {
            return PrintBars(ConsoleColor.Red, daysOverdue);
        }
        else if (lastCleanDate < currentDate && currentDate >= cleanDueDate.AddDays(-1))
        {
            return PrintBars(ConsoleColor.Yellow, daysSinceLastClean);
        }
        else if (currentDate < cleanDueDate.AddDays(-1))
        {
            return PrintBars(ConsoleColor.Green, daysSinceLastClean);
        }
        return listalimpa;

    }
    private List<string> PrintBars(ConsoleColor color, int barsNumber)
    {
        var teste = new List<string>();
        //Console.ForegroundColor = color;
        for (int i = 0; i < barsNumber; i++)
        {
            if (i >= 20)
                break;

            teste.Add("|");
            //Console.Write("|");
        }
        return teste;
        //Console.ResetColor();
    }
    public void AddCleaning(DateTime date)
    {
        LastClean = date;
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
            barColors.Add("red");
        }
        else if (lastCleanDate < currentDate && currentDate >= cleanDueDate.AddDays(-1))
        {
            barColors.Add("yellow");
        }
        else if (currentDate < cleanDueDate.AddDays(-1))
        {
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

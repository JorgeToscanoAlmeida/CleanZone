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

    public string ShowCleaningStatus(DateTime currentDate)
    {
        
        if (LastClean <= currentDate)
        {
            //Console.Write("»»» Sem registos de limpeza.");
            return "Sem registos de limpeza";
        }
        
        var cleanInterval = CleanInterval;
        var lastCleanDate = LastClean;
        var daysSinceLastClean = (currentDate - lastCleanDate).Days;
        var cleanDueDate = lastCleanDate.AddDays(cleanInterval);
        var daysOverdue = (currentDate - cleanDueDate).Days;
        
        if (currentDate > cleanDueDate.AddDays(2))
        {
            PrintBars(ConsoleColor.Red, daysOverdue);
        }
        else if (lastCleanDate < currentDate && currentDate >= cleanDueDate.AddDays(-1))
        {
            PrintBars(ConsoleColor.Yellow, daysSinceLastClean);
        }
        else if (currentDate < cleanDueDate.AddDays(-1))
        {
            PrintBars(ConsoleColor.Green, daysSinceLastClean);
        }
        return "limpo";
        
    }
    private void PrintBars(ConsoleColor color, int barsNumber)
    {
        //Console.ForegroundColor = color;
        for (int i = 0; i < barsNumber; i++)
        {
            if (i >= 20)
                break;
            //Console.Write("|");
        }
        //Console.ResetColor();
    }
}

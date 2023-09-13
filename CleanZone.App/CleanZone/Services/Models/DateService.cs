namespace CleanZone.Services.Models;

public class DateService
{
    private DateTime _dataAtual;

    public DateService()
    {
        _dataAtual = DateTime.Now;
    }

    public DateTime ObterDataAtual()
    {
        return _dataAtual;
    }
    public void IncrementarData()
    {
        _dataAtual = _dataAtual.AddDays(1);
    }
    public void IncrementarDataSimulation(int num)
    {
        _dataAtual = _dataAtual.AddDays(num);
    }
    public void DescrementarData()
    {
        _dataAtual = _dataAtual.AddDays(-1);
    }
}

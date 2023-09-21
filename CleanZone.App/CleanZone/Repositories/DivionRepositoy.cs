using CleanZone.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanZone.Repositories;

public class DivionRepositoy
{
    private readonly ApplicationDbContext _ctx;
    private readonly ILogger<ResidenceRepository> _logger;
    public DivionRepositoy(ApplicationDbContext ctx, ILogger<ResidenceRepository> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }
    public async Task<IList<Division>> GetDivisionsAsync(string username)
    {
        return await _ctx.Division
            .Where(r => r.Area.Residence.User.UserName == username)
            .Include(a => a.Area.Residence.User)
            .ToListAsync();
    }
    public async Task<List<DivisionViewModel>> GetListDivisionsByUsernameAsync(string username)
    {
        return await _ctx.Division
            .Where(d => d.Area.Residence.User.UserName == username)
            .Select(d => new DivisionViewModel
            {
                Id = d.ID,
                Name = d.Name,
                IsClean = d.IsClean,
                EmailSubject = d.Area.Residence.User.UserName,
            }).ToListAsync();
    }
    public async Task AddEmailLogAsync(EmailLog emailLog)
    {
        _ctx.EmailLogs.Add(emailLog);
        await _ctx.SaveChangesAsync();
    }
    public async Task<Division> GetByIdAsync(int? id)
    {
        return await _ctx.Division.Where(c => c.ID == id).FirstOrDefaultAsync();
    }
    public async Task DeleteByIdAsync(int? id)
    {
        int result = await _ctx.Division.Where(c => c.ID == id).ExecuteDeleteAsync();
    }
    public async Task UpdateAsync(Division division)
    {
        _ctx.Attach(division).State = EntityState.Modified;
        _ = await _ctx.SaveChangesAsync();
    }
    public async Task<Division> AddDivisionAsync(Division division)
    {
        _ctx.Division.Add(division);
        await _ctx.SaveChangesAsync();
        var clean = new CleanLog()
        {
            DateTime = division.LastClean,
            DivisionId = division.ID,
        };
        _ctx.CleanLogs.Add(clean);
        await _ctx.SaveChangesAsync();
        return division;
    }
    public async Task<Division> FindAsync(int? id)
    {
        return await _ctx.Division.FindAsync(id);
    }
    public async Task<List<Division>> GetByNameAsync(string username)
    {
        if (_ctx.Division != null)
        {
            return await _ctx.Division
            .Where(r => r.Area.Residence.User.UserName == username)
            .Include(a => a.Area.Residence)
            .ToListAsync();
        }
        return null;
    }
    public (bool isValid, string errorMessage, string name) Validate(DateTime lastClean, int cleanTime, int cleanInterval)
    {
        if (lastClean > DateTime.Now)
        {
            return (false, "The date cannot be later than the current date.", "LastClean");
        }
        if (cleanTime < 0)
        {
            return (false, "The value must be greater than or equal to 0.", "CleanTime");
        }
        if (cleanInterval < 0)
        {
            return (false, "The value must be greater than or equal to 0.", "CleanInterval");
        }
        return (true, null, null);
    }
    public SelectList ViewDataByName(string username)
    {
        var userResidences = _ctx.Area
        .Where(r => r.Residence.User.UserName == username)
        .ToList();
        return new SelectList(userResidences, "Id", "Name");
    }
}

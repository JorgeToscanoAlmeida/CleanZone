using Microsoft.EntityFrameworkCore;

namespace CleanZone.Repositories;

public class AreaRepository
{
    private readonly ApplicationDbContext _ctx;
    private readonly ILogger<ResidenceRepository> _logger;
    public AreaRepository(ApplicationDbContext ctx, ILogger<ResidenceRepository> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }
    public SelectList ViewDataByName(string username)
    {
        var userResidences = _ctx.Residence
    .Where(r => r.User.UserName == username)
    .ToList();
        return new SelectList(userResidences, "Id", "Name");
    }
    public async Task<Area> GetByIdAsync(int? id)
    {
        return await _ctx.Area.Where(c => c.Id == id).FirstOrDefaultAsync();
    }
    public async Task DeleteByIdAsync(int? id)
    {
        int result = await _ctx.Area.Where(c => c.Id == id).ExecuteDeleteAsync();
    }
    public async Task UpdateAsync(Area area)
    {
        _ctx.Attach(area).State = EntityState.Modified;
        _ = await _ctx.SaveChangesAsync();
    }
    public async Task<Area> AddAreaAsync(Area area)
    {
        _ = _ctx.Area.Add(area);
        _ = await _ctx.SaveChangesAsync();
        return area;
    }
    public async Task<Area> FindAsync(int? id)
    {
        return await _ctx.Area.FindAsync(id);
    }
    public async Task<List<Area>> GetByNameAsync(string username)
    {
        if (_ctx.Area != null)
        {
            return await _ctx.Area
            .Where(r => r.Residence.User.UserName == username)
            .Include(a => a.Residence)
            .ToListAsync();
        }
        return null;
    }

}

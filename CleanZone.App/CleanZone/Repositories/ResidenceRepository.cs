using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanZone.Repositories;

public class ResidenceRepository
{
    private readonly ApplicationDbContext _ctx;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<ResidenceRepository> _logger;
    public ResidenceRepository(ApplicationDbContext ctx, ILogger<ResidenceRepository> logger, UserManager<IdentityUser> userManager)
    {
        _ctx = ctx;
        _logger = logger;
        _userManager = userManager;
    }
    public async Task<IList<Residence>> GetResidencesAsync(string username)
    {
        return await _ctx.Residence
                .Where(r => r.User.UserName == username)
                .Include(r => r.User)
                .ToListAsync();
    }
    public async Task<Residence> GetByIdAsync(int? id)
    {
        return await _ctx.Residence.Where(c => c.Id == id).FirstOrDefaultAsync();
    }
    public async Task DeleteByIdAsync(int? id)
    {
        int result = await _ctx.Residence.Where(c => c.Id == id).ExecuteDeleteAsync();
    }
    public async Task UpdateAsync(Residence residencia)
    {
        _ctx.Attach(residencia).State = EntityState.Modified;
        _ = await _ctx.SaveChangesAsync();
    }
    public async Task<Residence> AddResidencyAsync(Residence residencia)
    {
        _ = _ctx.Residence.Add(residencia);
        _ = await _ctx.SaveChangesAsync();
        return residencia;
    }
    public SelectList ViewDataByName(string username)
    {
        return new SelectList(_ctx.Users.Where(u => u.UserName == username), "Id", "UserName");
    }
    public async Task<Residence> FindAsync(int? id)
    {
        return await _ctx.Residence.FindAsync(id);
    }
    public async Task<List<Residence>> GetByNameAsync(string username)
    {
        if (!string.IsNullOrEmpty(username))
        {
            return await _ctx.Residence
            .Where(r => r.User.UserName == username) // Filtra por nome de usuário
            .Include(r => r.User)
            .ToListAsync();
        }
        return null;
    }
    public async Task<string> ObterIdDoUsuario(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user != null)
        {
            return user.Id;
        }

        return null;
    }
}

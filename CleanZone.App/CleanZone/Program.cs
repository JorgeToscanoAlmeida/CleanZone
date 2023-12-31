using CleanZone.Repositories;
using CleanZone.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("CleanDbConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
//Os meus Servicos/////////////////////////////////////////
builder.Services.AddSingleton<DateService>();
builder.Services.AddScoped<ResidenceRepository>();
builder.Services.AddScoped<AreaRepository>();
builder.Services.AddScoped<DivisionRepositoy>();
builder.Services.AddScoped<ImportRepository>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<StatisticsViewModelRepository>();
/////////////////////////////////////////////////////////////
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

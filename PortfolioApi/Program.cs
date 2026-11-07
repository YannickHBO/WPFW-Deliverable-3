using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;
using PortfolioApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Data: EF Core DbContext against SQL Server (scoped: one instance per HTTP request).
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioDb")));

// Application logic: services injected into the controllers via constructor injection.
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();

// HTTP layer + Swagger (Swashbuckle.AspNetCore).
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply pending EF Core migrations on startup, so the database and tables exist.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;

namespace WeatherAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

}
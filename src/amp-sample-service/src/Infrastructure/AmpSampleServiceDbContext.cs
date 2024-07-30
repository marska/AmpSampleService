using AmpSampleService.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AmpSampleService.Infrastructure;

public class AmpSampleServiceDbContext : IdentityDbContext<IdentityUser>
{
    public AmpSampleServiceDbContext(DbContextOptions<AmpSampleServiceDbContext> options)
        : base(options) { }

    public DbSet<AiDbModel> Ais { get; set; }
}

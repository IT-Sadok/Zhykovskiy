using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public sealed class ApplicationContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.Migrate();
    }
}
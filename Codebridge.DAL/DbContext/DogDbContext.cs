using Codebridge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codebridge.DbContext;

public class DogDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DogDbContext(DbContextOptions<DogDbContext> options) : base(options)
    {
    }

    public DbSet<Dog> Dogs { get; set; }
}
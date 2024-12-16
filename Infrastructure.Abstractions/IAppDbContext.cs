using CSharpClicker.Domain;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Infrastructure.Abstractions;

public interface IAppDbContext
{
    DbSet<ApplicationRole> ApplicationRoles { get; }

    DbSet<ApplicationUser> ApplicationUsers { get; }

    DbSet<Boost> Boosts { get; }

    DbSet<UserBoost> UserBoosts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

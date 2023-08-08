using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TechWizMain.Areas.Identity.Data;

namespace TechWizMain.Data;

public class UserManagerContext : IdentityDbContext<UserManager>
{
    public UserManagerContext(DbContextOptions<UserManagerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName(); //B2: Lay ten cua table
            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<UserManager>
{
    public void Configure(EntityTypeBuilder<UserManager> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(50);
        builder.Property(u => u.LastName).HasMaxLength(50);
        builder.Property(u => u.UserName).HasMaxLength(50);
        builder.Property(u => u.PhoneNumber).HasMaxLength(13);
    }
}
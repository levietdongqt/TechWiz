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
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName(); //B2: Lay ten cua table
            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
        //builder.Entity<UserManager>(entity =>
        //{
        //    entity.Property(e => e.FirstName).null
        //});
    }
}
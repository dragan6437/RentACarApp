using Domain.Models;
using Domain.Models.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Context
{
    public class RentACarDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RentACarDbContext(DbContextOptions<RentACarDbContext> options, 
                                 IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableBaseEntry && (
                            e.State == EntityState.Added
                            || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if(entityEntry.State == EntityState.Added)
                {
                    ((AuditableBaseEntry)entityEntry.Entity).CreatedOn = DateTime.UtcNow;
                    ((AuditableBaseEntry)entityEntry.Entity).CreatedBy = this._httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Guest";
                }
                else
                {
                    ((AuditableBaseEntry)entityEntry.Entity).UpdatedOn = DateTime.UtcNow;
                    ((AuditableBaseEntry)entityEntry.Entity).UpdatedBy = this._httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Guest";
                }
            }

            return base.SaveChanges();
        }
    }
}

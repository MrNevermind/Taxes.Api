using Microsoft.EntityFrameworkCore;
using Taxes.Core.Tables;

namespace Taxes.Core
{
    public class TaxesContext : DbContext
    {
        public DbSet<TaxTable> Taxes { get; set; }
        public DbSet<MunicipalityTable> Municipalities { get; set; }

        public const string DefaultSchema = "dbo";
        private readonly string connectionString;

        public TaxesContext()
        {
            this.connectionString = Settings.ConnectionString;
        }

        public TaxesContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public TaxesContext(DbContextOptions options) : base(options) { }

        public static TaxesContext CreateContext()
        {
            return new TaxesContext(Settings.ConnectionString);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(connectionString,
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", DefaultSchema));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchema);

            modelBuilder.Entity<TaxTable>().HasIndex(x => x.MunicipalityId);
            modelBuilder.Entity<TaxTable>().HasIndex(x => x.StartDate);
            modelBuilder.Entity<TaxTable>().HasIndex(x => x.EndDate);

            base.OnModelCreating(modelBuilder);
        }
    }
}

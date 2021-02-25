using Microsoft.EntityFrameworkCore;
using Taxes.Core.Tables;

namespace Taxes.Core
{
    public class TaxesContext : DbContext
    {
        public DbSet<TaxTable> Taxes { get; set; }

        public const string DefaultSchema = "dbo";
        private readonly string connectionString;

        public TaxesContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public TaxesContext(DbContextOptions options) : base(options) { }

        public static TaxesContext CreateContext()
        {
            return new TaxesContext("Server=tcp:edvinas.database.windows.net,1433;Initial Catalog=Taxes;Persist Security Info=False;User ID=edvinas;Password=SL4pt4z0d1$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
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

            modelBuilder.Entity<TaxTable>().HasIndex(x => x.Municipality);
            modelBuilder.Entity<TaxTable>().HasIndex(x => x.StartDate);
            modelBuilder.Entity<TaxTable>().HasIndex(x => x.EndDate);

            base.OnModelCreating(modelBuilder);
        }
    }
}

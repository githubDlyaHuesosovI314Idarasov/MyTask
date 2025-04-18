using Microsoft.EntityFrameworkCore;
using Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Areas.AspNet.Identity.Data;
using DAL.EF.Configurations;


namespace CompanyDAL.EF
{
    public partial class UrlDbContext : IdentityDbContext<ApplicationUser>
    {
        public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=URLDatabase;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");
        }

        public virtual DbSet<ShortUrl> ShortUrls { get; set; } = null!;
        public virtual DbSet<ShortUrlInfo> ShortUrlInfos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ShortUrlConfiguration());


        }

    }
}

using EvaluacionDesempenoApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace EvaluacionDesempenoApi.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApplicationDbContext> _logger;

        public DbSet<QuestionType> QuestionType { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Employees> Employees { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
       : base(options)
        {
            _configuration = configuration;
            ChangeTracker.LazyLoadingEnabled = true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configura la cadena de conexión a tu base de datos PostgreSQL
            try
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
            catch (Exception e)
            {
                _logger.LogError("OnConfiguring" + e.Message);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Employees>()
                .HasOne(e => e.Responsible)
                .WithMany(e => e.Subordinates)
                .HasForeignKey(e => e.IDResponsible);

                modelBuilder.Entity<Employees>()
                  .HasOne(d => d.Positions)
                  .WithMany(t => t.Employees)
                  .HasForeignKey(d => d.IdPosition);

                modelBuilder.Entity<Employees>()
                  .HasOne(d => d.Divisions)
                  .WithMany(t => t.Employees)
                  .HasForeignKey(d => d.CdDivisions);

                modelBuilder.Entity<Questions>()
                   .HasOne(d => d.QuestionType)
                   .WithMany(t => t.Questions)
                   .HasForeignKey(d => d.IdQuestionType);

                modelBuilder.Entity<Questions>()
                  .HasOne(c => c.Formats)
                  .WithOne(ic => ic.Questions)
                  .HasForeignKey<Formats>(i => i.IdFormats);

               modelBuilder.Entity<QuestionGroupRelation>()
                .HasOne(m => m.Questions)
                .WithMany(e => e.QuestionGroupRelations)
                .HasForeignKey(m => m.IdQuestions);

                modelBuilder.Entity<QuestionGroupRelation>()
                    .HasOne(m => m.Groups)
                    .WithMany(c => c.QuestionGroupRelations)
                    .HasForeignKey(m => m.IdGroups);
            }
            catch (Exception ex)
            {

                _logger.LogError("OnModelCreating: " + ex.Message);
            }
        }
    }
}

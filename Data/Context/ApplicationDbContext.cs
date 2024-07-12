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
        public DbSet<Questionaries> Questionaries { get; set; }
        public DbSet<EscalesValues> EscalesValues { get; set; }
        public DbSet<Escales> Escales { get; set; }
        public DbSet<QuestionariesConfig> QuestionariesConfig { get; set; }
        public DbSet<Areas> Areas { get; set; }
        public DbSet<Frequencies> Frequencies { get; set; }
        public DbSet<Evaluations> Evaluations { get; set; }
        public DbSet<EvaluationsPositions> EvaluationsPositions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
       : base(options)
        {
            _configuration = configuration;
            ChangeTracker.LazyLoadingEnabled = true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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

                modelBuilder.Entity<EvaluationsPositions>()
                   .HasOne(m => m.Evaluations)
                   .WithMany(e => e.EvaluationsPositions)
                   .HasForeignKey(m => m.IdEvaluations);

                modelBuilder.Entity<EvaluationsPositions>()
                    .HasOne(m => m.Positions)
                    .WithMany(c => c.EvaluationsPositions)
                    .HasForeignKey(m => m.IdPosition);

                modelBuilder.Entity<Evaluations>()
                   .HasOne(m => m.Employees)
                   .WithMany(e => e.Questionaries)
                   .HasForeignKey(m => m.IDProcessLeader);

                modelBuilder.Entity<Evaluations>()
                  .HasOne(d => d.Questionaries)
                  .WithMany(t => t.Evaluations)
                  .HasForeignKey(d => d.IdQuestionary);

                modelBuilder.Entity<Evaluations>()
                  .HasOne(d => d.Divisions)
                  .WithMany(t => t.Evaluations)
                  .HasForeignKey(d => d.CdDivisions);

                modelBuilder.Entity<Evaluations>()
                  .HasOne(e => e.Escales)
                  .WithMany(es => es.Evaluations)
                  .HasForeignKey(e => e.IdEscales);

                modelBuilder.Entity<EscalesValues>()
                  .HasOne(e => e.Escales)
                  .WithMany(e => e.EscalesValues)
                  .HasForeignKey(e => e.IdEscales);


                modelBuilder.Entity<QuestionariesConfig>()
                    .HasOne(m => m.Frequencies)
                    .WithMany(e => e.QuestionariesConfig)
                    .HasForeignKey(m => m.Cdfrequency);

                modelBuilder.Entity<QuestionariesConfig>()
                    .HasOne(m => m.Questions)
                    .WithMany(e => e.QuestionariesConfig)
                    .HasForeignKey(m => m.IdQuestions);

                modelBuilder.Entity<QuestionariesConfig>()
                    .HasOne(m => m.Questionary)
                    .WithMany(c => c.QuestionariesConfig)
                    .HasForeignKey(m => m.IdQuestionary);

               modelBuilder.Entity<Questionaries>()
                  .HasOne(m => m.Area)
                  .WithMany(e => e.Questionaries)
                  .HasForeignKey(m => m.CdArea);


                modelBuilder.Entity<Questionaries>()
                    .HasOne(e => e.QuestionaryTypes)
                    .WithMany(e => e.Questionaries)
                    .HasForeignKey(e => e.CdQuestionaryType);

                
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
                  .HasOne(d => d.Areas)
                  .WithMany(t => t.Questions)
                  .HasForeignKey(d => d.CdArea);

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

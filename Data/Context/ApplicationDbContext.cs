using EvaluacionDesempenoApi.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace EvaluacionDesempenoApi.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
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
        public DbSet<EvaluationRecord> EvaluationRecord { get; set; }
        public DbSet<EvaluationStates> EvaluationStates { get; set; }
        public DbSet<RecordDetails> RecordDetails { get; set; }
        public DbSet<RecordDetailsTemp> RecordDetailsTemp { get; set; }
        public DbSet<KpiRecordDetails> KpiRecordDetails { get; set; }
        public DbSet<FileTypes> FileTypes { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<Flags> Flags { get; set; }
        public DbSet<FlagRules> FlagRules { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<FlagTypes> FlagTypes { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Profiles> Profiles { get; set; }
        public DbSet<UsersProfiles> UsersProfiles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public ApplicationDbContext() : base(new DbContextOptions<ApplicationDbContext>())
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                base.OnModelCreating(modelBuilder);



                modelBuilder.Entity<UsersProfiles>()
                  .HasOne(m => m.Profiles)
                  .WithMany(e => e.UsersProfiles)
                  .HasForeignKey(m => m.CdProfile);

                modelBuilder.Entity<UsersProfiles>()
                    .HasOne(m => m.User)
                    .WithMany(c => c.UsersProfiles)
                    .HasForeignKey(m => m.IdUser);


                /* modelBuilder.Entity<ApplicationUser>()
                    .HasOne(m => m.Employees)
                    .WithOne(e => e.ApplicationUser)
                     .HasForeignKey<Employees>(i => i.IdEmployees);*/

                modelBuilder.Entity<ApplicationUser>()
                    .HasOne(u => u.Employees)
                    .WithOne()
                    .HasForeignKey<ApplicationUser>(u => u.IdEmployee)  // La clave foránea en ApplicationUser
                    .OnDelete(DeleteBehavior.SetNull);

                modelBuilder.Entity<ApplicationUser>()
                  .HasOne<Languages>()
                  .WithMany()
                  .HasForeignKey(u => u.CdLanguage);


                modelBuilder.Entity<Evaluations>()
                  .HasOne(m => m.Flags)
                  .WithMany(e => e.Evaluations)
                  .HasForeignKey(m => m.IdFlag);

                modelBuilder.Entity<FlagRules>()
                  .HasOne(e => e.Colors)
                  .WithMany(e => e.FlagRules)
                  .HasForeignKey(e => e.IdColor);

                modelBuilder.Entity<FlagRules>()
                   .HasOne(e => e.FlagTypes)
                   .WithMany(e => e.FlagRules)
                   .HasForeignKey(e => e.CdFlagType);

                modelBuilder.Entity<FlagRules>()
                .HasOne(m => m.Flags)
                .WithMany(e => e.FlagRules)
                .HasForeignKey(m => m.IdFlag);

                modelBuilder.Entity<Files>()
                   .HasOne(e => e.FileType)
                   .WithMany(e => e.Files)
                   .HasForeignKey(e => e.CdFileType);

                modelBuilder.Entity<KpiRecordDetails>()
                 .HasOne(m => m.EvaluationRecord)
                 .WithMany(e => e.KpiRecordDetails)
                 .HasForeignKey(m => m.IdEvaluationRecord);

                modelBuilder.Entity<RecordDetails>()
                 .HasOne(m => m.EvaluationRecord)
                 .WithMany(e => e.RecordDetails)
                 .HasForeignKey(m => m.IdEvaluationRecord);

                modelBuilder.Entity<RecordDetailsTemp>()
                 .HasOne(m => m.EvaluationRecord)
                 .WithMany(e => e.RecordDetailsTemp)
                 .HasForeignKey(m => m.IdEvaluationRecord);

                modelBuilder.Entity<EvaluationRecord>()
                 .HasOne(m => m.EvaluationStates)
                 .WithMany(e => e.EvaluationRecords)
                 .HasForeignKey(m => m.CdEvaluationStates);

                modelBuilder.Entity<EvaluationRecord>()
                 .HasOne(m => m.Evaluation)
                 .WithMany(e => e.EvaluationRecords)
                 .HasForeignKey(m => m.IdEvaluations);

                modelBuilder.Entity<EvaluationRecord>()
             .HasOne(er => er.Evaluator)
             .WithMany(e => e.EvaluationsAsEvaluator)
             .HasForeignKey(er => er.IdEvaluator); // O la opción que prefieras

                modelBuilder.Entity<EvaluationRecord>()
                    .HasOne(er => er.Employee)
                    .WithMany(e => e.EvaluationsAsEmployee)
                    .HasForeignKey(er => er.IdEmployee);

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

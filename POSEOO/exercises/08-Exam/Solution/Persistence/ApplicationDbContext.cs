namespace Persistence;

using Core.Entities;

using Microsoft.EntityFrameworkCore;

using Base.Tools;
using System.Diagnostics;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() : base()
    {
        //We need this constructor for migration
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Examinee>             Examinee             => Set<Examinee>();
    public DbSet<ExamineeExamQuestion> ExamineeExamQuestion => Set<ExamineeExamQuestion>();
    public DbSet<ExamQuestion>         ExamQuestion         => Set<ExamQuestion>();
    public DbSet<Exam>                 Exam                 => Set<Exam>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //We need this for migration
            string connectionString = ConfigurationHelper.GetConfiguration().Get("DefaultConnection", "ConnectionStrings");
            optionsBuilder.UseSqlServer(connectionString);
        }
        optionsBuilder.LogTo(message => Debug.WriteLine(message));
}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ExamineeExamQuestion>()
            .HasOne(p => p.ExamQuestion).WithMany()
            .HasForeignKey(p => p.ExamQuestionId)
            .OnDelete(DeleteBehavior.ClientNoAction);
    }
}
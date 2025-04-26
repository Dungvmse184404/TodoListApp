using System;
using System.Collections.Generic;
using Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.Database;

public partial class TodoListAppDbContext : DbContext
{
    public TodoListAppDbContext()
    {
    }

    public TodoListAppDbContext(DbContextOptions<TodoListAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DailyTask> DailyTasks { get; set; }

    public virtual DbSet<Label> Labels { get; set; }

    public virtual DbSet<SubTask> SubTasks { get; set; }

    public virtual DbSet<TodoTask> TodoTasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
    }

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DailyTask>(entity =>
        {
            entity.HasKey(e => e.DailyTasksId).HasName("PK__DailyTas__7C41DA0DAC15C028");

            entity.Property(e => e.DailyTasksId).HasColumnName("DailyTasksID");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<Label>(entity =>
        {
            entity.HasKey(e => e.LabelId).HasName("PK__Labels__397E2BA398FE3ABF");

            entity.Property(e => e.LabelId).HasColumnName("LabelID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LabelName).HasMaxLength(100);
        });

        modelBuilder.Entity<SubTask>(entity =>
        {
            entity.HasKey(e => e.SubTaskId).HasName("PK__SubTasks__869FF1A2B3AD1F4C");

            entity.Property(e => e.SubTaskId).HasColumnName("SubTaskID");
            entity.Property(e => e.IsCompleted).HasDefaultValue(false);
            entity.Property(e => e.TodoTaskId).HasColumnName("TodoTaskID");

            entity.HasOne(d => d.TodoTask).WithMany(p => p.SubTasks)
                .HasForeignKey(d => d.TodoTaskId)
                .HasConstraintName("FK_SubTasks_TodoTasks");
        });

        modelBuilder.Entity<TodoTask>(entity =>
        {
            entity.HasKey(e => e.TodoTaskId).HasName("PK__TodoTask__B9D813450FEE1091");

            entity.Property(e => e.TodoTaskId).HasColumnName("TodoTaskID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.LabelId).HasColumnName("LabelID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Label).WithMany(p => p.TodoTasks)
                .HasForeignKey(d => d.LabelId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_TodoTasks_Labels");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

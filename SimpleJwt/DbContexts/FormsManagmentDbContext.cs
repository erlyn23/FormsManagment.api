using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SimpleJwt.Models.Entities;

#nullable disable

namespace SimpleJwt.DbContexts
{
    public partial class FormsManagmentDbContext : DbContext
    {
        public FormsManagmentDbContext()
        {
        }

        public FormsManagmentDbContext(DbContextOptions<FormsManagmentDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionOption> QuestionOptions { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFormAnswer> UserFormAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasIndex(e => e.QuestionId, "IX_Answers_QuestionId");

                entity.Property(e => e.Answer1)
                    .IsUnicode(false)
                    .HasColumnName("Answer");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId);
            });

            modelBuilder.Entity<Form>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Forms_UserId");

                entity.HasIndex(e => e.Name, "Ix_Forms_Name")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Forms");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasIndex(e => e.FormId, "IX_Questions_FormId");

                entity.HasIndex(e => e.QuestionTypeId, "IX_Questions_QuestionTypeId");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.FormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_Forms");

                entity.HasOne(d => d.QuestionType)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuestionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_QuestionTypes");
            });

            modelBuilder.Entity<QuestionOption>(entity =>
            {
                entity.HasIndex(e => e.QuestionId, "IX_QuestionOptions_QuestionId");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionOptions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionOptions_QuestionId");
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.HasIndex(e => e.QuestionType1, "Ix_QuestionTypes_QuestionType")
                    .IsUnique();

                entity.Property(e => e.QuestionType1)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("QuestionType");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.ProfilePhoto)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserFormAnswer>(entity =>
            {
                entity.HasKey(e => new { e.UserFormAnswerId, e.UserId, e.FormId });

                entity.Property(e => e.UserFormAnswerId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.UserFormAnswers)
                    .HasForeignKey(d => d.FormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFormAnswers_Forms");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFormAnswers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFormAnswers_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

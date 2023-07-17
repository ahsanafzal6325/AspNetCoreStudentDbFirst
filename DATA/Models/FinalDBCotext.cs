using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DATA.Models
{
    public partial class FinalDBCotext : DbContext
    {
        public FinalDBCotext()
        {
        }

        public FinalDBCotext(DbContextOptions<FinalDBCotext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentSubject> StudentSubject { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;User Id=ahsan;Password=1234;Database=FinalDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StudentSubject>(entity =>
            {
                entity.HasKey(e => e.StudSubId)
                    .HasName("PK__StudentS__C867B722FCD28855");

                entity.HasOne(d => d.Stud)
                    .WithMany(p => p.StudentSubject)
                    .HasForeignKey(d => d.StudId)
                    .HasConstraintName("FK__StudentSu__StudI__1332DBDC");

                entity.HasOne(d => d.Sub)
                    .WithMany(p => p.StudentSubject)
                    .HasForeignKey(d => d.SubId)
                    .HasConstraintName("FK__StudentSu__SubId__14270015");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.SubId)
                    .HasName("PK__Subject__4D9BB84A65B4EA02");

                entity.Property(e => e.SubId).ValueGeneratedNever();

                entity.Property(e => e.SubName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.TContact).HasMaxLength(50);

                entity.Property(e => e.TName).HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

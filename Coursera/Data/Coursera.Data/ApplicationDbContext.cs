namespace Coursera.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Coursera.Data.Common.Models;
    using Coursera.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//           => optionsBuilder.UseSqlServer("Server=.;Database=coursera;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");


        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Instructor> Instructors { get; set; }

        public virtual DbSet<Setting> Settings { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<StudentsCoursesXref> StudentsCoursesXrefs { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Course>(entity =>
            {
                entity.ToTable("courses");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Credit).HasColumnName("credit");
                entity.Property(e => e.InstructorId).HasColumnName("instructor_id");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("name");
                entity.Property(e => e.TimeCreated)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("time_created");
                entity.Property(e => e.TotalTime).HasColumnName("total_time");

                entity.HasOne(d => d.Instructor).WithMany(p => p.Courses)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_courses_instructors");
            });

            builder.Entity<Instructor>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_teachers");

                entity.ToTable("instructors");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("first_name");
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("last_name");
                entity.Property(e => e.TimeCreated)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("time_created");
            });

            builder.Entity<Setting>(entity =>
            {
                entity.HasIndex(e => e.IsDeleted, "IX_Settings_IsDeleted");
            });

            builder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Pin);

                entity.ToTable("students");

                entity.Property(e => e.Pin)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("pin");
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_name");
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("last_name");
                entity.Property(e => e.TimeCreated)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("time_created");
            });

            builder.Entity<StudentsCoursesXref>(entity =>
            {
                entity.HasKey(e => new { e.StudentPin, e.CourseId });

                entity.ToTable("students_courses_xref");

                entity.Property(e => e.StudentPin)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("student_pin");
                entity.Property(e => e.CourseId).HasColumnName("course_id");
                entity.Property(e => e.CompletionDate)
                    .HasColumnType("date")
                    .HasColumnName("completion_date");

                entity.HasOne(d => d.Course).WithMany(p => p.StudentsCoursesXrefs)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_students_courses_xref_courses");

                entity.HasOne(d => d.StudentPinNavigation).WithMany(p => p.StudentsCoursesXrefs)
                    .HasForeignKey(d => d.StudentPin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_students_courses_xref_students");
            });

            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }



        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}

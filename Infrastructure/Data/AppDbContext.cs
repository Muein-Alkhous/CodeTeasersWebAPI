using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Description> Descriptions { get; set; }
    public virtual DbSet<Problem> Problems { get; set; }
    public virtual DbSet<ProblemCategory> ProblemCategories { get; set; }
    public virtual DbSet<Submission> Submissions { get; set; }
    public virtual DbSet<Template> Templates { get; set; }
    public virtual DbSet<Test> Tests { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserStatus> UserStatuses { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // // Apply soft delete filter to all entities inheriting BaseEntity
        // foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        // {
        //     if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
        //     {
        //         modelBuilder.Entity(entityType.ClrType)
        //             .HasQueryFilter(GetIsDeletedRestriction(entityType.ClrType));
        //     }
        // }
        
        modelBuilder.Entity<Category>(entity =>
        {
            entity
                .HasKey(e => e.Id)
                .HasName("categories_pkey");

            entity.ToTable("categories");

            entity.HasIndex(e => e.Title, "uq_categories_title").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<Description>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("descriptions_pkey");

            entity.ToTable("descriptions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Problem).WithOne(p => p.Description)
                .HasForeignKey<Description>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("descriptions_id_fkey");
        });

        modelBuilder.Entity<Problem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("problems_pkey");

            entity.ToTable("problems");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Difficulty)
                .HasColumnName("difficulty")
                .HasMaxLength(20);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<ProblemCategory>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("problem_categories_pkey");
            
            entity.ToTable("problem_categories");
            
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryId)
                .HasColumnName("category_id");
            entity.Property(e => e.ProblemId)
                .HasColumnName("problem_id");
            entity.HasOne(d => d.Category)
                .WithMany(p => p.ProblemCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("fk_problem_categories_category_id");

            entity.HasOne(d => d.Problem)
                .WithMany(p => p.ProblemCategories)
                .HasForeignKey(d => d.ProblemId)
                .HasConstraintName("fk_problem_categories_problem_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("submissions_pkey");

            entity.ToTable("submissions");

            entity.HasIndex(e => new { e.ProblemId, e.UserId, e.Language }, "uq_submissions_language_problem_id_user_id").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Language)
                .HasMaxLength(15)
                .HasDefaultValueSql("'python'::character varying")
                .HasColumnName("language");
            entity.Property(e => e.PassedTests).HasColumnName("passed_tests");
            entity.Property(e => e.Points).HasColumnName("points");
            entity.Property(e => e.ProblemId).HasColumnName("problem_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Problem).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.ProblemId)
                .HasConstraintName("fk_submissions_problem_id");

            entity.HasOne(d => d.User).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_submissions_user_id");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("templates_pkey");

            entity.ToTable("templates");

            entity.HasIndex(e => new { e.ProblemId, e.Language }, "uq_templates_problem_id_language").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Language)
                .HasMaxLength(15)
                .HasDefaultValueSql("'python'::character varying")
                .HasColumnName("language");
            entity.Property(e => e.ProblemId).HasColumnName("problem_id");
            entity.Property(e => e.TemplatePath).HasColumnName("template_path");

            entity.HasOne(d => d.Problem).WithMany(p => p.Templates)
                .HasForeignKey(d => d.ProblemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("templates_problem_id_fkey");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tests_pkey");

            entity.ToTable("tests");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Language)
                .HasMaxLength(15)
                .HasDefaultValueSql("'python'::character varying")
                .HasColumnName("language");
            entity.Property(e => e.NumCases).HasColumnName("num_cases");
            entity.Property(e => e.ProblemId).HasColumnName("problem_id");
            entity.Property(e => e.TestPath).HasColumnName("test_path");

            entity.HasOne(d => d.Problem).WithMany(p => p.Tests)
                .HasForeignKey(d => d.ProblemId)
                .HasConstraintName("fk_tests_problem");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "uq_users_email").IsUnique();

            entity.HasIndex(e => e.Username, "uq_users_username").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasComment("Hashed password")
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_statuses_pkey");

            entity.ToTable("user_statuses");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Points)
                .HasDefaultValue(0)
                .HasColumnName("points");
            entity.Property(e => e.Rank)
                .HasColumnName("rank")
                .HasMaxLength(20);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            
            entity.HasOne(d => d.IdNavigation).WithOne(p => p.UserStatus)
                .HasForeignKey<UserStatus>(d => d.Id)
                .HasConstraintName("fk_user_statuses_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

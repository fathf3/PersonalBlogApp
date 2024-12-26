using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BlogApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Data
{
    public class BlogDbContext : IdentityDbContext<Author>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Certificate> Certificates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between BlogPost and Tag
            modelBuilder.Entity<BlogPost>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.BlogPosts)
                .UsingEntity(j => j.ToTable("BlogPostTags"));

            // Configure one-to-many relationship between Author and BlogPost
            modelBuilder.Entity<BlogPost>()
                .HasOne(p => p.Author)
                .WithMany(a => a.BlogPosts)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relationships for Resume
            modelBuilder.Entity<Resume>()
                .HasOne(r => r.Author)
                .WithOne()
                .HasForeignKey<Resume>(r => r.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationships for WorkExperience
            modelBuilder.Entity<WorkExperience>()
                .HasOne(w => w.Resume)
                .WithMany(r => r.WorkExperiences)
                .HasForeignKey(w => w.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationships for Education
            modelBuilder.Entity<Education>()
                .HasOne(e => e.Resume)
                .WithMany(r => r.Education)
                .HasForeignKey(e => e.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationships for Skill
            modelBuilder.Entity<Skill>()
                .HasOne(s => s.Resume)
                .WithMany(r => r.Skills)
                .HasForeignKey(s => s.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationships for Project
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Resume)
                .WithMany(r => r.Projects)
                .HasForeignKey(p => p.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationships for Certificate
            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.Resume)
                .WithMany(r => r.Certificates)
                .HasForeignKey(c => c.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed test user
            var hasher = new PasswordHasher<Author>();
            var testUser = new Author
            {
                Id = "1",
                UserName = "test@example.com",
                NormalizedUserName = "TEST@EXAMPLE.COM",
                Email = "test@example.com",
                NormalizedEmail = "TEST@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "Test",
                LastName = "User"
            };
            testUser.PasswordHash = hasher.HashPassword(testUser, "Test123!");

            modelBuilder.Entity<Author>().HasData(testUser);
        }
    }
}
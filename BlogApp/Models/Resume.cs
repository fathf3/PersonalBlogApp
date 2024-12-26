using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public string AuthorId { get; set; } = string.Empty;
        public Author? Author { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Summary { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }
        public string? Website { get; set; }
        public string? LinkedIn { get; set; }
        public string? GitHub { get; set; }
        public string? ProfileImage { get; set; }

        public List<WorkExperience> WorkExperiences { get; set; } = new();
        public List<Education> Education { get; set; } = new();
        public List<Skill> Skills { get; set; } = new();
        public List<Project> Projects { get; set; } = new();
        public List<Certificate> Certificates { get; set; } = new();
    }

    public class WorkExperience
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public Resume? Resume { get; set; }

        [Required]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public string Position { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? CompanyUrl { get; set; }
        public string? Location { get; set; }
    }

    public class Education
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public Resume? Resume { get; set; }

        [Required]
        public string InstitutionName { get; set; } = string.Empty;

        [Required]
        public string Degree { get; set; } = string.Empty;

        [Required]
        public string FieldOfStudy { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public string? Grade { get; set; }
        public string? Activities { get; set; }
    }

    public class Skill
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public Resume? Resume { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Range(1, 5)]
        public int ProficiencyLevel { get; set; }
    }

    public class Project
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public Resume? Resume { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? ProjectUrl { get; set; }
        public string? GitHubUrl { get; set; }

        [Required]
        public string Technologies { get; set; } = string.Empty;

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class Certificate
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public Resume? Resume { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string IssuingOrganization { get; set; } = string.Empty;

        [Required]
        public DateTime IssueDate { get; set; }

        public DateTime? ExpiryDate { get; set; }
        public string? CredentialUrl { get; set; }
        public string? CredentialId { get; set; }
    }
}
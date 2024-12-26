using System.ComponentModel.DataAnnotations;
using BlogApp.Models;

namespace BlogApp.ViewModels
{
    public class ResumeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ad Soyad 2-100 karakter arasında olmalıdır.")]
        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Ünvan en fazla 100 karakter olabilir.")]
        [Display(Name = "Ünvan")]
        public string? Title { get; set; }

        [StringLength(1000, ErrorMessage = "Özet en fazla 1000 karakter olabilir.")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [Display(Name = "Özet")]
        public string? Summary { get; set; }

        [StringLength(100, ErrorMessage = "Konum en fazla 100 karakter olabilir.")]
        [Required(ErrorMessage = "Konum alanı zorunludur.")]
        [Display(Name = "Konum")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir.")]
        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }

        [Url(ErrorMessage = "Geçerli bir website adresi giriniz.")]
        [StringLength(255, ErrorMessage = "Website adresi en fazla 255 karakter olabilir.")]
        [Required(ErrorMessage = "Website alanı zorunludur.")]
        [Display(Name = "Website")]
        public string? Website { get; set; }

        [Url(ErrorMessage = "Geçerli bir LinkedIn profil adresi giriniz.")]
        [StringLength(255, ErrorMessage = "LinkedIn adresi en fazla 255 karakter olabilir.")]
        [Display(Name = "LinkedIn")]
        public string? LinkedIn { get; set; }

        [Url(ErrorMessage = "Geçerli bir GitHub profil adresi giriniz.")]
        [StringLength(255, ErrorMessage = "GitHub adresi en fazla 255 karakter olabilir.")]
        [Required(ErrorMessage = "GitHub alanı zorunludur.")]
        [Display(Name = "GitHub")]
        public string? GitHub { get; set; }

        [Url(ErrorMessage = "Geçerli bir profil resmi URL'si giriniz.")]
        [StringLength(1000, ErrorMessage = "Profil resmi URL'si en fazla 1000 karakter olabilir.")]
        [Required(ErrorMessage = "Profil resmi alanı zorunludur.")]
        [Display(Name = "Profil Resmi")]
        public string? ProfileImage { get; set; }

        public List<WorkExperienceViewModel> WorkExperiences { get; set; } = new();

        public List<Education> Education { get; set; } = new();

        public List<Skill> Skills { get; set; } = new();

        public List<Project> Projects { get; set; } = new();

        public List<Certificate> Certificates { get; set; } = new();
    }

    public class WorkExperienceViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Şirket adı zorunludur.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Şirket adı 2-100 karakter arasında olmalıdır.")]
        [Display(Name = "Şirket Adı")]

        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pozisyon zorunludur.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Pozisyon 2-100 karakter arasında olmalıdır.")]
        [Display(Name = "Pozisyon")]
        public string Position { get; set; } = string.Empty;

        [Required(ErrorMessage = "Başlangıç tarihi zorunludur.")]
        [Display(Name = "Başlangıç Tarihi")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Bitiş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Açıklama 10-2000 karakter arasında olmalıdır.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Şirket Web Sitesi")]
        [Url(ErrorMessage = "Geçerli bir web sitesi adresi giriniz.")]
        [StringLength(255, ErrorMessage = "Web sitesi adresi en fazla 255 karakter olabilir.")]
        public string? CompanyUrl { get; set; }

        [Display(Name = "Konum")]
        [StringLength(100, ErrorMessage = "Konum en fazla 100 karakter olabilir.")]
        public string? Location { get; set; }
    }

    public class EducationViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Institution Name")]
        public string InstitutionName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Degree")]
        public string Degree { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Field of Study")]
        public string FieldOfStudy { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Grade/GPA")]
        public string? Grade { get; set; }

        [Display(Name = "Activities and Societies")]
        public string? Activities { get; set; }
    }

    public class SkillViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Skill Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; } = string.Empty;

        [Range(1, 5)]
        [Display(Name = "Proficiency Level (1-5)")]
        public int ProficiencyLevel { get; set; }
    }

    public class ProjectViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Project URL")]
        [Url]
        public string? ProjectUrl { get; set; }

        [Display(Name = "GitHub Repository")]
        [Url]
        public string? GitHubUrl { get; set; }

        [Required]
        [Display(Name = "Technologies Used")]
        public string Technologies { get; set; } = string.Empty;

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }

    public class CertificateViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Certificate Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Issuing Organization")]
        public string IssuingOrganization { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Issue Date")]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }

        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "Credential URL")]
        [Url]
        public string? CredentialUrl { get; set; }

        [Display(Name = "Credential ID")]
        public string? CredentialId { get; set; }
    }
}
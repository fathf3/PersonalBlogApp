using BlogApp.Models;
using BlogApp.ViewModels;

namespace BlogApp.Services.Interfaces
{
    public interface IResumeService
    {
        Task<Resume?> GetResumeAsync();
        Task<Resume?> GetResumeByIdAsync(int id);
        Task<Resume?> GetResumeByUsernameAsync(string? username);
        Task<Resume?> GetResumeByUserIdAsync(string userId);

        Task<Resume> CreateResumeAsync(string userId, string fullName, string email);
        Task UpdateResumeAsync(Resume resume);
        Task<bool> IsUserAuthorizedToEditResume(string userId, int resumeId);

        // Work Experience
        Task<WorkExperience> AddWorkExperienceAsync(WorkExperience experience);
        Task DeleteWorkExperienceAsync(WorkExperience experience);
        Task<WorkExperience?> GetWorkExperienceByIdAsync(int id);

        // Education
        Task<Education> AddEducationAsync(Education education);
        Task DeleteEducationAsync(Education education);
        Task<Education?> GetEducationByIdAsync(int id);

        // Skills
        Task<Skill> AddSkillAsync(Skill skill);
        Task DeleteSkillAsync(int id, string userId);
        Task<Skill?> GetSkillByIdAsync(int id);

        // Projects
        Task<Project> AddProjectAsync(Project project);
        Task DeleteProjectAsync(int id, string userId);
        Task<Project?> GetProjectByIdAsync(int id);

        // Certificates
        Task<Certificate> AddCertificateAsync(Certificate certificate);
        Task DeleteCertificateAsync(int id, string userId);
        Task<Certificate?> GetCertificateByIdAsync(int id);
    }
}
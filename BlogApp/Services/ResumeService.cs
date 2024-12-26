using BlogApp.Data;
using BlogApp.Models;
using BlogApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Services
{
    public class ResumeService : IResumeService
    {
        private readonly BlogDbContext _context;

        public ResumeService(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<Resume?> GetResumeAsync()
        {
            return await _context.Resumes
                .Include(r => r.Author)
                .Include(r => r.WorkExperiences)
                .Include(r => r.Education)
                .Include(r => r.Skills)
                .Include(r => r.Projects)
                .Include(r => r.Certificates)
                .FirstOrDefaultAsync();
        }

        public async Task<Resume?> GetResumeByUsernameAsync(string? username)
        {
            return await _context.Resumes
                .Include(r => r.Author)
                .Include(r => r.WorkExperiences)
                .Include(r => r.Education)
                .Include(r => r.Skills)
                .Include(r => r.Projects)
                .Include(r => r.Certificates)
                .FirstOrDefaultAsync(r => r.Author!.UserName == username);
        }

        public async Task<Resume?> GetResumeByUserIdAsync(string userId)
        {
            return await _context.Resumes
                .Include(r => r.WorkExperiences)
                .Include(r => r.Education)
                .Include(r => r.Skills)
                .Include(r => r.Projects)
                .Include(r => r.Certificates)
                .FirstOrDefaultAsync(r => r.AuthorId == userId);
        }
        public async Task<Resume> CreateResumeAsync(string userId, string fullName, string email)
        {
            var resume = new Resume
            {
                AuthorId = userId,
                FullName = fullName,
                Email = email
            };

            _context.Resumes.Add(resume);
            await _context.SaveChangesAsync();
            return resume;
        }

        public async Task UpdateResumeAsync(Resume resume)
        {
            _context.Update(resume);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserAuthorizedToEditResume(string userId, int resumeId)
        {
            var resume = await _context.Resumes.FindAsync(resumeId);
            return resume?.AuthorId == userId;
        }

        // Work Experience Methods
        public async Task<WorkExperience> AddWorkExperienceAsync(WorkExperience experience)
        {
            _context.WorkExperiences.Add(experience);
            await _context.SaveChangesAsync();
            return experience;
        }

        public async Task DeleteWorkExperienceAsync(WorkExperience experience)
        {
            var _experience = await _context.WorkExperiences
                .Include(w => w.Resume)
                .FirstOrDefaultAsync(w => w.Id == experience.Id);

            if (_experience != null)
            {
                _context.WorkExperiences.Remove(_experience);
                await _context.SaveChangesAsync();
            }
        }

        // Education Methods
        public async Task<Education> AddEducationAsync(Education education)
        {
            _context.Education.Add(education);
            await _context.SaveChangesAsync();
            return education;
        }

        public async Task DeleteEducationAsync(Education education)
        {


            if (education != null)
            {
                _context.Education.Remove(education);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Education?> GetEducationByIdAsync(int id)
        {
            return await _context.Education

                .Include(e => e.Resume)

                .FirstOrDefaultAsync(e => e.Id == id);
        }


        // Skills Methods
        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            return skill;
        }

        public async Task DeleteSkillAsync(int id, string userId)
        {
            var skill = await _context.Skills
                .Include(s => s.Resume)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (skill != null && skill.Resume?.AuthorId == userId)
            {
                _context.Skills.Remove(skill);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Skill?> GetSkillByIdAsync(int id)
        {
            return await _context.Skills

               .Include(s => s.Resume)

               .FirstOrDefaultAsync(s => s.Id == id);

        }

        // Projects Methods
        public async Task<Project> AddProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task DeleteProjectAsync(int id, string userId)
        {
            var project = await _context.Projects
                .Include(p => p.Resume)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project != null && project.Resume?.AuthorId == userId)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            return await _context.Projects

                .Include(p => p.Resume)

                .FirstOrDefaultAsync(p => p.Id == id);

        }


        // Certificates Methods
        public async Task<Certificate> AddCertificateAsync(Certificate certificate)
        {
            _context.Certificates.Add(certificate);
            await _context.SaveChangesAsync();
            return certificate;
        }

        public async Task DeleteCertificateAsync(int id, string userId)
        {
            var certificate = await _context.Certificates
                .Include(c => c.Resume)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (certificate != null && certificate.Resume?.AuthorId == userId)
            {
                _context.Certificates.Remove(certificate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Resume?> GetResumeByIdAsync(int id)
        {
            return await _context.Resumes
                .Include(r => r.Author)
                .Include(r => r.WorkExperiences)
                .Include(r => r.Education)
                .Include(r => r.Skills)
                .Include(r => r.Projects)
                .Include(r => r.Certificates)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<WorkExperience?> GetWorkExperienceByIdAsync(int id)
        {
            var experience = await _context.WorkExperiences
                .Include(w => w.Resume)
                .FirstOrDefaultAsync(w => w.Id == id);
            return experience;
        }

        public async Task<Certificate?> GetCertificateByIdAsync(int id)
        {
            return await _context.Certificates
                .Include(c => c.Resume)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
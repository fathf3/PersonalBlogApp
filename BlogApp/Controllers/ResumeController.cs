using BlogApp.Data;

using BlogApp.Models;
using BlogApp.Services.Interfaces;
using BlogApp.ViewModels;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;



namespace BlogApp.Controllers

{

    public class ResumeController : Controller
    {

        private readonly BlogDbContext _context;
        private readonly IResumeService _resumeService;
        private readonly UserManager<Author> _userManager;

        public ResumeController(BlogDbContext context, IResumeService resumeService, UserManager<Author> userManager)
        {

            _context = context;
            _resumeService = resumeService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? username)
        {

            if (string.IsNullOrEmpty(username))
            {
                var resume = await _resumeService.GetResumeAsync();

                if (resume == null && User.Identity?.IsAuthenticated == true)
                {
                    return RedirectToAction(nameof(Edit));
                }

                return View(resume);
            }

            var userResume = await _resumeService.GetResumeByUsernameAsync(username);

            if (userResume == null)
            {
                if (User.Identity?.IsAuthenticated == true && username == User.Identity.Name)
                {
                    return RedirectToAction(nameof(Edit));
                }
                return NotFound();
            }



            return View(userResume);

        }



        [Authorize]
        public async Task<IActionResult> Edit()
        {

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {

                return NotFound();

            }

            var resume = await _resumeService.GetResumeByUserIdAsync(user.Id);

            if (resume == null)
            {

                await _resumeService.CreateResumeAsync(user.Id, user.FullName, user.Email);

            }

            var viewModel = new ResumeViewModel

            {

                Id = resume.Id,

                FullName = resume.FullName,

                Title = resume.Title,

                Summary = resume.Summary,

                Location = resume.Location,

                Email = resume.Email,

                Phone = resume.Phone,

                Website = resume.Website,

                LinkedIn = resume.LinkedIn,

                GitHub = resume.GitHub,

                ProfileImage = resume.ProfileImage,

                WorkExperiences = resume.WorkExperiences.Select(w => new WorkExperienceViewModel

                {

                    Id = w.Id,

                    CompanyName = w.CompanyName,

                    Position = w.Position,

                    Location = w.Location,

                    StartDate = w.StartDate,

                    EndDate = w.EndDate,

                    Description = w.Description,

                    CompanyUrl = w.CompanyUrl

                }).ToList(),

                Education = resume.Education.ToList(),

                Skills = resume.Skills.ToList(),

                Projects = resume.Projects.ToList(),

                Certificates = resume.Certificates.ToList()

            };



            return View(viewModel);

        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(ResumeViewModel model)
        {

            if (!ModelState.IsValid)
            {

                return View(model);

            }



            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {

                return NotFound();

            }



            var resume = await _resumeService.GetResumeByIdAsync(model.Id);

            if (resume == null || resume.AuthorId != user.Id)
            {

                return NotFound();

            }



            resume.FullName = model.FullName;

            resume.Title = model.Title;

            resume.Summary = model.Summary;

            resume.Location = model.Location;

            resume.Email = model.Email;

            resume.Phone = model.Phone;

            resume.Website = model.Website;

            resume.LinkedIn = model.LinkedIn;

            resume.GitHub = model.GitHub;

            resume.ProfileImage = model.ProfileImage;



            await _resumeService.UpdateResumeAsync(resume);

            return RedirectToAction(nameof(Index));

        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddWorkExperience([FromForm] WorkExperience model)
        {

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {

                return Json(new { success = false });

            }



            var resume = await _resumeService.GetResumeByUserIdAsync(user.Id);

            if (resume == null)
            {

                return Json(new { success = false });

            }

            model.ResumeId = resume.Id;
            await _resumeService.AddWorkExperienceAsync(model);

            return Json(new { success = true });

        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteWorkExperience(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {

                return Json(new { success = false });

            }



            var experience = await _resumeService.GetWorkExperienceByIdAsync(id);

            if (experience == null || experience.Resume?.AuthorId != user.Id)
            {

                return Json(new { success = false });

            }

            await _resumeService.DeleteWorkExperienceAsync(experience);


            return Json(new { success = true });

        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddEducation([FromForm] Education model)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {

                return Json(new { success = false });
            }



            //var resume = await _context.Resumes.FirstOrDefaultAsync(r => r.AuthorId == user.Id);
            var resume = await _resumeService.GetResumeByUserIdAsync(user.Id);
            if (resume == null)
            {

                return Json(new { success = false });
            }



            model.ResumeId = resume.Id;

            await _resumeService.AddEducationAsync(model);

            return Json(new { success = true });

        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {

                return Json(new { success = false });
            }



            var education = await _resumeService.GetEducationByIdAsync(id);

            if (education == null || education.Resume?.AuthorId != user.Id)
            {

                return Json(new { success = false });

            }

            await _resumeService.DeleteEducationAsync(education);

            return Json(new { success = true });

        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddSkill([FromForm] Skill model)

        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {

                return Json(new { success = false });

            }


            var resume = await _resumeService.GetResumeByUserIdAsync(user.Id);
            if (resume == null)
            {

                return Json(new { success = false });

            }

            model.ResumeId = resume.Id;
            await _resumeService.AddSkillAsync(model);

            return Json(new { success = true });
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteSkill(int id)

        {

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Json(new { success = false });

            }

            var skill = await _resumeService.GetSkillByIdAsync(id);

            if (skill == null || skill.Resume?.AuthorId != user.Id)
            {
                return Json(new { success = false });
            }

            await _resumeService.DeleteSkillAsync(skill.Id, user.Id);

            return Json(new { success = true });
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProject([FromForm] Project model)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {

                return Json(new { success = false });

            }

            var resume = await _resumeService.GetResumeByUserIdAsync(user.Id);
            if (resume == null)
            {
                return Json(new { success = false });

            }

            model.ResumeId = resume.Id;
            await _resumeService.AddProjectAsync(model);
            return Json(new { success = true });
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Json(new { success = false });

            }

            var project = await _resumeService.GetProjectByIdAsync(id);

            if (project == null || project.Resume?.AuthorId != user.Id)
            {

                return Json(new { success = false });
            }

            await _resumeService.DeleteProjectAsync(project.Id, user.Id);

            return Json(new { success = true });
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCertificate([FromForm] Certificate model)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {

                return Json(new { success = false });

            }

            var resume = await _resumeService.GetResumeByUserIdAsync(user.Id);
            if (resume == null)
            {

                return Json(new { success = false });

            }

            model.ResumeId = resume.Id;

            await _resumeService.AddCertificateAsync(model);

            return Json(new { success = true });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {

                return Json(new { success = false });

            }

            var certificate = await _resumeService.GetCertificateByIdAsync(id);

            if (certificate == null || certificate.Resume?.AuthorId != user.Id)
            {

                return Json(new { success = false });
            }

            await _resumeService.DeleteCertificateAsync(certificate.Id, user.Id);
            return Json(new { success = true });

        }

    }

}

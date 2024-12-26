using BlogApp.Data;
using BlogApp.Models;
using BlogApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Services
{
    public class TagService : ITagService
    {
        private readonly BlogDbContext _resumeService;
        private readonly ISlugService _slugService;

        public TagService(BlogDbContext resumeService, ISlugService slugService)
        {
            _resumeService = resumeService;
            _slugService = slugService;
        }

        public async Task<Tag> GetOrCreateTagAsync(string tagName)
        {
            tagName = tagName.Trim().ToLower();
            var tag = await _resumeService.Tags.FirstOrDefaultAsync(t => t.Name == tagName);

            if (tag == null)
            {
                tag = new Tag
                {
                    Name = tagName,
                    Slug = _slugService.GenerateSlug(tagName)
                };
                await _resumeService.Tags.AddAsync(tag);
                await _resumeService.SaveChangesAsync();
            }

            return tag;
        }

        public async Task<IEnumerable<Tag>> GetOrCreateTagsFromStringAsync(string tagsString)
        {
            var tagNames = tagsString.Split(',')
                .Select(t => t.Trim().ToLower())
                .Where(t => !string.IsNullOrEmpty(t))
                .Distinct();

            var tags = new List<Tag>();
            foreach (var tagName in tagNames)
            {
                tags.Add(await GetOrCreateTagAsync(tagName));
            }

            return tags;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _resumeService.Tags.ToListAsync();
        }
    }
}
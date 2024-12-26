using BlogApp.Data;
using BlogApp.Models;
using BlogApp.Services.Interfaces;
using BlogApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Services
{
    public class BlogService : IBlogService
    {
        private readonly BlogDbContext _resumeService;
        private readonly ISlugService _slugService;
        private readonly ITagService _tagService;

        public BlogService(BlogDbContext resumeService, ISlugService slugService, ITagService tagService)
        {
            _resumeService = resumeService;
            _slugService = slugService;
            _tagService = tagService;
        }

        public async Task<IEnumerable<BlogPost>> GetAllPostsAsync(string? searchTerm = null)
        {
            var query = _resumeService.BlogPosts
                .Include(p => p.Author)
                .Include(p => p.Tags)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(p =>
                    p.Title.ToLower().Contains(searchTerm) ||
                    p.Content.ToLower().Contains(searchTerm) ||
                    p.Summary.ToLower().Contains(searchTerm) ||
                    p.Tags.Any(t => t.Name.ToLower().Contains(searchTerm))
                );
            }

            return await query.OrderByDescending(p => p.CreatedDate).ToListAsync();
        }

        public async Task<BlogPost?> GetPostByIdAsync(int id)
        {
            return await _resumeService.BlogPosts
                .Include(p => p.Author)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<BlogPost> CreatePostAsync(BlogPostViewModel model, string authorId)
        {
            var post = new BlogPost
            {
                Title = model.Title,
                Slug = _slugService.GenerateSlug(model.Title),
                Content = model.Content,
                Summary = model.Summary,
                HeaderImage = model.HeaderImage,
                AuthorId = authorId,
                CreatedDate = DateTime.Now
            };

            var tags = await _tagService.GetOrCreateTagsFromStringAsync(model.TagsString);
            foreach (var tag in tags)
            {
                post.Tags.Add(tag);
            }

            await _resumeService.BlogPosts.AddAsync(post);
            await _resumeService.SaveChangesAsync();

            return post;
        }

        public async Task<BlogPost?> UpdatePostAsync(int id, BlogPostViewModel model, string currentUserId)
        {
            var post = await _resumeService.BlogPosts
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null || post.AuthorId != currentUserId)
            {
                return null;
            }

            post.Title = model.Title;
            post.Slug = _slugService.GenerateSlug(model.Title);
            post.Content = model.Content;
            post.Summary = model.Summary;
            post.HeaderImage = model.HeaderImage;

            post.Tags.Clear();
            var tags = await _tagService.GetOrCreateTagsFromStringAsync(model.TagsString);
            foreach (var tag in tags)
            {
                post.Tags.Add(tag);
            }

            await _resumeService.SaveChangesAsync();
            return post;
        }

        public async Task<bool> DeletePostAsync(int id, string currentUserId)
        {
            var post = await _resumeService.BlogPosts
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null || post.AuthorId != currentUserId)
            {
                return false;
            }

            post.Tags.Clear();
            _resumeService.BlogPosts.Remove(post);
            await _resumeService.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsUserAuthorizedToEditPost(int postId, string userId)
        {
            var post = await _resumeService.BlogPosts.FindAsync(postId);
            return post?.AuthorId == userId;
        }

        public async Task GenerateSlugsForExistingPosts()
        {
            var posts = await _resumeService.BlogPosts.ToListAsync();
            foreach (var post in posts)
            {
                if (string.IsNullOrEmpty(post.Slug))
                {
                    post.Slug = _slugService.GenerateSlug(post.Title);
                }
            }
            await _resumeService.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetPostsByTagAsync(string tagName)
        {
            return await _resumeService.BlogPosts
                .Include(p => p.Author)
                .Include(p => p.Tags)
                .Where(p => p.Tags.Any(t => t.Name.ToLower() == tagName.ToLower()))
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<BlogPost?> GetPostBySlugAsync(string slug)
        {
            return await _resumeService.BlogPosts
                .Include(p => p.Author)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }
    }
}
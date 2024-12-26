using BlogApp.Models;
using BlogApp.ViewModels;

namespace BlogApp.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogPost>> GetAllPostsAsync(string? searchTerm = null);
        Task<IEnumerable<BlogPost>> GetPostsByTagAsync(string tagName);
        Task<BlogPost?> GetPostByIdAsync(int id);
        Task<BlogPost> CreatePostAsync(BlogPostViewModel model, string authorId);
        Task<BlogPost?> UpdatePostAsync(int id, BlogPostViewModel model, string currentUserId);
        Task<bool> DeletePostAsync(int id, string currentUserId);
        Task<bool> IsUserAuthorizedToEditPost(int postId, string userId);
        Task GenerateSlugsForExistingPosts();
        Task<BlogPost?> GetPostBySlugAsync(string slug);
    }
}
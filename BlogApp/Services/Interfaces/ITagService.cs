using BlogApp.Models;

namespace BlogApp.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllTagsAsync();
        Task<Tag> GetOrCreateTagAsync(string tagName);
        Task<IEnumerable<Tag>> GetOrCreateTagsFromStringAsync(string tagsString);
    }
}
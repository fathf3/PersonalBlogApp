using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels
{
    public class BlogPostViewModel
    {
        public BlogPostViewModel()
        {
            Title = string.Empty;
            Content = string.Empty;
            Summary = string.Empty;
            TagsString = string.Empty;
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Summary { get; set; }

        public string? HeaderImage { get; set; }

        [Required]
        [Display(Name = "Tags (comma separated)")]
        public string TagsString { get; set; }
    }
}
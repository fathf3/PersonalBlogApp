using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class BlogPost
    {
        public BlogPost()
        {
            Tags = new HashSet<Tag>();
            Title = string.Empty;
            Slug = string.Empty;
            Content = string.Empty;
            Summary = string.Empty;
            AuthorId = string.Empty;
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Summary { get; set; }

        public string? HeaderImage { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual Author Author { get; set; } = null!;

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
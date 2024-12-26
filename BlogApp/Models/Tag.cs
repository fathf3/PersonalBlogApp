using System.Collections.Generic;

namespace BlogApp.Models
{
    public class Tag
    {
        public Tag()
        {
            BlogPosts = new HashSet<BlogPost>();
        }

        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public virtual ICollection<BlogPost> BlogPosts { get; set; }
    }
}
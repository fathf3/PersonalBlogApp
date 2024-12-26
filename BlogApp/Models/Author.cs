using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Models
{
    public class Author : IdentityUser
    {
        public Author()
        {
            BlogPosts = new HashSet<BlogPost>();
            JoinDate = DateTime.Now;
        }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime JoinDate { get; set; }
        public virtual ICollection<BlogPost> BlogPosts { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
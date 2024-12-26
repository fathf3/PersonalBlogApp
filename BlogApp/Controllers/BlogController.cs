using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using BlogApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BlogApp.Services.Interfaces;

namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly UserManager<Author> _userManager;
        private readonly ITagService _tagService;

        public BlogController(IBlogService blogService, UserManager<Author> userManager, ITagService tagService)
        {
            _blogService = blogService;
            _userManager = userManager;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index(string? search = null, string? tag = null)
        {
            IEnumerable<BlogPost> posts;

            if (!string.IsNullOrEmpty(tag))
            {
                posts = await _blogService.GetPostsByTagAsync(tag);
                ViewBag.CurrentTag = tag;
            }
            else
            {
                posts = await _blogService.GetAllPostsAsync(search);
                ViewBag.Search = search;
            }

            var tags = await _tagService.GetAllTagsAsync();

            // SEO meta etiketleri
            ViewData["Title"] = "Blog Yazıları";
            ViewData["Description"] = "Yazılım, teknoloji ve kişisel deneyimlerim hakkında blog yazıları.";
            ViewData["Keywords"] = string.Join(", ", tags.Select(t => t.Name));
            ViewData["OgType"] = "website";
            ViewData["OgImage"] = "~/img/logo.png"; // Varsayılan blog kapak resmi

            return View(posts);
        }

        public async Task<IActionResult> Details(int id, string? slug = null)
        {
            var post = await _blogService.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            // If no slug is provided or the wrong slug is provided, redirect to the correct URL
            if (string.IsNullOrEmpty(slug) || slug != post.Slug)
            {
                return RedirectToAction(nameof(Details), new { id = post.Id, slug = post.Slug });
            }

            return View(post);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(new BlogPostViewModel());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var author = await _userManager.GetUserAsync(User);
                if (author == null)
                {
                    return NotFound("Author not found");
                }

                var post = await _blogService.CreatePostAsync(model, author.Id);
                return RedirectToAction(nameof(Details), new { id = post.Id, slug = post.Slug });
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || !await _blogService.IsUserAuthorizedToEditPost(id, currentUser.Id))
            {
                return Forbid();
            }

            var viewModel = new BlogPostViewModel
            {
                Title = post.Title,
                Content = post.Content,
                Summary = post.Summary,
                HeaderImage = post.HeaderImage,
                TagsString = string.Join(", ", post.Tags.Select(t => t.Name))
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return NotFound("User not found");
                }

                var post = await _blogService.UpdatePostAsync(id, model, currentUser.Id);
                if (post == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Details), new { id = post.Id, slug = post.Slug });
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || !await _blogService.IsUserAuthorizedToEditPost(id, currentUser.Id))
            {
                return Forbid();
            }

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound("User not found");
            }

            var result = await _blogService.DeletePostAsync(id, currentUser.Id);
            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GenerateSlugs()
        {
            await _blogService.GenerateSlugsForExistingPosts();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Post(string slug)
        {
            var post = await _blogService.GetPostBySlugAsync(slug);
            if (post == null)
                return NotFound();

            // SEO meta etiketleri
            ViewData["Title"] = post.Title;
            ViewData["Description"] = post.Summary ?? post.Content.Substring(0, Math.Min(post.Content.Length, 160));
            ViewData["Keywords"] = string.Join(", ", post.Tags.Select(t => t.Name));
            ViewData["OgType"] = "article";
            ViewData["OgImage"] = post.HeaderImage ?? "~/img/logo.png";

            return View(post);
        }
    }
}
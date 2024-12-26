using System.Text;
using System.Text.RegularExpressions;
using BlogApp.Services.Interfaces;

namespace BlogApp.Services
{
    public class SlugService : ISlugService
    {
        public string GenerateSlug(string title)
        {
            // Remove diacritics (accents)
            string normalizedString = title.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            string slug = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            // Convert to lowercase and replace spaces with hyphens
            slug = slug.ToLowerInvariant()
                      .Replace(" ", "-")
                      .Replace("ı", "i")
                      .Replace("ğ", "g")
                      .Replace("ü", "u")
                      .Replace("ş", "s")
                      .Replace("ö", "o")
                      .Replace("ç", "c");

            // Remove invalid characters
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

            // Remove multiple hyphens
            slug = Regex.Replace(slug, @"-+", "-");

            // Trim hyphens from start and end
            slug = slug.Trim('-');

            return slug;
        }
    }
}
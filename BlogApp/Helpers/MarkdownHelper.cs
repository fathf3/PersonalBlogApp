using Markdig;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.Helpers
{
    public static class MarkdownHelper
    {
        private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();

        public static HtmlString MarkdownToHtml(this IHtmlHelper html, string markdown)
        {
            if (string.IsNullOrEmpty(markdown)) return new HtmlString(string.Empty);
            var result = Markdown.ToHtml(markdown, Pipeline);
            return new HtmlString(result);
        }
    }
}
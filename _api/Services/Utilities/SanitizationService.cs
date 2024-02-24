namespace api_sylvainbreton.Services.Utilities
{
    using api_sylvainbreton.Services.Interfaces;
    using Ganss.Xss;
    using HtmlAgilityPack;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class SanitizationService : ISanitizationService
    {
        private readonly HtmlSanitizer _sanitizer = new();

        public SanitizationService()
        {
            _sanitizer = new HtmlSanitizer();
            ConfigureSanitizer();
        }

        public string SanitizeInput(string input)
        {
            // Basic HTML sanitization
            var sanitizedInput = _sanitizer.Sanitize(input);

            // Further processing for links and styles within the sanitized HTML
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(sanitizedInput);

            // Validate and sanitize href attributes
            var anchorNodes = htmlDoc.DocumentNode.SelectNodes("//a[@href]");
            if (anchorNodes != null)
            {
                foreach (var node in anchorNodes)
                {
                    var href = node.GetAttributeValue("href", string.Empty);
                    if (!IsValidHref(href))
                    {
                        node.Attributes["href"].Remove();
                    }
                }
            }

            // Validate and sanitize style attributes
            var nodesWithStyle = htmlDoc.DocumentNode.SelectNodes("//*[contains(@style)]");
            if (nodesWithStyle != null)
            {
                foreach (var node in nodesWithStyle)
                {
                    var style = node.GetAttributeValue("style", string.Empty);
                    var validatedStyle = ValidateAndSanitizeStyle(style);
                    if (!string.IsNullOrWhiteSpace(validatedStyle))
                    {
                        node.SetAttributeValue("style", validatedStyle);
                    }
                    else
                    {
                        node.Attributes["style"].Remove();
                    }
                }
            }

            return htmlDoc.DocumentNode.OuterHtml;
        }

        private void ConfigureSanitizer()
        {
            _sanitizer.AllowedTags.Clear();
            _sanitizer.AllowedTags.UnionWith(["p", "strong", "em", "br", "a"]);
            _sanitizer.AllowedAttributes.Clear();
            _sanitizer.AllowedAttributes.UnionWith(["href", "style"]);
            _sanitizer.AllowedCssProperties.Clear();
            _sanitizer.AllowedCssProperties.UnionWith(["color", "font-weight", "margin", "padding"]);
            _sanitizer.KeepChildNodes = true;
        }

        // Example methods to illustrate potential additional functionality
        // These methods can be adapted or removed based on actual requirements
        private static bool IsValidHref(string href)
        {
            return Uri.TryCreate(href, UriKind.Absolute, out Uri parsedUri) &&
                   (parsedUri.Scheme == Uri.UriSchemeHttp ||
                    parsedUri.Scheme == Uri.UriSchemeHttps ||
                    parsedUri.Scheme == Uri.UriSchemeMailto);
        }

        private static string ValidateAndSanitizeStyle(string style)
        {
            var allowedStyles = new Dictionary<string, string>
            {
                { "color", @"^#[0-9a-fA-F]{6}$" },
                { "font-weight", @"^(bold|normal|\d{1,3})$" },
                { "margin", @"^\d+px$" },
                { "padding", @"^\d+px$" }
            };

            var sanitizedStyles = new List<string>();
            var individualStyles = style.Split(';').Where(s => !string.IsNullOrWhiteSpace(s));

            foreach (var individualStyle in individualStyles)
            {
                var styleParts = individualStyle.Split(':');
                if (styleParts.Length == 2)
                {
                    var key = styleParts[0].Trim().ToLower();
                    var value = styleParts[1].Trim();

                    if (allowedStyles.TryGetValue(key, out var regexPattern) && Regex.IsMatch(value, regexPattern))
                    {
                        sanitizedStyles.Add($"{key}: {value}");
                    }
                }
            }

            return string.Join("; ", sanitizedStyles);
        }
    }
}

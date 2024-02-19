namespace api_sylvainbreton.Services.Interfaces
{
    using Ganss.Xss;
    using System.Text.RegularExpressions;
    using HtmlAgilityPack;

    public interface ISanitizationService
    {
        string SanitizeInput(string input);
    }

    public class SanitizationService : ISanitizationService
    {
        private static readonly HtmlSanitizer sanitizer = new HtmlSanitizer();

        static SanitizationService()
        {
            sanitizer.AllowedTags.Clear();
            sanitizer.AllowedTags.UnionWith(new[] { "p", "strong", "em", "br", "a" });
            sanitizer.AllowedAttributes.Clear();
            sanitizer.AllowedAttributes.UnionWith(new[] { "href", "style" });
            sanitizer.AllowedCssProperties.Clear();
            sanitizer.AllowedCssProperties.UnionWith(new[] { "color", "font-weight" });
            sanitizer.AllowedCssProperties.Add("margin");
            sanitizer.AllowedCssProperties.Add("padding");
            sanitizer.KeepChildNodes = true;
        }

        public string SanitizeInput(string input)
        {
            var sanitizedInput = sanitizer.Sanitize(input);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(sanitizedInput);

            ModifyHrefAttributes(htmlDocument);
            ModifyStyleAttributes(htmlDocument);

            return htmlDocument.DocumentNode.OuterHtml;
        }

        private static void ModifyHrefAttributes(HtmlDocument document)
        {
            var anchorNodes = document.DocumentNode.SelectNodes("//a[@href]");
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
        }

        private static void ModifyStyleAttributes(HtmlDocument document)
        {
            var nodesWithStyle = document.DocumentNode.SelectNodes("//*[contains(@style)]");
            if (nodesWithStyle != null)
            {
                foreach (var node in nodesWithStyle)
                {
                    var style = node.GetAttributeValue("style", string.Empty);
                    var validatedStyle = ValidateAndSanitizeStyle(style);
                    if (string.IsNullOrEmpty(validatedStyle))
                    {
                        node.Attributes["style"].Remove();
                    }
                    else
                    {
                        node.SetAttributeValue("style", validatedStyle);
                    }
                }
            }
        }

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

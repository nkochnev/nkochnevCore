using System;
using System.Text;

namespace NkochnevCore.WebApi
{
    public static class MarkdownHelper
    {
        private const string DefaultLang = "csharp";
        private const string CodeWrapSymbols = "```";

        public static string AddDefaultLangToCodeWrappers(string content)
        {
            var sb = new StringBuilder();
            var lines = content.Split(new[] {"\n"}, StringSplitOptions.None);
            var countOfCodeWrapSymbols = 0;
            foreach (var line in lines)
            {
                if (line.StartsWith(CodeWrapSymbols))
                {
                    countOfCodeWrapSymbols++;
                    if (line.Length == CodeWrapSymbols.Length)
                        sb.AppendLine(countOfCodeWrapSymbols % 2 == 1
                            ? $"{CodeWrapSymbols}{DefaultLang}"
                            : CodeWrapSymbols);
                    else
                        sb.AppendLine(line);
                }
                else
                    sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}
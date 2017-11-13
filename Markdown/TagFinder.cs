using System;
using System.Collections.Generic;
using System.Linq;
using Markdown.Tags;

namespace Markdown
{
    public class TagFinder
    {
        protected string[] tagStrings;

        public TagFinder(IEnumerable<string> tagStrings)
        {
            this.tagStrings = tagStrings.OrderByDescending(s => s.Length).ToArray();
        }

        protected Tag FindTag(string str)
        {
            var previousCharIsWhiteSpace = true;

            for (var i = 0; i < str.Length; i++)
            {
                //if()
            }
            return new Tag("", 0);
        }

        protected string SelectProperOpenTag(string tagStartsStr)
        {
            var properTagStr = tagStrings
                .FirstOrDefault(s => tagStartsStr.Length > s.Length &&
                                     tagStartsStr.StartsWith(s) && !char.IsWhiteSpace(tagStartsStr[s.Length]));

            if (properTagStr == null)
                return string.Empty;

            return properTagStr;
        }

        protected string SelectProperCloseTag(string tagStartsStr)
        {
            var spaceIndex = tagStartsStr.IndexOf(" ", StringComparison.Ordinal);
            var firstWord = spaceIndex <= 0 ? tagStartsStr : tagStartsStr.Substring(0, spaceIndex);

            var properTagStr = tagStrings
                .FirstOrDefault(s => firstWord.EndsWith(s));

            if (properTagStr == null)
                return string.Empty;

            return properTagStr;
        }
    }
}
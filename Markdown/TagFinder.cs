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

        public Tag FindTag(string str)
        {
            var previousCharIsWhiteSpace = true;

            for (var i = 0; i < str.Length; i++)
            {
                if (!tagStrings.Any(s => str.Substring(i).StartsWith(s)))
                {
                    previousCharIsWhiteSpace = char.IsWhiteSpace(str[i]);
                    continue;
                }

                var foundTag = previousCharIsWhiteSpace ? (Tag)
                    SelectProperOpenTag(str, i) : SelectProperCloseTag(str, i);

                if (foundTag != null)
                    return foundTag;
            }
            return null;
        }

        protected OpenTag SelectProperOpenTag(string input, int selectStartIndex)
        {
            var properTagStr = tagStrings
                .FirstOrDefault(s => input.Substring(selectStartIndex).Length > s.Length &&
                                     input.Substring(selectStartIndex).StartsWith(s) && 
                                     !char.IsWhiteSpace(input[s.Length]));

            return properTagStr == null ? null : new OpenTag(properTagStr, selectStartIndex);
        }

        protected CloseTag SelectProperCloseTag(string input, int selectStartIndex)
        {
            var spaceIndex = input.Substring(selectStartIndex).IndexOf(" ", StringComparison.Ordinal);
            var firstWord = spaceIndex <= 0 ? 
                    input.Substring(selectStartIndex) : input.Substring(selectStartIndex, spaceIndex);

            var properTagStr = tagStrings
                .FirstOrDefault(s => firstWord == s);

            return properTagStr == null ? null : new CloseTag(properTagStr, selectStartIndex);
        }
    }
}
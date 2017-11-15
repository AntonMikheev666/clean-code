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

        public Tag[] FindMarkingTags(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new Tag[] {};

            var markingTags = new List<Tag>();
            var openTagStack = new Stack<OpenTag>();
            var previousCharIsWhiteSpace = true;
            for (var i = 0; i < input.Length; previousCharIsWhiteSpace = char.IsWhiteSpace(input[i - 1]))
            {
                var newTag = FindTag(input, i, previousCharIsWhiteSpace);
                if (newTag == null)
                    break;

                if (newTag is CloseTag && !openTagStack.Any(t => Tag.OneTagStringStartsWithAnother(t, newTag)))
                {
                    i = newTag.StartIndex + newTag.TagString.Length;
                    continue;
                }

                if (newTag is OpenTag)
                {
                    openTagStack.Push((OpenTag)newTag);
                    i = newTag.StartIndex + newTag.TagString.Length;
                    continue;
                }

                var topTag = openTagStack.Pop();
                while (openTagStack.Count > 0 && !Tag.OneTagStringStartsWithAnother(topTag, newTag))
                    topTag = openTagStack.Pop();

                Tag.SetTagStringToLesser(topTag, newTag);
                markingTags.Add(topTag);
                markingTags.Add(newTag);
                i = newTag.StartIndex + newTag.TagString.Length;
            }
            return markingTags.ToArray();
        }

        private Tag FindTag(string str, int startIndex = 0, bool previousCharIsWhiteSpace = true)
        {
            var prevCharIsWhiteSpace = previousCharIsWhiteSpace;

            for (var i = startIndex; i < str.Length; i++)
            {
                if (!tagStrings.Any(s => str.Substring(i).StartsWith(s)))
                {
                    prevCharIsWhiteSpace = char.IsWhiteSpace(str[i]);
                    continue;
                }

                var foundTag = prevCharIsWhiteSpace ? (Tag)
                    SelectProperOpenTag(str, i) : SelectProperCloseTag(str, i);

                if (foundTag != null)
                    return foundTag;
            }
            return null;
        }

        private OpenTag SelectProperOpenTag(string input, int selectStartIndex)
        {
            var properTagStr = tagStrings
                .FirstOrDefault(s => input.Substring(selectStartIndex).Length > s.Length &&
                                     input.Substring(selectStartIndex).StartsWith(s) && 
                                     !char.IsWhiteSpace(input[s.Length]));

            return properTagStr == null ? null : new OpenTag(properTagStr, selectStartIndex);
        }

        private CloseTag SelectProperCloseTag(string input, int selectStartIndex)
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
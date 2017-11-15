using System;
using System.Collections.Generic;
using System.Linq;
using Markdown.Interfaces;
using Markdown.Tags;

namespace Markdown
{
    public class TagFinder : ITagFinder
    {
        private readonly string[] tagStringsFromLongest;

        public TagFinder(IEnumerable<string> tagStrings)
        {
            this.tagStringsFromLongest = tagStrings.OrderByDescending(s => s.Length).ToArray();
        }

        public IEnumerable<Tag> FindMarkingTags(string strWithTags)
        {
            if (string.IsNullOrWhiteSpace(strWithTags))
                return new Tag[] {};
            
            var markingTags = new List<Tag>();
            var openTagStack = new Stack<Tag>();
            var previousCharIsWhiteSpace = true;
            for (var currentIndex = 0; currentIndex < strWithTags.Length; previousCharIsWhiteSpace = char.IsWhiteSpace(strWithTags[currentIndex - 1]))
            {
                var newTag = FindTag(strWithTags, currentIndex, previousCharIsWhiteSpace);
                if (newTag == null)
                    break;

                if (newTag is CloseTag && !openTagStack.Any(t => t.IsPairOf(newTag)))
                {
                    currentIndex = GetShift(newTag);
                    continue;
                }

                if (newTag is OpenTag)
                {
                    openTagStack.Push(newTag);
                    currentIndex = GetShift(newTag);
                    continue;
                }

                var topTag = openTagStack.Pop();
                while (openTagStack.Count > 0 && !topTag.IsPairOf(newTag))
                    topTag = openTagStack.Pop();

                MakeTagsPaired(ref topTag, ref newTag);
                markingTags.Add(topTag);
                markingTags.Add(newTag);
                currentIndex = GetShift(newTag);
            }
            return markingTags;
        }

        private int GetShift(Tag newTag)
        {
            return newTag.StartIndex + newTag.TagString.Length;
        }

        private void MakeTagsPaired(ref Tag firstTag, ref Tag secondTag)
        {
            if (firstTag.TagString.Length > secondTag.TagString.Length)
                firstTag = firstTag.MakePairedWith(secondTag);
            else
                secondTag = secondTag.MakePairedWith(firstTag);
        }

        private Tag FindTag(string str, int startIndex = 0, bool previousCharIsWhiteSpace = true)
        {
            var prevCharIsWhiteSpace = previousCharIsWhiteSpace;

            for (var i = startIndex; i < str.Length; i++)
            {
                if (!tagStringsFromLongest.Any(s => str.Substring(i).StartsWith(s)))
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
            var followingString = input.Substring(selectStartIndex);
            var properTagStr = tagStringsFromLongest
                .FirstOrDefault(s => followingString.Length > s.Length &&
                                     followingString.StartsWith(s) && 
                                     !char.IsWhiteSpace(followingString[s.Length]));

            return properTagStr == null ? null : new OpenTag(properTagStr, selectStartIndex);
        }

        private CloseTag SelectProperCloseTag(string input, int selectStartIndex)
        {
            var spaceIndex = input.Substring(selectStartIndex).IndexOf(" ", StringComparison.Ordinal);
            var firstWord = spaceIndex <= 0 ? 
                    input.Substring(selectStartIndex) : input.Substring(selectStartIndex, spaceIndex);

            var properTagStr = tagStringsFromLongest
                .FirstOrDefault(s => firstWord == s);

            return properTagStr == null ? null : new CloseTag(properTagStr, selectStartIndex);
        }
    }
}
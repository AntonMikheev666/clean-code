using System.Collections.Generic;
using Markdown.Tags;

namespace Markdown
{
    public class TagChanger
    {
        private Dictionary<string, string> openTagChangeTo;
        private Dictionary<string, string> closeTagChangeTo;

        public TagChanger(Dictionary<string, string> openTagChangeTo, Dictionary<string, string> closeTagChangeTo)
        {
            this.openTagChangeTo = openTagChangeTo;
            this.closeTagChangeTo = closeTagChangeTo;
        }

        public string ChangeTag(string strWithTags, Tag tag)
        {
            Dictionary<string, string> tagsToPlace = tag is OpenTag ? openTagChangeTo : closeTagChangeTo;
            return strWithTags
                .Remove(tag.StartIndex, tag.TagString.Length)
                .Insert(tag.StartIndex, tagsToPlace[tag.TagString]);
        }
    }
}
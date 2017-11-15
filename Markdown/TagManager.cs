using System;
using System.Collections.Generic;
using System.Linq;
using Markdown.TagChanges;
using Markdown.Tags;

namespace Markdown
{
    public class TagManager
    {
        private IEnumerable<MdHtmlTagMap> tagChanges;
        public TagManager(IEnumerable<MdHtmlTagMap> tagChanges)
        {
            this.tagChanges = tagChanges;
        }

        public string InsertTag(string strWithTags, Tag tag)
        {
            Type tagChangeType = tag is OpenTag ? typeof(OpenMdHtmlTagMap) : typeof(CloseMdHtmlTagMap);
            var properTagChange = tagChanges
                .FirstOrDefault(c => c.GetType() == tagChangeType &&
                                     c.MdTagString == tag.TagString
                );

            return properTagChange == null
                ? strWithTags
                : strWithTags
                    .Remove(tag.StartIndex, tag.TagString.Length)
                    .Insert(tag.StartIndex, properTagChange.HtmlTagString);
        }
    }
}
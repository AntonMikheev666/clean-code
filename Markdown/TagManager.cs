using System;
using System.Collections.Generic;
using System.Linq;
using Markdown.TagChanges;
using Markdown.Tags;

namespace Markdown
{
    public class TagManager
    {
        private IEnumerable<TagChange> tagChanges;
        public TagManager(IEnumerable<TagChange> tagChanges)
        {
            this.tagChanges = tagChanges;
        }

        public string InsertTag(string strWithTags, Tag tag)
        {
            Type tagChangeType = tag is OpenTag ? typeof(OpenTagChange) : typeof(CloseTagChange);
            var properTagChange = tagChanges
                .FirstOrDefault(c => c.GetType() == tagChangeType &&
                                     c.OldTagString == tag.TagString
                );

            return properTagChange == null
                ? strWithTags
                : strWithTags
                    .Remove(tag.StartIndex, tag.TagString.Length)
                    .Insert(tag.StartIndex, properTagChange.NewTagString);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Markdown.Interfaces;
using Markdown.TagChanges;
using Markdown.Tags;

namespace Markdown
{
    public class TagManager : ITagManager
    {
        private IEnumerable<Md2HtmlTagMap> tagMaps;
        public TagManager(IEnumerable<Md2HtmlTagMap> tagMaps)
        {
            this.tagMaps = tagMaps;
        }

        public string InsertTag(string strWithTags, Tag tag)
        {
            var tagMap = tag is OpenTag
                ? tagMaps.FirstOrDefault(m => m.MdOpenTagString == tag.TagString)
                : tagMaps.FirstOrDefault(m => m.MdCloseTagString == tag.TagString);

            if (tagMap == null)
                return strWithTags;

            return tag is OpenTag
                ? ReplaceTagWithStr(strWithTags, tag, tagMap.GetHtmlOpenTagString)
                : ReplaceTagWithStr(strWithTags, tag, tagMap.GetHtmlCloseTagString);
        }

        private string ReplaceTagWithStr(string strWithTags, Tag tag, string strToPut)
        {
            return strWithTags
                .Remove(tag.StartIndex, tag.TagString.Length)
                .Insert(tag.StartIndex, strToPut);
        }
    }
}
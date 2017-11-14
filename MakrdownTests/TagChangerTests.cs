using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Markdown;
using Markdown.Tags;
using NUnit.Framework;
using FluentAssertions;

namespace MakrdownTests
{
    [TestFixture]
    public class TagChangerTests
    {
        [Test]
        public void TagChanger_ChangeOpenTag()
        {
            var openTags = new Dictionary<string, string> { {"_", "<em>"}, {"__", "<strong>"} };
            var closeTags = new Dictionary<string, string> { { "_", "</em>" }, { "__", "</strong>" } };
            new TagChanger(openTags, closeTags).ChangeTag("_", new OpenTag("_", 0))
                .ShouldBeEquivalentTo("<em>");
        }

        [Test]
        public void TagChanger_ChangeCloseTag()
        {
            var openTags = new Dictionary<string, string> { { "_", "<em>" } };
            var closeTags = new Dictionary<string, string> { { "_", "</em>" } };
            new TagChanger(openTags, closeTags).ChangeTag("_", new CloseTag("_", 0))
                .ShouldBeEquivalentTo("</em>");
        }
    }
}

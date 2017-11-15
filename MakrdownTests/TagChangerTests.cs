using System.Collections.Generic;
using Markdown;
using Markdown.Tags;
using NUnit.Framework;
using FluentAssertions;
using Markdown.TagChanges;

namespace MakrdownTests
{
    [TestFixture]
    public class TagChangerTests
    {
        [Test]
        public void TagChanger_ChangeOpenTag()
        {
            new TagManager(new TagChange[] { new OpenTagChange("_", "<em>") })
                .InsertTag("_", new OpenTag("_", 0))
                .ShouldBeEquivalentTo("<em>");
        }

        [Test]
        public void TagChanger_ChangeCloseTag()
        {
            new TagManager(new TagChange[] {new CloseTagChange("_", "</em>"), })
                .InsertTag("_", new CloseTag("_", 0))
                .ShouldBeEquivalentTo("</em>");
        }
    }
}

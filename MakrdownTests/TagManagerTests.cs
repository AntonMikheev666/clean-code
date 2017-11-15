using Markdown;
using Markdown.Tags;
using NUnit.Framework;
using FluentAssertions;
using Markdown.TagChanges;

namespace MakrdownTests
{
    [TestFixture]
    public class TagManagerTests
    {
        [Test]
        public void TagManager_InsertOpenTag()
        {
            new TagManager(new TagChange[] { new OpenTagChange("_", "<em>") })
                .InsertTag("_", new OpenTag("_", 0))
                .ShouldBeEquivalentTo("<em>");
        }

        [Test]
        public void TagManager_InsertCloseTag()
        {
            new TagManager(new TagChange[] {new CloseTagChange("_", "</em>"), })
                .InsertTag("_", new CloseTag("_", 0))
                .ShouldBeEquivalentTo("</em>");
        }
    }
}

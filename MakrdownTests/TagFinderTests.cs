using System.Linq;
using FluentAssertions;
using Markdown;
using Markdown.Tags;
using NUnit.Framework;

namespace MakrdownTests
{
    [TestFixture]
    public class TagFinderTests: TagFinder
    {
        public TagFinderTests() : base(new [] {"_", "__"}) {}

        [Test]
        public void TagFinder_FindAllPairedTags()
        {
            var q = FindMarkingTags("_q __qwe__ __qw _q w_ qw_");
            q.ShouldBeEquivalentTo(new Tag[]
                {
                    new OpenTag("__", 3),
                    new CloseTag("__", 8),
                    new OpenTag("_", 11),
                    new OpenTag("_", 16),
                    new CloseTag("_", 20),
                    new CloseTag("_", 24)
                });
        }
    }
}

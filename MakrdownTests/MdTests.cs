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
    public class MdTests
    {
        [TestCase("_qwe_", ExpectedResult = "<em>qwe</em>", TestName = "OneItalicWord")]
        [TestCase("__qwe__", ExpectedResult = "<strong>qwe</strong>", TestName = "OneBoldWord")]
        public string Md_RenderToHTML(string mdText)
        {
            var openTags = new Dictionary<string, string> { {"_", "<em>"}, {"__", "<strong>"} };
            var closeTags = new Dictionary<string, string> { { "_", "</em>" }, { "__", "</strong>" } };
            return new Md(new[] { "_", "__" }, openTags, closeTags).RenderToHtml(mdText);
        }

        [Test]
        public void qwe()
        {
            var a = new Stack<int>();
            a.Push(5);
            a.Push(3);
            
            a.ToArray()[0].ShouldBeEquivalentTo(3);
        }
    }
}

using System.Collections.Generic;
using System.IO;
using Markdown;
using Markdown.TagChanges;
using NUnit.Framework;

namespace MakrdownTests
{
    [TestFixture]
    public class MdTests
    {
        [TestCase("_qwe_", ExpectedResult = "<em>qwe</em>", TestName = "OneItalicWord")]
        [TestCase("__qwe__", ExpectedResult = "<strong>qwe</strong>", TestName = "OneBoldWord")]
        [TestCase("___qwe___", ExpectedResult = "<strong>_qwe_</strong>", TestName = "TripleUnderline")]
        [TestCase("_q __qwe__ __qw _q w_ qw_", ExpectedResult = "_q <strong>qwe</strong> <em>_qw <em>q w</em> qw</em>", TestName = "Mix")]
        public string Md_RenderToHTML(string mdText)
        {
            var tagChanges = new MdHtmlTagMap[]
            {
                new OpenMdHtmlTagMap("_", "<em>"), new OpenMdHtmlTagMap("__", "<strong>"),
                new CloseMdHtmlTagMap("_", "</em>"), new CloseMdHtmlTagMap("__", "</strong>")
            };
            return new Md(new[] { "_", "__" }, tagChanges).RenderToHtml(mdText);
        }

        [Test]
        public void Md_TimeTest()
        {
            
        }
    }
}

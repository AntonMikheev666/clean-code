using System.Collections.Generic;
using Markdown;
using NUnit.Framework;

namespace MakrdownTests
{
    [TestFixture]
    public class MdTests
    {
        [TestCase("_qwe_", ExpectedResult = "<em>qwe</em>", TestName = "OneItalicWord")]
        [TestCase("__qwe__", ExpectedResult = "<strong>qwe</strong>", TestName = "OneBoldWord")]
        [TestCase("_q __qwe__ __qw _q w_ qw_", ExpectedResult = "_q <strong>qwe</strong> <em>_qw <em>q w</em> qw</em>", TestName = "Mix")]
        public string Md_RenderToHTML(string mdText)
        {
            var openTags = new Dictionary<string, string> { {"_", "<em>"}, {"__", "<strong>"} };
            var closeTags = new Dictionary<string, string> { { "_", "</em>" }, { "__", "</strong>" } };
            return new Md(new[] { "_", "__" }, openTags, closeTags).RenderToHtml(mdText);
        }
    }
}

using System.Collections.Generic;
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
        [TestCase("_q __qwe__ __qw _q w_ qw_", ExpectedResult = "_q <strong>qwe</strong> <em>_qw <em>q w</em> qw</em>", TestName = "Mix")]
        public string Md_RenderToHTML(string mdText)
        {
            var tagChanges = new TagChange[]
            {
                new OpenTagChange("_", "<em>"), new OpenTagChange("__", "<strong>"),
                new CloseTagChange("_", "</em>"), new CloseTagChange("__", "</strong>")
            };
            return new Md(new[] { "_", "__" }, tagChanges).RenderToHtml(mdText);
        }
    }
}

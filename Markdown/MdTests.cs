using NUnit.Framework;

namespace Markdown
{
    [TestFixture]
    public class Md_ShouldRender
    {
        private Md sut = new Md();

        [TestCase("_qwe_", ExpectedResult = "<em>qwe</em>", TestName = "WithoutSpaces")]
        [TestCase("_q w e_", ExpectedResult = "<em>q w e</em>", TestName = "WithSpaces")]
        [TestCase("werfgh _qwe_ bnm", ExpectedResult = "werfgh <em>qwe</em> bnm", TestName = "InText")]
        [TestCase(@"_\_qwe_", ExpectedResult = "<em>_qwe</em>", TestName = "WithShieldingPrefix")]
        [TestCase(@"_qwe\__", ExpectedResult = "<em>qwe_</em>", TestName = "WithShieldingSufix")]
        [TestCase(@"_\__qweqwe__", ExpectedResult = "<strong>_qwe_</strong>", TestName = "WithShielding3")]
        public string ItalicText(string mdText)
        {
            return sut.RenderToHtml(mdText);
        }

        [TestCase("_qwe", ExpectedResult = "_qwe", TestName = "Italic")]
        [TestCase("__qwe", ExpectedResult = "__qwe", TestName = "Bold")]
        public string SingleMark(string mdText)
        {
            return sut.RenderToHtml(mdText);
        }

        [TestCase(@"\_qwe\_", ExpectedResult = "_qwe_", TestName = "ItalicBoth")]
        [TestCase(@"\__qwe\__", ExpectedResult = "__qwe__", TestName = "BoldBoth")]
        [TestCase(@"qwe\_", ExpectedResult = "qwe_", TestName = "ItalicSecond")]
        [TestCase(@"qwe\__", ExpectedResult = "qwe__", TestName = "BoldSecond")]
        public string Shielding(string mdText)
        {
            return sut.RenderToHtml(mdText);
        }

        [TestCase("q_w_e", ExpectedResult = "q_w_e", TestName = "Italic")]
        [TestCase("q__w__e", ExpectedResult = "q__w__e", TestName = "Bold")]
        public string InwordMark(string mdText)
        {
            return sut.RenderToHtml(mdText);
        }

        [TestCase("__q _w_ e__", ExpectedResult = "<strong>q <em>w</em> e</strong>", TestName = "ItalicInBold")]
        [TestCase("_q __w__ e_", ExpectedResult = "<em>q <strong>w</strong> e</em>", TestName = "BoldInItalic")]
        public string NestedMarks(string mdText)
        {
            return sut.RenderToHtml(mdText);
        }

        [TestCase("__qwe__", ExpectedResult = "<strong>qwe</strong>", TestName = "WithoutSpaces")]
        [TestCase("__q w e__", ExpectedResult = "<strong>q w e</strong>", TestName = "WithSpaces")]
        [TestCase("werfgh __qwe__ bnm", ExpectedResult = "werfgh <strong>qwe</strong> bnm", TestName = "InText")]
        [TestCase(@"__qwe\___", ExpectedResult = "<strong>qwe_</strong>", TestName = "WithShieldingSufix")]
        [TestCase(@"__\__qwe__", ExpectedResult = "<strong>__qwe</strong>", TestName = "WithShieldingPrefix")]
        [TestCase(@"__\_qweqwe__\_", ExpectedResult = "<strong>_qwe</strong>_", TestName = "WithShielding1")]
        [TestCase(@"__\_qweqwe\___", ExpectedResult = "<strong>_qwe_</strong>", TestName = "WithShielding2")]
        public string BoldText(string mdText)
        {
            return sut.RenderToHtml(mdText);
        }

        [TestCase("___qwe___", ExpectedResult = "<strong>qwe</strong>", TestName = "WithoutSpaces")]
        [TestCase("___q w e___", ExpectedResult = "<strong>q w e</strong>", TestName = "WithSpaces")]
        [TestCase("werfgh ___qwe___ bnm", ExpectedResult = "werfgh <strong>qwe</strong> bnm", TestName = "InText")]
        [TestCase(@"___qwe\____", ExpectedResult = "<strong>qwe_</strong>", TestName = "WithShieldingSufix")]
        [TestCase(@"___\___qwe___", ExpectedResult = "<strong>__qwe</strong>", TestName = "StrongWithShieldingPrefix")]
        public string BoldItalicText(string mdText)
        {
            return sut.RenderToHtml(mdText);
        }
    }
}
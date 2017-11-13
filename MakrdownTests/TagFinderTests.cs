using System.Collections.Generic;
using Markdown.Implementations;
using NUnit.Framework;

namespace MakrdownTests
{
    [TestFixture]
    public class TagFinderTests
    {
        [TestCase("_", ExpectedResult = "", TestName = "SingleUnderline")]
        [TestCase("__", ExpectedResult = "_", TestName = "DoubleUnderline")]
        [TestCase("_qwe", ExpectedResult = "_", TestName = "SingleUnderlineWithWord")]
        [TestCase("__qwe", ExpectedResult = "__", TestName = "DoubleUnderlineWithWord")]
        [TestCase("___qwe", ExpectedResult = "__", TestName = "TripleUnderlineWithWord")]
        [TestCase("_ qwe", ExpectedResult = "", TestName = "SpaceAfterSingleUnderline")]
        [TestCase("__ qwe", ExpectedResult = "_", TestName = "SpaceAfterDoubleUnderline")]
        [TestCase("___ qwe", ExpectedResult = "__", TestName = "SpaceAfterTripleUnderline")]
        public string TagFinder_SelectProperOpenTag_WorksCorrectly(string str)
        {

            return new TestTagFinder(new HashSet<string> { "_", "__" })
                .TestSelectProperOpenTag(str);
        }

        [TestCase("_", ExpectedResult = "_", TestName = "SingleUnderline")]
        [TestCase("__", ExpectedResult = "__", TestName = "DoubleUnderline")]
        [TestCase("___", ExpectedResult = "__", TestName = "TripleUnderline")]
        [TestCase("_ qwe", ExpectedResult = "_", TestName = "SpaceAfterSingleUnderline")]
        [TestCase("__ qwe", ExpectedResult = "__", TestName = "SpaceAfterDoubleUnderline")]
        [TestCase("___ qwe", ExpectedResult = "__", TestName = "SpaceAfterTripleUnderline")]
        public string TagFinder_SelectProperCloseTag_WorkCorrectly(string str)
        {

            return new TestTagFinder(new HashSet<string> { "_", "__" })
                .TestSelectProperCloseTag(str);
        }
    }
}

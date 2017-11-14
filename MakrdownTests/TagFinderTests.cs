using System.Collections;
using System.Collections.Generic;
using Markdown;
using Markdown.Tags;
using NUnit.Framework;

namespace MakrdownTests
{
    [TestFixture]
    public class TagFinderTests: TagFinder
    {
        public TagFinderTests() : base(new [] {"_", "__"})
        {
        }

        [TestCase("_", ExpectedResult = "", TestName = "SingleUnderlineForOpenTag")]
        [TestCase("__", ExpectedResult = "_", TestName = "DoubleUnderlineForOpenTag")]
        [TestCase("_qwe", ExpectedResult = "_", TestName = "SingleUnderlineWithWordForOpenTag")]
        [TestCase("__qwe", ExpectedResult = "__", TestName = "DoubleUnderlineWithWordForOpenTag")]
        [TestCase("___qwe", ExpectedResult = "__", TestName = "TripleUnderlineWithWordForOpenTag")]
        [TestCase("_ qwe", ExpectedResult = "", TestName = "SpaceAfterSingleUnderlineForOpenTag")]
        [TestCase("__ qwe", ExpectedResult = "_", TestName = "SpaceAfterDoubleUnderlineForOpenTag")]
        [TestCase("___ qwe", ExpectedResult = "__", TestName = "SpaceAfterTripleUnderlineForOpenTag")]
        public string TagFinder_SelectProperOpenTag(string str, int selectStartIndex = 0)
        {
            var selectedTag = SelectProperOpenTag(str, selectStartIndex);
            return selectedTag == null ? "" : selectedTag.TagString;
        }

        [TestCase("_", ExpectedResult = "_", TestName = "SingleUnderlineForCloseTag")]
        [TestCase("__", ExpectedResult = "__", TestName = "DoubleUnderlineForCloseTag")]
        [TestCase("___", ExpectedResult = "", TestName = "TripleUnderlineForCloseTag")]
        [TestCase("_ qwe", ExpectedResult = "_", TestName = "SpaceAfterSingleUnderlineForCloseTag")]
        [TestCase("__ qwe", ExpectedResult = "__", TestName = "SpaceAfterDoubleUnderlineForCloseTag")]
        [TestCase("___ qwe", ExpectedResult = "", TestName = "SpaceAfterTripleUnderlineForCloseTag")]
        [TestCase("_qwe", ExpectedResult = "", TestName = "WordAfterSingleUnderlineForCloseTag")]
        [TestCase("__qwe", ExpectedResult = "", TestName = "WordAfterDoubleUnderlineForCloseTag")]
        [TestCase("___qwe", ExpectedResult = "", TestName = "WordAfterTripleUnderlineForCloseTag")]
        public string TagFinder_SelectProperCloseTag(string str, int selectStartIndex = 0)
        {
            var selectedTag = SelectProperCloseTag(str, selectStartIndex);
            return selectedTag == null ? "" : selectedTag.TagString;
        }

        [Test, TestCaseSource(nameof(FindTagTestSource))]
        public Tag TagFinder_FindTag(string str)
        {
            return FindTag(str);
        }

        public static IEnumerable FindTagTestSource
        {
            get
            {
                yield return new TestCaseData("_").Returns(null).SetName("SingleUnderline");
                yield return new TestCaseData("__").Returns(new OpenTag("_", 0)).SetName("DoubleUnderline");
                yield return new TestCaseData("_qwe").Returns(new OpenTag("_", 0)).SetName("SingleUnderlineWithWord");
                yield return new TestCaseData("__qwe").Returns(new OpenTag("__", 0)).SetName("DoubleUnderlineWithWord");
                yield return new TestCaseData("___qwe").Returns(new OpenTag("__", 0)).SetName("TripleUnderlineWithWord");
                yield return new TestCaseData("_ qwe").Returns(null).SetName("SpaceAfterSingleUnderline");
                yield return new TestCaseData("__ qwe").Returns(new OpenTag("_", 0)).SetName("SpaceAfterDoubleUnderline");
                yield return new TestCaseData("___ qwe").Returns(new OpenTag("__", 0)).SetName("SpaceAfterTripleUnderline");
                yield return new TestCaseData("qwe_").Returns(new CloseTag("_", 3)).SetName("SingleUnderlineAfterWord");
                yield return new TestCaseData("qwe__").Returns(new CloseTag("__", 3)).SetName("DoubleUnderlineAfterWord");
                yield return new TestCaseData("qwe___").Returns(new CloseTag("__", 4)).SetName("TripleUnderlineAfterWord");
                yield return new TestCaseData("qwe_ qw").Returns(new CloseTag("_", 3)).SetName("SpaceAfterSingleCloseTag");
                yield return new TestCaseData("qwe__ qw").Returns(new CloseTag("__", 3)).SetName("SpaceAfterDoubleCloseTag");
                yield return new TestCaseData("qwe___ qw").Returns(new CloseTag("__", 4)).SetName("SpaceAfterTripleCloseTag");
                yield return new TestCaseData("qwe_qw").Returns(null).SetName("SingleUnderlineInWord");
                yield return new TestCaseData("qwe__qw").Returns(null).SetName("DoubleUnderlineInWord");
                yield return new TestCaseData("qwe___qw").Returns(null).SetName("TripleUnderlineInWord");
            }
        }
    }
}

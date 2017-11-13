using System;
using System.Collections;
using System.Collections.Generic;
using Markdown.Tags;
using NUnit.Framework;
using FluentAssertions;

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
        public string TagFinder_SelectProperOpenTag_WorksCorrectly(string str, int selectStartIndex = 0)
        {
            var selectedTag = new TestTagFinder(new HashSet<string> {"_", "__"})
                .TestSelectProperOpenTag(str, selectStartIndex);
            return selectedTag == null ? "" : selectedTag.TagString;
        }

        [TestCase("_", ExpectedResult = "_", TestName = "SingleUnderline")]
        [TestCase("__", ExpectedResult = "__", TestName = "DoubleUnderline")]
        [TestCase("___", ExpectedResult = "__", TestName = "TripleUnderline")]
        [TestCase("_ qwe", ExpectedResult = "_", TestName = "SpaceAfterSingleUnderline")]
        [TestCase("__ qwe", ExpectedResult = "__", TestName = "SpaceAfterDoubleUnderline")]
        [TestCase("___ qwe", ExpectedResult = "__", TestName = "SpaceAfterTripleUnderline")]
        [TestCase("_qwe", ExpectedResult = "", TestName = "WordAfterSingleUnderline")]
        [TestCase("__qwe", ExpectedResult = "", TestName = "WordAfterDoubleUnderline")]
        [TestCase("___qwe", ExpectedResult = "", TestName = "WordAfterTripleUnderline")]
        public string TagFinder_SelectProperCloseTag_WorkCorrectly(string str, int selectStartIndex = 0)
        {
            var selectedTag = new TestTagFinder(new HashSet<string> { "_", "__" })
                .TestSelectProperCloseTag(str, selectStartIndex);
            return selectedTag == null ? "" : selectedTag.TagString;
        }

        [Test, TestCaseSource(nameof(FindTagTestSource))]
        public Tag TagFinder_FindTag_WorkCorrectly(string str)
        {
            return new TestTagFinder(new HashSet<string> { "_", "__" }).TestFindTag(str);
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
                yield return new TestCaseData("qwe_qw").Returns(null).SetName("SpaceAfterSingleCloseTag");
                yield return new TestCaseData("qwe__qw").Returns(null).SetName("SpaceAfterDoubleCloseTag");
                yield return new TestCaseData("qwe___qw").Returns(null).SetName("SpaceAfterTripleCloseTag");
            }
        }
    }
}

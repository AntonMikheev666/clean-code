using System.Collections.Generic;
using Markdown;
using Markdown.Tags;

namespace MakrdownTests
{
    public class TestTagFinder: TagFinder
    {
        public TestTagFinder(HashSet<string> tagStrings) : base(tagStrings)
        {
        }

        public string TestSelectProperOpenTag(string input)
        {
            return base.SelectProperOpenTag(input);
        }

        public string TestSelectProperCloseTag(string input)
        {
            return base.SelectProperCloseTag(input);
        }

        public Tag TestFindTag(string input)
        {
            return base.FindTag(input);
        }
    }
}
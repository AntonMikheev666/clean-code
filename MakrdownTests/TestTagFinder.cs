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

        public OpenTag TestSelectProperOpenTag(string input, int selectStartIndex)
        {
            return base.SelectProperOpenTag(input, selectStartIndex);
        }

        public CloseTag TestSelectProperCloseTag(string input, int selectStartIndex)
        {
            return base.SelectProperCloseTag(input, selectStartIndex);
        }

        public Tag TestFindTag(string input)
        {
            return base.FindTag(input);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Markdown.TagChanges;

namespace Markdown
{
    public class Md
	{
        private readonly TagFinder tagFinder;
        private readonly TagManager tagManager;

        public Md(IEnumerable<string> tagStrings, IEnumerable<MdHtmlTagMap> tagChanges)
	    {
	        tagFinder = new TagFinder(tagStrings);
            tagManager = new TagManager(tagChanges);
	    }

		public string RenderToHtml(string markdownString)
		{
		    var allPairedTags = tagFinder.FindMarkingTags(markdownString);

		    foreach (var tag in allPairedTags.OrderByDescending(t => t.StartIndex))
		        markdownString = tagManager.InsertTag(markdownString, tag);

		    return markdownString;
		}
	}
}
using System.Collections.Generic;
using System.Linq;

namespace Markdown
{
	public class Md
	{
        private readonly TagFinder tagFinder;
        private readonly TagChanger tagChanger;

        public Md(IEnumerable<string> tagStrings,
            Dictionary<string, string> openTagChangeTo, Dictionary<string, string> closeTagChangeTo)
	    {
	        tagFinder = new TagFinder(tagStrings);
            tagChanger = new TagChanger(openTagChangeTo, closeTagChangeTo);
	    }

		public string RenderToHtml(string markdownString)
		{
		    var allPairedTags = tagFinder.FindMarkingTags(markdownString);

		    foreach (var tag in allPairedTags.OrderByDescending(t => t.StartIndex))
		        markdownString = tagChanger.ChangeTag(markdownString, tag);

		    return markdownString;
		}
	}
}
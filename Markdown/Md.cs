using System.Collections.Generic;
using Markdown.Tags;

namespace Markdown
{
	public class Md
	{
        private TagFinder tagFinder = new TagFinder(new []{"_", "__"});
		public string RenderToHtml(string markdown)
		{
            var tagStack = new Stack<OpenTag>();
		    return "";
		}
	}
}
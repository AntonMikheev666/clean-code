namespace Markdown.TagChanges
{
    public class MdHtmlTagMap : IMdHtmlTagMap
    {
        public string MdTagString { get; }
        public string HtmlTagString { get; }

        public MdHtmlTagMap(string mdTagString, string htmlTagString)
        {
            MdTagString = mdTagString;
            HtmlTagString = htmlTagString;
        }
    }
}
using Markdown.Interfaces;

namespace Markdown.TagChanges
{
    public class Md2HtmlTagMap : IMd2HtmlTagMap
    {
        public string MdOpenTagString { get; }
        public string MdCloseTagString { get; }
        public string HtmlTagString { get; }
        public string GetHtmlOpenTagString => $"<{HtmlTagString}>";
        public string GetHtmlCloseTagString => $"</{HtmlTagString}>";

        public Md2HtmlTagMap(string mdOpenTagString, string mdCloseTagString, string htmlTagString)
        {
            MdOpenTagString = mdOpenTagString;
            MdCloseTagString = mdCloseTagString;
            HtmlTagString = htmlTagString;
        }
    }
}
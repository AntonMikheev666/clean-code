namespace Markdown.TagChanges
{
    public interface IMd2HtmlTagMap
    {
        string MdOpenTagString { get; }
        string MdCloseTagString { get; }
        string HtmlTagString { get; }
        string GetHtmlOpenTagString { get; }
        string GetHtmlCloseTagString { get; }
    }
}
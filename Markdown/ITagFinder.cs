using Markdown.Tags;

namespace Markdown
{
    public interface ITagFinder
    {
        Tag[] FindMarkingTags(string input);
    }
}
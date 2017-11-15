using Markdown.Tags;

namespace Markdown.Interfaces
{
    public interface ITagFinder
    {
        Tag[] FindMarkingTags(string input);
    }
}
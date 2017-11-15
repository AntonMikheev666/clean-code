using Markdown.Tags;

namespace Markdown.Interfaces
{
    public interface ITagManager
    {
        string InsertTag(string stringWithTags, Tag tag);
    }
}
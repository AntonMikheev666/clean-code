using Markdown.Tags;

namespace Markdown
{
    public interface ITagManager
    {
        string InsertTag(string stringWithTags, Tag tag);
    }
}
using System.Collections.Generic;
using Markdown.Tags;

namespace Markdown.Interfaces
{
    public interface ITagFinder
    {
        IEnumerable<Tag> FindMarkingTags(string input);
    }
}
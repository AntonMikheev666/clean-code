namespace Markdown.Tags
{
    public class Tag
    {
        public string TagString { get; }

        public int StartIndex { get; }

        public Tag(string tag, int startIndex)
        {
            TagString = tag;
            StartIndex = startIndex;
        }
    }
}
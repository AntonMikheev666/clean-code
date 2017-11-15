namespace Markdown.Tags
{
    public interface ITag
    {
        string TagString { get; }
        int StartIndex { get; }
    }
    public class Tag
    {
        public string TagString { get; private set; }

        public int StartIndex { get; }

        public Tag(string tag, int startIndex)
        {
            TagString = tag;
            StartIndex = startIndex;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;

            var otherTag = (Tag) obj;
            return TagString == otherTag.TagString && StartIndex == otherTag.StartIndex;
        }

        public override int GetHashCode()
        {
            return StartIndex.GetHashCode();
        }

        public override string ToString()
        {
            return $"TagString: {TagString}. Length: {TagString.Length}. StartIndex: {StartIndex}.";
        }
    }
}
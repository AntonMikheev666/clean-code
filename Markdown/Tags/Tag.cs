namespace Markdown.Tags
{
    public class Tag
    {
        public string TagString { get; private set; }

        public int StartIndex { get; }

        public Tag(string tag, int startIndex)
        {
            TagString = tag;
            StartIndex = startIndex;
        }

        public static bool OneTagStringStartsWithAnother(Tag firstTag, Tag secondTag)
        {
            return firstTag.TagString.StartsWith(secondTag.TagString) ||
                   secondTag.TagString.StartsWith(firstTag.TagString);
        }

        public static void SetTagStringToLesser(Tag firstTag, Tag secondTag)
        {
            var lesser = firstTag.TagString.Length > secondTag.TagString.Length
                ? firstTag.TagString
                : secondTag.TagString;
            firstTag.TagString = lesser;
            secondTag.TagString = lesser;
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
            return TagString.GetHashCode() + StartIndex.GetHashCode();
        }

        public override string ToString()
        {
            return $"TagString: {TagString}. Length: {TagString.Length}. StartIndex: {StartIndex}.";
        }
    }
}
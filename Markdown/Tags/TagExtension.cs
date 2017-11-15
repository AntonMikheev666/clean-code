namespace Markdown.Tags
{
    public static class TagExtension
    {
        public static bool StartsWith(this Tag firstTag, Tag secondTag)
        {
            return firstTag.TagString.StartsWith(secondTag.TagString);
        }

        /// <summary>
        /// Changes TagString of current object if tagString of secondTag have a smaller length.
        /// </summary>
        /// <param name="firstTag"></param>
        /// <param name="secondTag"></param>
        /// <returns></returns>
        public static Tag ChangeTagString(this Tag firstTag, Tag secondTag)
        {
            if (secondTag == null || secondTag.TagString.Length > firstTag.TagString.Length)
                return firstTag;
            if(firstTag is OpenTag)
                return new OpenTag(secondTag.TagString, firstTag.StartIndex);
            return new CloseTag(secondTag.TagString, firstTag.StartIndex);
        }

        /// <summary>
        /// Returns true if starts with secondTag OR appears to be a start of secondTag.
        /// </summary>
        /// <param name="firstTag"></param>
        /// <param name="secondTag"></param>
        /// <returns></returns>
        public static bool IsPairOf(this Tag firstTag, Tag secondTag)
        {
            return firstTag.StartsWith(secondTag) || secondTag.StartsWith(firstTag);
        }
    }
}
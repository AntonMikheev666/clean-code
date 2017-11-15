using System;

namespace Markdown.Tags
{
    public static class TagExtension
    {
        public static bool StartsWith(this Tag firstTag, Tag secondTag)
        {
            return firstTag.TagString.StartsWith(secondTag.TagString);
        }

        public static Tag MakePairedWith(this Tag firstTag, Tag secondTag)
        {
            // ReSharper disable once PossibleNullReferenceException
            return (Tag)firstTag.GetType()
                .GetConstructor(new[] {typeof(string), typeof(int)})
                .Invoke(new object[] {secondTag.TagString, firstTag.StartIndex});
        }

        public static bool IsPairOf(this Tag firstTag, Tag secondTag)
        {
            return firstTag.StartsWith(secondTag) || secondTag.StartsWith(firstTag);
        }
    }
}
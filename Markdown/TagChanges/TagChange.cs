namespace Markdown.TagChanges
{
    public class TagChange
    {
        public string OldTagString { get; }
        public string NewTagString { get; }

        public TagChange(string oldTagString, string newTagString)
        {
            OldTagString = oldTagString;
            NewTagString = newTagString;
        }
    }
}
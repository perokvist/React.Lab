using System.IO;
using Newtonsoft.Json.Linq;

namespace React.Lab
{
    public static class CommentsContainer
    {
        private static JArray _comments;
        public static JArray Comments
        {
            get { return _comments ?? (_comments = JArray.Parse(File.ReadAllText("_comments.json"))); }
        }
    }
}
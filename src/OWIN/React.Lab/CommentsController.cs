using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace React.Lab
{
    [RoutePrefix("comments.json")]
    public class CommentsController : ApiController
    {
        private readonly JArray _comments;

        public CommentsController() : this(CommentsContainer.Comments)
        {}

        public CommentsController(JArray comments)
        {
            _comments = comments;
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            return Json(_comments);
        }

        [Route("")]
        public IHttpActionResult Post(JToken comment)
        {
            _comments.Add(comment);
            return Json(_comments);
        }
    }
}
using System.Web.Http;

namespace GetCats.Controllers.WebAPI
{
    [Authorize]
    public class TestController : ApiController
    {
        public string Get()
        {
            return "got here!";
        }
    }
}

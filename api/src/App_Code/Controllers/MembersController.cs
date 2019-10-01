using System.Web.Http;
using PN.Models;

namespace PN.Controllers
{
    [RoutePrefix("api/v1")]
    public class MembersController : ApiController
    {
        [Route("members")]
        [HttpPost]
        public string Register(string email, string password)
        {
            return "";
        }

        [Route("members")]
        [HttpGet]
        public string Testing()
        {
            return "";
        }
    }
}

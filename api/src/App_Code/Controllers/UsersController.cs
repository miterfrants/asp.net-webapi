using System.Linq;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

using Sample.Services;
using Sample.Models;
using Sample.DataServices;
using Sample.Constants;

namespace Sample.Controllers
{
    [RoutePrefix("api/v1")]
    public class UsersController : ApiController
    {
        [Route("users")]
        [HttpPost]
        public IHttpActionResult Register([FromBody] dynamic postBody)
        {
            #region validation
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(postBody.email.Value);
            if (!match.Success)
            {
                return ResponseMessage(
                    Request.CreateResponse(
                        HttpStatusCode.ExpectationFailed,
                        ERROR.INVALID_EMAIL
                    ));
            }

            if (postBody.password.Value.Length < 12)
            {
                return ResponseMessage(
                     Request.CreateResponse(
                         HttpStatusCode.ExpectationFailed,
                         new ErrorObject(ERROR.PASSWORD_TOO_SHORT.ErrorCode, ERROR.PASSWORD_TOO_SHORT.ErrorMessage + ": Use a minimum password length of 12 or more characters if permitted")
                     ));
            }

            string specialCharacter = "!@#$%&*?-_";
            char[] passwordCharacterList = postBody.password.Value.ToCharArray();
            int passwordCountOfSpeicalCharacter = passwordCharacterList.Where(x => specialCharacter.Contains(x)).Count();
            if (passwordCountOfSpeicalCharacter == 0)
            {
                return ResponseMessage(
                     Request.CreateResponse(
                         HttpStatusCode.ExpectationFailed,
                         new ErrorObject(ERROR.PASSWORD_TOO_WEAK.ErrorCode, ERROR.PASSWORD_TOO_WEAK.ErrorMessage + $": password have to include one character of {specialCharacter}")
                     ));
            }
            #endregion

            user newUser = UsersDataService.Register(postBody.email.Value, postBody.password.Value);
            return Json(new
            {
                data = newUser
            });
        }

        [Route("users/for")]
        [HttpPost]
        public IHttpActionResult ForgotPassword([FromBody] dynamic postBody)
        {
            
            user newUser = UsersDataService.Register(postBody.email.Value, postBody.password.Value);
            return Json(new
            {
                data = newUser
            });
        }

        [Route("users")]
        [HttpGet]
        public string Testing()
        {
            return JWTService.GenerateToken("miterfrants@gmail.com", 1, "admin");
        }

        [Route("users/test")]
        [HttpGet]
        public IHttpActionResult CCCC(string token)
        {
            return Json(JWTService.DecodeToken(token));
        }
    }
}

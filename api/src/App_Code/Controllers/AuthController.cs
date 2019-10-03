using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using Sample.Services;
using Sample.Models;
using Sample.DataServices;
using Sample.Constants;


namespace Sample.Controllers
{
    [RoutePrefix("api/v1/auth")]
    public class AuthController : ApiController
    {
        [Route("forgot-password")]
        [HttpPost]
        public IHttpActionResult ForgotPassword([FromBody] dynamic postBody)
        {
            string email = postBody.email.Value.ToString();
            string redirectUrl = postBody.redirectUrl.value.ToString();

            #region validation
            // TODO: postBody validation
            user user = UsersDataService.GetUserByEmail(email);
            if (user == null)
            {

            }
            #endregion

            bool setStateResult = UsersDataService.SetupUserToForgotPasswordState(user);
            if (setStateResult)
            {
                // TODO: Implment SMTPService; 
                // SMTPService.Send($"{ENV.END_POINT}/auth/reset-password?token={user.reset_password_token}&redirect-url={HttpUtility.UrlEncode(redirectUrl);}";
            }

            return Json(new
            {
                data = CUSTOM_RESPONSE.STATUS.OK.ToString()
            });
        }

        // GET: Auth
        [Route("reset-password")]
        [HttpGet]
        public IHttpActionResult ResetPassword([FromBody] dynamic postBody)
        {
            return "";
        }
    }
}
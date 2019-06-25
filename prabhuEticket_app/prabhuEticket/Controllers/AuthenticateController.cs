using prabhuEticket.AppCode;
using prabhuEticket.Models;
using prabhuEticket.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace prabhuEticket.Controllers
{
    [RoutePrefix("api/v1")]
    public class AuthenticateAPIController : ApiController
    {
        AuthenticationRepo authenticationRepo = new AuthenticationRepo();
        [HttpPost,Route("authenticate")]
        public authenticate_output authenticate_Output([FromBody]authenticate_request request)
        {
            string api_key = "";
            if (Request.Headers.Contains("api-key"))
            {
                try
                {
                    api_key = Request.Headers.GetValues("api-key").First();
                    //access_token = Request.Headers.GetValues("access-token").First();
                }
                catch
                {
                    api_key = "";
                    //access_token = "";
                }
            }
            authenticate_output output = new authenticate_output();
            output = authenticationRepo.validate_login(request, api_key);
            return output;
        }
    }
}

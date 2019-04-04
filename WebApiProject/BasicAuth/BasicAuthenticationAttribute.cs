using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiProject.BasicAuth
{
    //[BasicAuthentication] filter
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        // Basic authentication
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null) // if header is not empty
            {
                var authToken = actionContext.Request.Headers
                    .Authorization.Parameter;

                // decoding the header
                var decodeauthToken = System.Text.Encoding.UTF8.GetString(
                    Convert.FromBase64String(authToken));

                // spliting it ( from username:password )   
                var arrUserNameandPassword = decodeauthToken.Split(':');

                // [0] = username [1] = password. 
                if (IsAuthorizedUser(arrUserNameandPassword[0], arrUserNameandPassword[1]))
                {
                    // setting current principle  
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(arrUserNameandPassword[0]), null);
                }
                else
                { // if user don't have rights.
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            { // if header is empty
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
        // determine if user has rights or not.
        public bool IsAuthorizedUser(string username, string password)
        {
            // we can implement whatever custom logic we want here ( check in DB , etc. ). 
            // in our case we will hardcode admin identity.
            if ((username == "root") && (password == "root")) return true;
            return false;
        }
    }
}
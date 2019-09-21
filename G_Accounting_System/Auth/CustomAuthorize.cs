using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using G_Accounting_System.ENT;
using G_Accounting_System.APP;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Text;
using System.Security.Principal;
using System.Threading;
using System.Security.Claims;

namespace G_Accounting_System.Auth
{
    public class CustomAuthorize : System.Web.Http.AuthorizeAttribute
    {
        //public override void OnAuthorization(HttpActionContext actionContext)
        //{
        //    var authHeader = actionContext.Request.Headers.GetValues("authenticationToken");

        //    if (authHeader != null)
        //    {
        //        //var authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
        //        var authenticationToken = Convert.ToString(actionContext.Request.Headers.GetValues("authenticationToken").FirstOrDefault());
        //        Tokens accesstokens = new Catalog().GetToken(authenticationToken);

        //        string authenticationTokenPersistant = accesstokens.AccessToken.ToString();

        //        // Replace this with your own system of security / means of validating credentials

        //        if (authenticationTokenPersistant == authenticationToken)
        //        {
        //            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK);

        //            return;
        //        }
        //    }

        //    HandleUnathorized(actionContext);
        //}

        //private static void HandleUnathorized(HttpActionContext actionContext)
        //{
        //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        //    actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='Data' location = 'http://localhost:");
        //}


        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            string authenticationTokenPersistant = null;
            if (actionContext.Request.Headers.Contains("authenticationToken"))
            {
                // get value from header
                string authenticationToken = Convert.ToString(actionContext.Request.Headers.GetValues("authenticationToken").FirstOrDefault());
                // authenticationTokenPersistant
                // it is saved in some data store
                // i will compare the authenticationToken sent by client with
                // authenticationToken persist in database against specific user, and act accordingly

                Tokens accesstokens = new Catalog().GetToken(authenticationToken);
                if (accesstokens != null)
                {
                    authenticationTokenPersistant = accesstokens.AccessToken.ToString();
                    if (authenticationTokenPersistant != authenticationToken)
                    {
                        HttpContext.Current.Response.AddHeader("authenticationToken", authenticationToken);
                        HttpContext.Current.Response.AddHeader("AuthenticationStatus", "NotAuthorized");
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "NotAuthorized");
                        
                        return;
                    }

                }
                else
                {
                    HttpContext.Current.Response.AddHeader("authenticationToken", authenticationToken);
                    HttpContext.Current.Response.AddHeader("AuthenticationStatus", "NotAuthorized");
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "NotAuthorized");
                   
                    return;
                }

                var principal = new GenericPrincipal(new GenericIdentity(accesstokens.User_id.ToString()), null);
                HttpContext.Current.User = principal;
                HttpContext.Current.Response.AddHeader("authenticationToken", authenticationToken);
                HttpContext.Current.Response.AddHeader("AuthenticationStatus", "Authorized");
                HttpContext.Current.Response.AddHeader("Content-Type", "application/json; charset=utf-8");
                

                return;
                //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK);

                //actionContext.Response = new HttpResponseMessage
                //{
                //    StatusCode = HttpStatusCode.OK,
                //    Content = new StringContent("You are unauthorized to access this resource")
                //};

            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                actionContext.Response.ReasonPhrase = "Please provide valid inputs";
            }
            base.OnAuthorization(actionContext);
        }
    }
}
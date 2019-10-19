using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rakuten.Test.Web
{
    /// <summary>
    /// Summary description for Email
    /// </summary>
    public class Document : IHttpHandler
    {

        private UserService.UserSoap _userService;

        public Document()
        {
            _userService = new UserService.UserSoapClient();
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            bool result = false;

            try
            {
                var _exists = _userService.DocumentExists(new UserService.DocumentExistsRequest { Body = new UserService.DocumentExistsRequestBody { documentId = context.Request["documentId"] } });
                result = _exists.Body.DocumentExistsResult.Data.Status == UserService.ServiceResponseStatus.Yes;
            }
            catch
            {
                result = false;
            }

            context.Response.Write(JsonConvert.SerializeObject(new { status = result }));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
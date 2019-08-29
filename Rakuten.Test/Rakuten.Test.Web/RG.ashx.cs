using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rakuten.Test.Web
{
    /// <summary>
    /// Summary description for RG
    /// </summary>
    public class RG : IHttpHandler
    {

        private UserService.UserSoap _userService;

        public RG()
        {
            _userService = new UserService.UserSoapClient();
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            bool result = false;

            try
            {
                var _exists = _userService.RGExists(new UserService.RGExistsRequest { Body = new UserService.RGExistsRequestBody { rg = context.Request["rg"] } });
                result = _exists.Body.RGExistsResult.Data.Status == UserService.ServiceResponseStatus.Yes;
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
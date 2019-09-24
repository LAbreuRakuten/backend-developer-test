using log4net;
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
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                var _exists = _userService.RGExists(new UserService.RGExistsRequest { Body = new UserService.RGExistsRequestBody { RG = context.Request["RG"] } });
                result = _exists.Body.RGExistsResult.Data.Status == UserService.ServiceResponseStatus.Yes;

                if (result)
                {
                    Log.Warn("RG já existe na base");
                }
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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rakuten.Test.Web
{
    /// <summary>
    /// Summary description for RgCpf
    /// </summary>
    public class RgCpf : IHttpHandler
    {

        private UserService.UserSoap _userService;

        public RgCpf()
        {
            _userService = new UserService.UserSoapClient();
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            bool result = false;

            try
            {
                var _existsCpf = _userService.DocumentExists(new UserService.DocumentExistsRequest { Body = new UserService.DocumentExistsRequestBody { documentId = context.Request["documentId"] } });
                var _existsRg = _userService.DocumentRgExists(new UserService.DocumentRgExistsRequest { Body = new UserService.DocumentRgExistsRequestBody { documentRg = context.Request["documentRg"] } });

                result = (_existsCpf.Body.DocumentExistsResult.Data.Status == UserService.ServiceResponseStatus.Yes ||
                    _existsRg.Body.DocumentRgExistsResult.Data.Status == UserService.ServiceResponseStatus.Yes);
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
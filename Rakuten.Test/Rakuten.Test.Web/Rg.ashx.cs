using Newtonsoft.Json;
using System.Web;

namespace Rakuten.Test.Web
{
    /// <summary>
    /// Summary description for Document
    /// </summary>
    public class Rg : IHttpHandler
    {

        private UserService.UserSoap _userService;

        public Rg()
        {
            _userService = new UserService.UserSoapClient();
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            bool result = false;

            try
            {
                var _exists = _userService.RgExists(new UserService.RgExistsRequest { Body = new UserService.RgExistsRequestBody { rg = context.Request["rg"] } });
                result = _exists.Body.RgExistsResult.Data.Status == UserService.ServiceResponseStatus.Yes;
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
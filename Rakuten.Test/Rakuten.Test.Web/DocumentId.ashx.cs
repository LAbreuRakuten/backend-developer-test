using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rakuten.Test.Web
{
    /// <summary>
    /// Summary description for DocumentID
    /// </summary>
    public class DocumentId : IHttpHandler
    {

        private UserService.UserSoap _userService;

        public DocumentId()
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
                if (result)
                {                //Salva no arquivo txt
                    Log.Save("Documento já está cadastrado no banco de dados");
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
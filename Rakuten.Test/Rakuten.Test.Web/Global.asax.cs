using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Rakuten.Test.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_Error(object sender, EventArgs e)
        {
            //URL da pagina
            string URL = HttpContext.Current.Request.Url.AbsoluteUri.ToString();

            //Criando um objeto do tipo System.Exception e pegar o último erro
            Exception error = Server.GetLastError();

            //Se error não for null
            if (error != null)
            {
                string strErrorMessage = string.Empty;
                //Pegando o código do erro.
                //Caso o erro seja do tipo HttpException vai pegar o código do erro, 
                //ou então definirá o código como 500
                int code = (error is HttpException) ? (error as HttpException).GetHttpCode() : 500;

                //Pegando os erros e montar a mensagem
                while (error.InnerException != null)
                {
                    //código do erro
                    strErrorMessage += "Código:" + code + "\r\n";
                    //Mensagem  de erro (InnerException)
                    strErrorMessage += "Mensagem:" + error.InnerException;

                    //Salva no arquivo txt
                    Log.Save(strErrorMessage);

                    //Próximo erro
                    error = error.InnerException;
                }

                //Caso o erro seja 404, vamos salvar como URL não encontrada.
                if (code == 404)
                {
                    strErrorMessage += "URL não encontrada";

                    //Definir o StatusCode da página para 404
                    Response.StatusCode = 404;

                    //Salva no arquivo txt
                    Log.Save(strErrorMessage);

                }
                else
                {
                    //Definir o StatusCode da página para 500 
                    Response.StatusCode = 500;
                }
            }
        }

    }
}
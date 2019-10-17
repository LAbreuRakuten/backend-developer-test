using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;

namespace Rakuten.Test.Web
{
    public class Log
    {
        /// <summary>
        /// Salva o LOG de erros, cria um novo arquivo para cada dia.
        /// </summary>
        /// <param name="msg"></param>
        public static void Save(string msg)
        {
            //Path do arquivo txt
            //C:\inetpub\wwwroot\nomedosite\App_Data\log-20111024.txt
            string strFile = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/log-" + DateTime.Now.ToString("yyyyMMdd") + ".txt");

            //Se arquivo não existir
            if (!File.Exists(strFile))
            {
                //Criar o arquivo, 
                //Estou usando o using para fazer o Dispose automático do arquivo após criá-lo.
                using (FileStream fs = File.Create(strFile)) { }
            }

            //Escreve o Erro no txt
            //Os erros são concatenados, ou seja, não são sobreescritos.
            using (StreamWriter w = File.AppendText(strFile))
            {
                string _msg = string.Empty;
                string URL = HttpContext.Current.Request.Url.AbsoluteUri.ToString();

                //Adicionar um separador
                _msg = "#############################################################\r\n";
                //Data do erro
                _msg += "Data:" + DateTime.Now.ToString("yyyyMMdd-HH:mm:ss") + "\r\n";
                //URL do erro
                _msg += "URL:" + URL + "\r\n";
                //Adicionando a mensagem
                _msg += msg;
                //quebra de lina e nova linha
                _msg += "\r\n\r\n";

                //Escreve no arquivo
                w.Write(_msg);
                //Fecha
                w.Close();
            }
        }
    }
}
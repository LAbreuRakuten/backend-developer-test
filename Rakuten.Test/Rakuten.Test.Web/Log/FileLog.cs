using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Rakuten.Test.Web.Log
{
    public static class FileLog
    {
        public static void Log(string message)
        {
            try
            {
                string pathLog = ConfigurationManager.AppSettings.Get("PastaLog");

                if (!Directory.Exists(pathLog))
                    Directory.CreateDirectory(pathLog);

                string fullFileName = Path.Combine(ConfigurationManager.AppSettings.Get("PastaLog"), "LogExecução_"+ DateTime.Now.ToString("yyyyMMdd") + ".log");

                message = DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss") + " : " + message + "\n";

                // Grava o texto no arquivo (e cria o arquivo se não existir)
                File.AppendAllText(fullFileName, message);
            }
            catch { }
        }
    }
}
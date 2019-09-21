using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace G_Accounting_System
{
    public class ApiRequestToJson
    {
        public string ToJson()
        {
            HttpRequest resolveRequest = HttpContext.Current.Request;
            resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
            string strJson = new StreamReader(resolveRequest.InputStream).ReadToEnd();
            return strJson;
        }
    }
}
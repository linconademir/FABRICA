using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace CEAG.WEB.Constants
{
    public class Erro
    {
        public int CodErro { get; set; }
        public HttpStatusCode statusCode { get; set; }
        public string MensagemErro { get; set; }
        public string UrlChamada { get; set; }

    }
}
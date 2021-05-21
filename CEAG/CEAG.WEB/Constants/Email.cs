using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEAG.WEB.Constants
{
    public class Email
    {
        public string Recebedor { get; set; }
        public string Assunto { get; internal set; }
        public string Mensagem { get; internal set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Debito
{
    public class QtdDebitoExibicaoViewModel
    {
        public int DescricaoNumeral { get; set; }
        public string DescricaoNome { get; set; }
        public int QtdPagoDoPeriodo { get; set; }
        public int QtdPagoGeral { get; set; }
        public double ValorPagoDoPeriodo { get; set; }
        public double ValorPagoGeral { get; set; }
        public int QtdPagoAtrasado { get; set; }
        public double ValorPagoAtrasado { get; set; }
        public int QtdAtrasadoEmAberto { get; set; }
        public double ValorAtrasadoEmAberto { get; set; }
    }
}
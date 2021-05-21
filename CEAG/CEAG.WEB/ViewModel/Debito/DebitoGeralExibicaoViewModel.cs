using CEAG.WEB.ViewModel.Aluno;
using DotNet.Highcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Debito
{
    public class DebitoGeralExibicaoViewModel
    {
        public int QtdDebitosVencidosNoMes { get; set; }
        public int QtdDebitosVencidosMesesGeral { get; set; }
        public double ValorDebitosVencidosNoMes { get; set; }
        public double ValorDebitosVencidosMesesGeral { get; set; }
        public int QtdAlunosJaAtrasaram { get; set; }
        public int QtdAlunoPagamEmDia { get; set; }
        //public double ValorAlunoJaAtrasaram { get; set; }
        //public double ValorAlunosPagamEmDia { get; set; }
        public List<QtdDebitoExibicaoViewModel> MesPagosEAtrasados { get; set; }
        public List<QtdDebitoExibicaoViewModel> AnoPagosEAtrasados { get; set; }
        public List<Highcharts> Graficos { get; set; }

    }
}
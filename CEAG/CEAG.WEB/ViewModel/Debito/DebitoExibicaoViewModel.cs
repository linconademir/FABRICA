using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.AlunoMatricula;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Debito
{
    public class DebitoExibicaoViewModel
    {
        public int CodDebito { get; set; }
        public DateTime Inclusao { get; set; }

        [Display(Name = "PAGAMENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? Pagamento { get; set; }

        [Display(Name = "PERÍODO")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/yyyy}")]
        public DateTime Periodo { get; set; }

        [Display(Name = "VENCIMENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Vencimento { get; set; }

        public DateTime? Negociacao { get; set; }

        [Display(Name = "TIPO")]
        public string TipoDebito { get; set; }

        [Display(Name = "RESPONSÁVEL")]
        public string Responsavel { get; set; }

        public string IdenficadorHexa { get; set; }

        [Display(Name = "VALOR DÉBITO")]
        [DataType(DataType.Currency)]
        public double ValorDebito { get; set; }

        [Display(Name = "VALOR PAGO")]
        [DataType(DataType.Currency)]
        public double? ValorPago { get; set; }

        [Display(Name = "DESCONTO")]
        public double? Desconto { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        public string Descricao { get; set; }

        [Display(Name = "OBSERVAÇÃO")]
        public string Observacao { get; set; }

        [Display(Name = "STATUS")]
        public string Status { get; set; }

        [Display(Name = "FORMA DE PAGAMENTO")]
        public string FormaPagamento { get; set; }

        public int? CodAcesso { get; set; }

        public int CodAlunoMatricula { get; set; }

        public AlunoMatriculaExibicaoViewModel AlunoMatriculaExibicaoViewModel { get; set; }

        public AlunoExibicaoViewModel AlunoExibicaoViewModel { get; set; }

        public List<DebitoHistoricoExibicaoViewModel> DebitoHistoricoExibicaoViewModels { get; set; }
    }
}
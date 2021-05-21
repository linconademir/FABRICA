using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Turma
{
    public class TurmaViewModel
    {
        private string _descricao { get; set; }
        private string _segmento { get; set; }
        private string _periodo { get; set; }
        private string _status { get; set; }
        private string _nivel { get; set; }

        public int CodTurma { get; set; }

        [Display(Name = "DESCRIÇÃO DA TURMA")]
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MaxLength(200, ErrorMessage = "A descrição não poderá conter mais que 160 caracteres.")]
        public string Descricao
        {
            get => _descricao;
            set => _descricao = value?.ToUpper();
        }

        [Display(Name = "SEGMENTO")]
        [MaxLength(200, ErrorMessage = "O segmento não poderá conter mais que 160 caracteres.")]
        public string Segmento
        {
            get => _segmento;
            set => _segmento = value?.ToUpper();
        }

        [Display(Name = "PERÍODO")]
        public string Periodo
        {
            get => _periodo;
            set => _periodo = value?.ToUpper();
        }

        [Display(Name = "VAGAS")]
        public int Vagas { get; set; }

        [Display(Name = "STATUS")]
        public string Status
        {
            get => _status;
            set => _status = value?.ToUpper();
        }

        [Display(Name = "ANO LETIVO")]
        public int AnoLetivo { get; set; }

        [Display(Name = "FECHAMENTO")]
        public DateTime? Fechamento { get; set; }

        [Display(Name = "ABERTURA")]
        public DateTime? Abertura { get; set; }
            
        [Display(Name = "NÍVEL")]
        public string Nivel
        {
            get => _nivel;
            set => _nivel= value?.ToUpper();
        }

        [Display(Name = "DESCONTO POR ANTECIPAÇÃO")]
        [DataType(DataType.Currency)]
        public double? DescontoMensalidade { get; set; }

        [Display(Name = "MODELO DE HORÁRIO")]
        public int? CodHorario { get; set; }

        [Display(Name = "PORTARIA")]
        public string Portaria { get; set; }
        public DateTime Inclusao { get; set; }
        public int? CodEscola { get; set; }
    }
}
using CEAG.WEB.ViewModel.Aluno;
using CEAG.WEB.ViewModel.Aula;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Advertencia
{
    public class AdvertenciaExibicaoViewModel
    {
        private string _tipo;
        private string _titulo;
        private string _descricao;
        private string _urgencia;
        private string _motivo;


        public int CodAdvertencia { get; set; }

        [Display(Name = "TIPO")]
        public string Tipo
        {
            get => _tipo;
            set => _tipo = value?.ToUpper();
        }

        [Display(Name = "TITULO")]
        public string Titulo
        {
            get => _titulo;
            set => _titulo = value?.ToUpper();
        }

        [Display(Name = "MOTIVO")]
        public string Motivo
        {
            get => _motivo;
            set => _motivo = value?.ToUpper();
        }



        [Display(Name = "DESCRIÇÃO")]
        public string Descricao
        {
            get => _descricao;
            set => _descricao = value?.ToUpper();
        }


        [Display(Name = "URGÊNCIA")]
        public string Urgencia
        {
            get => _urgencia;
            set => _urgencia = value?.ToUpper();
        }

        [Display(Name = "INCLUSÃO")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Inclusao { get; set; }

        [Display(Name = "COMPARECIMENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Comparecimento { get; set; }

        public int CodAcesso { get; set; }

        [Display(Name = "RESOLUÇÃO")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? Resolucao { get; set; }


        [Display(Name = "STATUS")]
        public string Status { get; set; }

        public AlunoExibicaoViewModel AlunoExibicaoViewModel { get; set; }
        public AulaExibicaoViewModel AulaExibicaoViewModel { get; set; }
    }
}
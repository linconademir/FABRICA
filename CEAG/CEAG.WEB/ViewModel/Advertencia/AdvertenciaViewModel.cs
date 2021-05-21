using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Advertencia
{
    public class AdvertenciaViewModel
    {
        private string _tipo;
        private string _titulo;
        private string _descricao;
        private string _urgencia;
        private string _motivo;

        public int CodAdvertencia { get; set; }

        [Display(Name = "TIPO")]
        [Required(ErrorMessage = "O tipo é obrigatório")]
        [MaxLength(100, ErrorMessage = "O tipo não poderá conter mais que 100 caracteres.")]
        public string Tipo
        {
            get => _tipo;
            set => _tipo = value?.ToUpper();
        }


        [Display(Name = "MOTIVO")]
        [Required(ErrorMessage = "O motivo é obrigatório")]
        [MaxLength(100, ErrorMessage = "O motivo não poderá conter mais que 100 caracteres.")]
        public string Motivo
        {
            get => _motivo;
            set => _motivo = value?.ToUpper();
        }

        [Display(Name = "TITULO")]
        [Required(ErrorMessage = "O titulo é obrigatório")]
        [MaxLength(200, ErrorMessage = "O titulo não poderá conter mais que 200 caracteres.")]
        public string Titulo
        {
            get => _titulo;
            set => _titulo = value?.ToUpper();
        }


        [Display(Name = "DESCRIÇÃO")]
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [DataType(DataType.MultilineText)]
        public string Descricao
        {
            get => _descricao;
            set => _descricao = value?.ToUpper();
        }


        [Display(Name = "URGÊNCIA")]
        [Required(ErrorMessage = "A urgência é obrigatória")]
        public string Urgencia
        {
            get => _urgencia;
            set => _urgencia = value?.ToUpper();
        }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Inclusao { get; set; }

        [Display(Name = "DATA DE COMPARECIMENTO")]
        [Required(ErrorMessage = "A data de comparecimento é obrigatória.")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Comparecimento { get; set; }

        public int CodAcesso { get; set; }

        public DateTime? Resolucao { get; set; }

        public int CodAlunoMatricula { get; set; }

        public int? CodAula { get; set; }
    }
}
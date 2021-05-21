using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Feriado
{
    public class FeriadoViewModel
    {
        private string _descricao;
        private string _tipo;


        public int CodFeriado { get; set; }

        [Display(Name = "DATA")]
        [Required(ErrorMessage = "A data é obrigatória")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Data { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MaxLength(160, ErrorMessage = "A descrição não poderá conter mais que 160 caracteres.")]
        public string Descricao
        {
            get => _descricao;
            set => _descricao = value?.ToUpper();
        }

        [Display(Name = "TIPO")]
        public string Tipo
        {
            get => _tipo;
            set => _tipo = value?.ToUpper();
        }

        public int? CodEscola { get; set; }
    }
}
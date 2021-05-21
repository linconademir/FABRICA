using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Informacao
{
    public class InformacaoViewModel
    {
        private string _tipo { get; set; }
        private string _descricao { get; set; }

        public int CodInformacao { get; set; }

        [Display(Name = "TIPO")]
        [Required(ErrorMessage = "Selecione o Tipo de informação")]
        public string Tipo
        {
            get => _tipo;
            set => _tipo = value?.ToUpper();
        }

        [Display(Name = "DESCRIÇÃO")]
        [Required(ErrorMessage = "A Descrição é obrigatória")]
        [MaxLength(300, ErrorMessage = "A Descrição não poderá conter mais que 300 caracteres.")]
        public string Descricao
        {
            get => _descricao;
            set => _descricao = value?.ToUpper();
        }

        public int CodAluno { get; set; }
    }
}
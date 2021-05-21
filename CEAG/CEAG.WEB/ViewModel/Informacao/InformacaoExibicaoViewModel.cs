using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Informacao
{
    public class InformacaoExibicaoViewModel
    {
        private string _tipo { get; set; }
        private string _descricao { get; set; }

        public int CodInformacao { get; set; }

        [Display(Name = "TIPO")]
        public string Tipo
        {
            get => _tipo;
            set => _tipo = value?.ToUpper();
        }

        [Display(Name = "DESCRIÇÃO")]
        public string Descricao
        {
            get => _descricao;
            set => _descricao = value?.ToUpper();
        }

        public int CodAluno { get; set; }
    }
}
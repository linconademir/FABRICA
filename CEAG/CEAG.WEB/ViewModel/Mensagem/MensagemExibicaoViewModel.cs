using CEAG.WEB.ViewModel.Aluno;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Mensagem
{
    public class MensagemExibicaoViewModel
    {
        public int CodMensagem { get; set; }

        [Display(Name = "TITULO")]
        public string Titulo { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        public string Descricao { get; set; }

        [Display(Name = "STATUS")]
        public string Status { get; set; }

        [Display(Name = "INCLUSÃO")]
        public DateTime Inclusao { get; set; }

        [Display(Name = "ENVIO")]
        public DateTime? Envio { get; set; }

        [Display(Name = "DESTINATÁRIO")]
        public string Destinatario { get; set; }

        [Display(Name = "CÓPIA")]
        public string DestinatarioCopia { get; set; }

        [Display(Name = "TIPO")]
        public string Tipo { get; set; }

        [Display(Name = "PRIORIDADE")]
        public int Prioridade { get; set; }

        public AlunoViewModel AlunoViewModel { get; set; }
    }
}
using CEAG.DOMINIO;
using CEAG.WEB.ViewModel.Funcionario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CEAG.WEB.ViewModel.Acesso
{
    public class AcessoViewModel
    {
        public int CodAcesso { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public string Ativo { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Foto { get; set; }
        public int? CodEscola { get; set; }
        public int? CodEscolaGrupo { get; set; }
        public int CodFuncionario { get; set; }
        public List<TurmaFuncionarioDisciplina> TurmaFuncionarioDisciplinas { get; set; }
        [HiddenInput]
        public string ReturnUrl { get; set; }
        public Escola Escola { get; set; }
        public EscolaGrupo EscolaGrupo { get; set; }
        public List<FuncionarioExibicaoViewModel> Funcionarios { get; set; }
        public List<AcessoRole> AcessoRoles { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Acesso
    {
        [Key]
        public int CodAcesso { get; set; }


        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }
        public string Ativo { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Foto { get; set; }
        
        public int? CodEscola { get; set; }
        public int? CodEscolaGrupo { get; set; }
        public int? CodFuncionario { get; set; }

        public virtual Funcionario Funcionario { get; set; }

        public virtual Escola Escola { get; set; }
        public virtual EscolaGrupo EscolaGrupo { get; set; }
        public virtual List<Funcionario> Funcionarios { get; set; }
        public virtual List<AcessoRole> AcessoRoles { get; set; }


        
    }
}

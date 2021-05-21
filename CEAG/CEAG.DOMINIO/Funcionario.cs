using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Funcionario
    {
        [Key]
        public int CodFuncionario { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Sexo { get; set; }
        public string Rg { get; set; }
        public string Funcao { get; set; }
        public string Titulacao { get; set; }
        public string Observacao { get; set; }
        public string Nacionalidade { get; set; }
        public string Naturalidade { get; set; }

        public string RgOrgaoEmissor { get; set; }
        public string RgUf { get; set; }

        public DateTime Nascimento { get; set; }
        public DateTime Inclusao { get; set; }
        public string Email { get; set; }
        public int CodLogradouro { get; set; }
        public int CodEscola { get; set; }
        public int? CodAcesso { get; set; }
        public DateTime? Cancelamento { get; set; }

        public virtual Logradouro Logradouro { get; set; }
        public virtual Escola Escola { get; set; }
        public virtual Acesso Acesso { get; set; }
        public virtual List<Telefone> Telefones{ get; set; }

        public virtual List<FuncionarioDisciplina> FuncionarioDisciplinas { get; set; }
        public virtual List<TurmaFuncionarioDisciplina> TurmaFuncionarioDisciplinas { get; set; }
        public virtual List<AlunoMatriculaUnidade> AlunoMatriculaUnidades { get; set; }
        public virtual List<Acesso> Acessos { get; set; }

    }
}

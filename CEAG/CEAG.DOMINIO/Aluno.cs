using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CEAG.DOMINIO
{
    public class Aluno
    {
        [Key]
        public int CodAluno { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }

        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string RgUf { get; set; }
        public string RgOrgaoEmissor { get; set; }
        public DateTime Nascimento { get; set; }
        public DateTime Inclusao { get; set; }
        public string Email { get; set; }
        public string MaeNome { get; set; }
        public string MaeProfissao { get; set; }
        public string PaiNome { get; set; }
        public string PaiProfissao { get; set; }
        public int CodEscola { get; set; }
        public int CodLogradouro { get; set; }
        public string TipoSanguineo { get; set; }
        public string Nacionalidade { get; set; }
        public string Naturalidade { get; set; }
       
        public string FatorRH { get; set; }

        public int? CodAcesso { get; set; }


        public virtual List<Informacao> Informacaos { get; set; }
        public virtual List<Responsavel> Responsavels { get; set; }
        public virtual List<Telefone> Telefones { get; set; }
        public virtual List<AlunoMatricula> AlunoMatriculas { get; set; }
        public virtual Escola Escola { get; set; }
        public virtual Logradouro Logradouro { get; set; }
        public virtual List<AlunoQuestionario> AlunoQuestionarios { get; set; }
        public virtual List<ParentescoAluno> ParentescoAlunos { get; set; }
    }
}

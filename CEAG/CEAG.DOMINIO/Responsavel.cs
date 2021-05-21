using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Responsavel
    {
        [Key]
        public int CodResponsavel { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Tipo { get; set; }
        public string Rg { get; set; }
        public string RgOrgaoEmissor { get; set; }
        public string RgUf { get; set; }
        public string Profissao { get; set; }
        public string EstadoCivil { get; set; }
        public string Naturalidade { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string RecebeEmail { get; set; }
        public string Nacionalidade { get; set; }
        public int? CodAcesso { get; set; }
        public int CodLogradouro { get; set; }
        public int CodAluno { get; set; }
        public Logradouro Logradouro { get; set; }
        public string DeclaraIR { get; set; }
        public Aluno Aluno { get; set; }
        public virtual List<Telefone> Telefones { get; set; }
    }
}

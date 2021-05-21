using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Logradouro
    {
        [Key]
        public int CodLogradouro { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
        public string Referencia { get; set; }
        public int? CodAcesso { get; set; }
        public virtual List<Aluno> Alunos { get; set; }
        public virtual List<Escola> Escolas { get; set; }
        public virtual List<Funcionario> Funcionarios { get; set; }
        //public virtual List<Escola> Escolas { get; set; }
        public virtual List<Responsavel> Responsavels { get; set; }
    }
}

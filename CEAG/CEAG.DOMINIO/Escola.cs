using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.DOMINIO
{
    public class Escola
    {
        [Key]
        public int CodEscola { get; set; }
        public string Razao { get; set; }
        public string Fantasia { get; set; }
        public string Cnpj { get; set; }
        public string Logo { get; set; }
        
        public string Autorizacao { get; set; }
        public DateTime? PublicacaoDiarioOficial{ get; set; }
        public string Inscricao { get; set; }
        public int? CodLogradouro { get; set; }

        public virtual Logradouro Logradouro { get; set; }

        public int? CodEscolaGrupo { get; set; }
        public virtual EscolaGrupo EscolaGrupo { get; set; }
        public virtual List<Aluno> Alunos { get; set; }
        public virtual List<MensalidadeValor> MensalidadeValors { get; set; }
        public virtual List<Taxa> Taxas{ get; set; }
        public virtual List<Turma> Turma{ get; set; }
        public virtual List<Matricula> Matriculas { get; set; }
        public virtual List<Funcionario> Funcionarios{ get; set; }
        public virtual List<Disciplina> Disciplinas { get; set; }
        public virtual List<Acesso> Acessos { get; set; }
        public virtual List<Horario> Horarios { get; set; }
        public virtual List<Feriado> Feriados { get; set; }
        public virtual List<Unidade> Unidades { get; set; }
        public virtual List<ContaBancaria> ContaBancarias { get; set; }
        public virtual List<PontoAtencao> PontoAtencaos { get; set; }
        public virtual List<Parentesco> Parentescos { get; set; }

    }
}

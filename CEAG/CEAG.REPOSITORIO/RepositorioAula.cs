using CEAG.DOMINIO;
using CEAG.REPOSITORIO.Genericos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.REPOSITORIO
{
    public class RepositorioAula : RepositorioGenericoEntity<Aula, int>
    {
        public RepositorioAula(DbContext contexto) : base(contexto)
        {
        }

        public override List<Aula> Selecionar(int id)
        {
            return null;
        }

        public override Aula SelecionarPorId(int id)
        {
            return _contexto.Set<Aula>()
                .Include(p => p.AulaAlunos)
                .Include("AulaAlunos.AlunoMatricula")
                .Include("AulaAlunos.AlunoMatricula.Aluno")
                .Include(p => p.Disciplina)
                .Include(p => p.Turma)
                .SingleOrDefault(p => p.CodAula == id);
        }

        public List<Aula> SelecionarPorIdTurma(int codTurma)
        {
            return _contexto.Set<Aula>()
                .Include(p => p.AulaAlunos)
                .Include(p => p.Disciplina)
                .Include(p => p.Turma)
                .Where(p => p.CodTurma == codTurma)
                .ToList();
        }

        public List<Aula> SelecionarPorIdTurma(int codTurma, int ano, int mes)
        {
            return _contexto.Set<Aula>()
                .Include(p => p.AulaAlunos)
                .Include(p => p.Disciplina)
                .Include(p => p.Turma)
                .Where(p => p.CodTurma == codTurma && p.Realizada.Year == ano && p.Realizada.Month == mes)
                .ToList();
        }

        public List<Aula> SelecionarPorData(DateTime data, int codTurma)
        {
            return _contexto.Set<Aula>()
                .Include(p => p.AulaAlunos)
                .Include(p => p.Disciplina)
                .Include(p => p.Turma)
                .Where(p => p.Realizada == data && p.CodTurma == codTurma)
                .ToList();
        }


        public List<Aula> SelecionarPorData(int codDisciplina, int codTurma)
        {
            return _contexto.Set<Aula>()
                .Include(p => p.AulaAlunos)
                .Include(p => p.Disciplina)
                .Include(p => p.Turma)
                .Where(p => p.CodDisciplina == codDisciplina && p.CodTurma == codTurma)
                .ToList();
        }
    }
}

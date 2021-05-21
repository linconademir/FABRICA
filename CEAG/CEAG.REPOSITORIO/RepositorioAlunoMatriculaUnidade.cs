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
    public class RepositorioAlunoMatriculaUnidade : RepositorioGenericoEntity<AlunoMatriculaUnidade, int>
    {
        public RepositorioAlunoMatriculaUnidade(DbContext contexto) : base(contexto)
        {
        }


        [Obsolete("Não use esse método nesse repositorio, está em branco")]
        public override List<AlunoMatriculaUnidade> Selecionar(int id)
        {
            var listaAluno = _contexto.Set<AlunoMatriculaUnidade>()
                 .Where(p => p.CodAlunoMatriculaUnidade == 0)
                 .ToList();

            return listaAluno;
        }

        public List<AlunoMatriculaUnidade> SelecionarPorTurma(int id)
        {
            var listaAluno = _contexto.Set<AlunoMatriculaUnidade>()
                 .Include(p => p.Disciplina)
                 .Include(p => p.Funcionario)
                 .Include(p => p.AlunoMatricula)
                 .Include("AlunoMatricula.Aluno")
                 .Include("AlunoMatricula.Turma")
                 .Where(p => p.AlunoMatricula.CodTurma == id)
                 .ToList();

            return listaAluno;
        }

        public List<AlunoMatriculaUnidade> SelecionarPorAlunoMatricula(int id)
        {
            var listaAluno = _contexto.Set<AlunoMatriculaUnidade>()
                 .Include(p => p.Disciplina)
                 .Include(p => p.Funcionario)
                 .Include(p => p.AlunoMatricula)
                 .Include("AlunoMatricula.Aluno")
                 .Include("AlunoMatricula.Turma")
                 .Where(p => p.AlunoMatricula.CodAlunoMatricula == id)
                 .ToList();

            return listaAluno;
        }

        public List<AlunoMatriculaUnidade> SelecionarPorDisciplina(int id)
        {
            var listaAluno = _contexto.Set<AlunoMatriculaUnidade>()
                 .Include(p => p.Disciplina)
                 .Include(p => p.Funcionario)
                 .Include(p => p.AlunoMatricula)
                 .Include("AlunoMatricula.Aluno")
                 .Include("AlunoMatricula.Turma")
                 .Where(p => p.CodDisciplina == id)
                 .ToList();

            return listaAluno;
        }

        public List<AlunoMatriculaUnidade> SelecionarPorProfessor(int codFuncionario)
        {
            var listaAluno = _contexto.Set<AlunoMatriculaUnidade>()
                 .Include(p => p.Disciplina)
                 .Include(p => p.Funcionario)
                 .Include(p => p.AlunoMatricula)
                 .Include("AlunoMatricula.Aluno")
                 .Include("AlunoMatricula.Turma")
                 .Where(p => p.CodFuncionario == codFuncionario)
                 .ToList();

            return listaAluno;
        }


        public override AlunoMatriculaUnidade SelecionarPorId(int id)
        {
            return _contexto.Set<AlunoMatriculaUnidade>()
                .Include(p => p.Disciplina)
                .Include(p => p.Funcionario)
                .Include(p => p.AlunoMatricula)
                .Include("AlunoMatricula.Aluno")
                .SingleOrDefault(p => p.CodAlunoMatriculaUnidade == id);
        }
    }
}

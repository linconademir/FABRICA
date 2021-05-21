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
    public class RepositorioTurmaFuncionarioDisciplina : RepositorioGenericoEntity<TurmaFuncionarioDisciplina, int>
    {
        public RepositorioTurmaFuncionarioDisciplina(DbContext contexto) : base(contexto)
        {
        }

        public override List<TurmaFuncionarioDisciplina> Selecionar(int id)
        {
            var listaTurmaFuncionarioDisciplina = _contexto.Set<TurmaFuncionarioDisciplina>()
                 .Include(p => p.Disciplina)
                 .Include(p => p.Funcionario)
                 .Include(p => p.Turma)
                 .Where(p => p.Turma.CodEscola == id)
                 .ToList();

            return listaTurmaFuncionarioDisciplina;
        }

        public override TurmaFuncionarioDisciplina SelecionarPorId(int id)
        {
            return _contexto.Set<TurmaFuncionarioDisciplina>()
                .Include(p => p.Disciplina)
                 .Include(p => p.Funcionario)
                 .Include(p => p.Turma)
                .SingleOrDefault(p => p.CodTurmaFuncionarioDisciplina == id);
        }

        public List<TurmaFuncionarioDisciplina> SelecionarPorTurma(int id)
        {
            var listaTurmaFuncionarioDisciplina = _contexto.Set<TurmaFuncionarioDisciplina>()
                    .Include(p => p.Disciplina)
                    .Include(p => p.Funcionario)
                    .Include(p => p.Turma)
                    .Where(p => p.CodTurma == id)
                    .ToList();

            return listaTurmaFuncionarioDisciplina;
        }

        public List<TurmaFuncionarioDisciplina> SelecionarPorProfessor(int id)
        {
            var listaTurmaFuncionarioDisciplina = _contexto.Set<TurmaFuncionarioDisciplina>()
                    .Include(p => p.Disciplina)
                    .Include(p => p.Funcionario)
                    .Include(p => p.Turma)
                    .Where(p => p.CodFuncionario == id)
                    .ToList();

            return listaTurmaFuncionarioDisciplina;
        }

        public List<TurmaFuncionarioDisciplina> SelecionarPorDisciplina(int id)
        {
            var listaTurmaFuncionarioDisciplina = _contexto.Set<TurmaFuncionarioDisciplina>()
                    .Include(p => p.Disciplina)
                    .Include(p => p.Funcionario)
                    .Include(p => p.Turma)
                    .Where(p => p.CodDisciplina == id)
                    .ToList();

            return listaTurmaFuncionarioDisciplina;
        }
    }
}

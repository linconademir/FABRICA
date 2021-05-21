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
    public class RepositorioFuncionarioDisciplina : RepositorioGenericoEntity<FuncionarioDisciplina, int>
    {
        public RepositorioFuncionarioDisciplina(DbContext contexto) : base(contexto)
        {
        }

        public override List<FuncionarioDisciplina> Selecionar(int id)
        {
            var listaFuncionarioDisciplina = _contexto.Set<FuncionarioDisciplina>()
                 .Include(p => p.Funcionario)
                 .Include(p => p.Disciplina)
                 .Where(p => p.Disciplina.CodEscola == id && p.Funcionario.Cancelamento == null && p.Cancelamento == null)
                 .ToList();

            return listaFuncionarioDisciplina;
        }

        public override FuncionarioDisciplina SelecionarPorId(int id)
        {
            return _contexto.Set<FuncionarioDisciplina>()
                .Include(p => p.Funcionario)
                .Include(p => p.Disciplina)
                .SingleOrDefault(p => p.CodFuncionarioDisciplina == id);
        }

        public List<FuncionarioDisciplina> SelecionarDisciplinasPorProfessor(int codFuncionario)
        {
            var retorno = _contexto.Set<FuncionarioDisciplina>()
                 .Include(p => p.Funcionario)
                 .Include(p => p.Disciplina)
                 .Where(p => p.CodFuncionario == codFuncionario)
                 .ToList();

            return retorno;
        }

        public List<FuncionarioDisciplina> SelecionarProfessorPorDisciplina(int codDisciplina)
        {
            var retorno = _contexto.Set<FuncionarioDisciplina>()
                 .Include(p => p.Funcionario)
                 .Include(p => p.Disciplina)
                 .Where(p => p.CodDisciplina == codDisciplina && p.Funcionario.Cancelamento == null)
                 .ToList();

            return retorno;
        }
    }

}

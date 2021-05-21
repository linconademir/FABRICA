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
    public class RepositorioAlunoQuestionario : RepositorioGenericoEntity<AlunoQuestionario, int>
    {
        public RepositorioAlunoQuestionario(DbContext contexto) : base(contexto)
        {
        }

        public override List<AlunoQuestionario> Selecionar(int id)
        {
            var listaAluno = _contexto.Set<AlunoQuestionario>()
                 .Include(p => p.Aluno)
                 .Include(p => p.Questionario)
                 .Where(p => p.Aluno.CodEscola == id)
                 .ToList();

            return listaAluno;
        }
        public override AlunoQuestionario SelecionarPorId(int id)
        {
            return _contexto.Set<AlunoQuestionario>()
                .Include(p => p.Aluno)
                .Include(p => p.Questionario)
                .SingleOrDefault(p => p.CodAlunoQuestionario == id);
        }

        public List<AlunoQuestionario> SelecionarPorIdAluno(int id)
        {
            var listaAluno = _contexto.Set<AlunoQuestionario>()
                 .Include(p => p.Aluno)
                 .Include(p => p.Questionario)
                 .Where(p => p.Aluno.CodAluno == id)
                 .ToList();

            return listaAluno;
        }

        public List<Questionario> SelecionarQuestionarioEmBranco(int idEscola)
        {
            var listaAluno = _contexto.Set<Questionario>()
                 .Where(p => p.Tipo.ToUpper().Equals("SAUDE") && p.CodEscola == idEscola)
                 .ToList();

            return listaAluno;
        }

    }
}

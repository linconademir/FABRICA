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
    public class RepositorioAdvertencia : RepositorioGenericoEntity<Advertencia, int>
    {
        public RepositorioAdvertencia(DbContext contexto) : base(contexto)
        {
        }

        
        public override List<Advertencia> Selecionar(int id)
        {
            return _contexto.Set<Advertencia>()
                .Include(p => p.AlunoMatricula)
                .Include("AlunoMatricula.Aluno")
                .Include(p => p.Aula)
                .Where(p => p.AlunoMatricula.Turma.CodEscola == id).ToList();
        }

        public override Advertencia SelecionarPorId(int id)
        {
            return _contexto.Set<Advertencia>()
                .Include(p => p.AlunoMatricula)
                .Include("AlunoMatricula.Aluno")
                .Include(p => p.Aula)
                .SingleOrDefault(p => p.CodAdvertencia == id);
        }

        public List<Advertencia> SelecionarPorMatricula(int id)
        {
            return _contexto.Set<Advertencia>()
                .Include(p => p.AlunoMatricula)
                .Include("AlunoMatricula.Aluno")
                .Include(p => p.Aula)
                .Where(p => p.CodAlunoMatricula == id).ToList();
        }

        public List<Advertencia> SelecionarPorAluno(int id)
        {
            return _contexto.Set<Advertencia>()
                .Include(p => p.AlunoMatricula)
                .Include("AlunoMatricula.Aluno")
                .Include(p => p.Aula)
                .Where(p => p.AlunoMatricula.CodAluno == id).ToList();
        }

    }
}

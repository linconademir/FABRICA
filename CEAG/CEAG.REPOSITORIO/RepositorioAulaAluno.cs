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
    public class RepositorioAulaAluno : RepositorioGenericoEntity<AulaAluno, int>
    {
        public RepositorioAulaAluno(DbContext contexto) : base(contexto)
        {
        }

        public override List<AulaAluno> Selecionar(int id)
        {
            return null;
        }

        public override AulaAluno SelecionarPorId(int id)
        {
            return _contexto.Set<AulaAluno>()
                .Include(p => p.Aula)
                .Include(p => p.AlunoMatricula)
                .SingleOrDefault(p => p.CodAulaAluno == id);
        }

        
        public List<AulaAluno> SelecionarPorIdAlunoMatricula(int id)
        {
            return _contexto.Set<AulaAluno>()
                .Include(p => p.Aula)
                .Include(p => p.AlunoMatricula)
                .Where(p => p.AlunoMatricula.CodAlunoMatricula == id)
                .ToList();
        }
    }
}

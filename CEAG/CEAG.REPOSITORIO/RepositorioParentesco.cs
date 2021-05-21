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
    public class RepositorioParentesco : RepositorioGenericoEntity<Parentesco, int>
    {
        public RepositorioParentesco(DbContext contexto) : base(contexto)
        {
        }

        
        public override List<Parentesco> Selecionar(int id)
        {
            return _contexto.Set<Parentesco>()
                .Include(p => p.ParentescoAlunos)
                .Include("ParentescoAluno.Aluno")
                .Where(p => p.CodEscola == id).ToList();
        }

        public override Parentesco SelecionarPorId(int id)
        {
            return _contexto.Set<Parentesco>()
                .Include(p => p.ParentescoAlunos)
                .Include("ParentescoAlunos.Aluno")
                .SingleOrDefault(p => p.CodParentesco == id);
        }

    }
}

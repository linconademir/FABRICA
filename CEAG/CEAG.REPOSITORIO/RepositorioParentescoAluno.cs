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
    public class RepositorioParentescoAluno : RepositorioGenericoEntity<ParentescoAluno, int>
    {
        public RepositorioParentescoAluno(DbContext contexto) : base(contexto)
        {
        }


        public override ParentescoAluno SelecionarPorId(int id)
        {
            return _contexto.Set<ParentescoAluno>()
                .Include(p => p.Parentesco)
                .Include(p => p.Aluno)
                .SingleOrDefault(p => p.CodParentescoAluno == id);
        }

        public List<ParentescoAluno> SelecionarParentesPorAluno(int id)
        {
            int codParentesco = 0;
            List<ParentescoAluno> parentescoAlunos = new List<ParentescoAluno>();
            List<ParentescoAluno> parentescoAlunosRetorno = new List<ParentescoAluno>();

           parentescoAlunos = _contexto.Set<ParentescoAluno>().Where(p => p.CodAluno == id).ToList();
           
            if (parentescoAlunos.Any())
            {
                foreach (var item in parentescoAlunos.Select(p => p.CodParentesco).Distinct())
                {
                    List<ParentescoAluno> listaParentescos = _contexto.Set<ParentescoAluno>()
                       .Include(p => p.Parentesco)
                       .Include(p => p.Aluno)
                       .Where(p => p.CodParentesco == item).ToList();

                    parentescoAlunosRetorno.AddRange(listaParentescos);

                }
            }

            //parentescoAlunosRetorno.Remove(parentescoAlunosRetorno.First(p => p.CodAluno == id));

            return parentescoAlunosRetorno;
        }
    }
}

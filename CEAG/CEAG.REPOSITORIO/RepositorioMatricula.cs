using CEAG.ACESSODADOS.Context;
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
    public class RepositorioMatricula : RepositorioGenericoEntity<Matricula, int>
    {
        public RepositorioMatricula(CeagDbContext contexto) : base(contexto)
        {
        }

        public override List<Matricula> Selecionar(int idEscola)
        {
            return _contexto.Set<Matricula>()
                .Where(p => p.CodEscola == idEscola)
                .ToList();
        }

        public Matricula SelecionarUltimaMatricula(int idEscola)
        {
            List<Matricula> matriculas  = _contexto.Set<Matricula>()
                                            .Where(p => p.CodEscola == idEscola)
                                            .OrderByDescending(p => p.Ordem)
                                            .ToList();
            return matriculas.Any() ? matriculas[0] : null;
        }

        public override Matricula SelecionarPorId(int id)
        {
            return _contexto.Set<Matricula>()
                .SingleOrDefault(p => p.CodMatricula == id);
        }
    }
}

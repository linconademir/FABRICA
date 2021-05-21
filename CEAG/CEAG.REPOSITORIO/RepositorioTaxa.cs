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
    public class RepositorioTaxa : RepositorioGenericoEntity<Taxa, int>
    {
        public RepositorioTaxa(CeagDbContext contexto) : base(contexto)
        {

        }

        public override List<Taxa> Selecionar(int idEscola)
        {
            return _contexto.Set<Taxa>()
                .Where(p => p.CodEscola == idEscola && p.Cancelamento == null)
                .ToList();
        }

        public override Taxa SelecionarPorId(int id)
        {
            return _contexto.Set<Taxa>()
                .SingleOrDefault(p => p.CodTaxa == id && p.Cancelamento == null);
        }
    }
}

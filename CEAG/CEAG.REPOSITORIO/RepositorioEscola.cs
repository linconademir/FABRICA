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
    public class RepositorioEscola : RepositorioGenericoEntity<Escola, int>
    {
        public RepositorioEscola(DbContext contexto) : base(contexto)
        {
        }

        public override Escola SelecionarPorId(int id)
        {
            return _contexto.Set<Escola>()
                .Include(p => p.Logradouro)
                .SingleOrDefault(p => p.CodEscola == id);
        }
    }
}

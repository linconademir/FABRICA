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
    public class RepositorioLogradouro : RepositorioGenericoEntity<Logradouro, int>
    {
        public RepositorioLogradouro(CeagDbContext contexto) : base(contexto)
        {
        }
       
        public override Logradouro SelecionarPorId(int id)
        {
            return _contexto.Set<Logradouro>()
                .SingleOrDefault(p => p.CodLogradouro == id);
        }
    }
}

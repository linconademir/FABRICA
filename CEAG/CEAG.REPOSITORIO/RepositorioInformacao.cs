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
    public class RepositorioInformacao : RepositorioGenericoEntity<Informacao, int>
    {
        public RepositorioInformacao(CeagDbContext contexto) : base(contexto)
        {

        }

        public override List<Informacao> Selecionar(int id)
        {
            return _contexto.Set<Informacao>()
                .Where(p => p.CodAluno == id)
                .ToList();
        }

        public override Informacao SelecionarPorId(int id)
        {
            return _contexto.Set<Informacao>()
                .SingleOrDefault(p => p.CodInformacao == id);
        }
    }
}

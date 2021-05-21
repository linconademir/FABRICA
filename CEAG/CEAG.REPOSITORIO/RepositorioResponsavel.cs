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
    public class RepositorioResponsavel : RepositorioGenericoEntity<Responsavel, int>
    {
        public RepositorioResponsavel(CeagDbContext contexto) : base(contexto)
        {

        }

        public override List<Responsavel> Selecionar(int id)
        {
            return _contexto.Set<Responsavel>()
                .Include(p => p.Logradouro)
                .Include(p => p.Telefones)
                .Where(p => p.CodAluno == id)
                .ToList();
        }

        public override Responsavel SelecionarPorId(int id)
        {
            return _contexto.Set<Responsavel>()
                .Include(p => p.Logradouro)
                .Include(p => p.Telefones)
                .SingleOrDefault(p => p.CodResponsavel == id);
        }
    }
}

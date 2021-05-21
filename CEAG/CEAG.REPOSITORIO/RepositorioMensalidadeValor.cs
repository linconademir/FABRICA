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
    public class RepositorioMensalidadeValor : RepositorioGenericoEntity<MensalidadeValor, int>
    {
        public RepositorioMensalidadeValor(CeagDbContext contexto) : base(contexto)
        {
        }

        public override List<MensalidadeValor> Selecionar(int idEscola)
        {
            return _contexto.Set<MensalidadeValor>()
                .Where(p => p.CodEscola == idEscola)
                .ToList();
        }

        public override MensalidadeValor SelecionarPorId(int id)
        {
            return _contexto.Set<MensalidadeValor>()
                .SingleOrDefault(p => p.CodMensalidadeValor == id);
        }
    }
}

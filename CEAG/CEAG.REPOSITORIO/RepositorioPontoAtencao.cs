using CEAG.DOMINIO;
using CEAG.DOMINIO.Procedure;
using CEAG.REPOSITORIO.Genericos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.REPOSITORIO
{
    public class RepositorioPontoAtencao : RepositorioGenericoEntity<PontoAtencao, int>
    {
        public RepositorioPontoAtencao(DbContext contexto) : base(contexto)
        {
        }

        public override List<PontoAtencao> Selecionar(int idEscola)
        {
            var listaMensalidade = _contexto.Set<PontoAtencao>()
                .Where(p => p.CodEscola == idEscola && !p.Resolucao.HasValue)
                .ToList();

            return listaMensalidade;
        }

        public override PontoAtencao SelecionarPorId(int id)
        {
            return _contexto.Set<PontoAtencao>()
                .SingleOrDefault(p => p.CodPontoAtencao == id);
        }

    }
}

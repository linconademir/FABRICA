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
    public class RepositorioDebitoHistorico : RepositorioGenericoEntity<DebitoHistorico, int>
    {
        public RepositorioDebitoHistorico(DbContext contexto) : base(contexto)
        {
        }

        public override List<DebitoHistorico> Selecionar(int idEscola)
        {
            var listaMensalidade = _contexto.Set<DebitoHistorico>()
                .Include(p => p.Debito)
                .Where(p => p.Debito.AlunoMatricula.Aluno.CodEscola == idEscola)
                .ToList();

            return listaMensalidade;
        }

        public override DebitoHistorico SelecionarPorId(int id)
        {
            return _contexto.Set<DebitoHistorico>()
                .Include(p => p.Debito)
                .SingleOrDefault(p => p.CodDebitoHistorico == id);
        }

    }
}

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
    public class RepositorioFeriado : RepositorioGenericoEntity<Feriado, int>
    {
        public RepositorioFeriado(DbContext contexto) : base(contexto)
        {
        }

        public override List<Feriado> Selecionar(int idEscola)
        {
            return _contexto.Set<Feriado>()
                .Where(p => p.CodEscola == idEscola)
                .ToList();
        }

        public override Feriado SelecionarPorId(int id)
        {
            return _contexto.Set<Feriado>()
                .SingleOrDefault(p => p.CodFeriado == id);
        }

        public List<Feriado> SelecionarPorAno(int idEscola, int ano)
        {
            return _contexto.Set<Feriado>()
                .Where(p => p.CodEscola == idEscola && p.Data.Year == ano)
                .ToList();
        }

        public List<Feriado> SelecionarPorData(int idEscola, DateTime data)
        {
            return _contexto.Set<Feriado>()
                .Where(p => p.CodEscola == idEscola && p.Data == data)
                .ToList();
        }

    }
}

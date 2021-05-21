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
    public class RepositorioHorario : RepositorioGenericoEntity<Horario, int>
    {
        public RepositorioHorario(DbContext contexto) : base(contexto)
        {
        }

        public override List<Horario> Selecionar(int id)
        {
            var lista = _contexto.Set<Horario>()
                .Include(p => p.Escola)
                .Include(p => p.HorarioAulas)
                 .Where(p => p.CodEscola == id)
                 .ToList();

            return lista;
        }

        public override Horario SelecionarPorId(int id)
        {
            return _contexto.Set<Horario>()
                .Include(p => p.Escola)
                .Include(p => p.HorarioAulas)
                .SingleOrDefault(p => p.CodHorario == id);
        }
    }
}

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
    public class RepositorioHorarioAula : RepositorioGenericoEntity<HorarioAula, int>
    {
        public RepositorioHorarioAula(DbContext contexto) : base(contexto)
        {
        }

        public override List<HorarioAula> Selecionar(int id)
        {
            var lista = _contexto.Set<HorarioAula>()
                .Include(p => p.Horario)
                 .Where(p => p.Horario.CodEscola == id)
                 .ToList();

            return lista;
        }

        public List<HorarioAula> SelecionarPorIdHorario(int id)
        {
            return _contexto.Set<HorarioAula>()
                .Include(p => p.Horario)
                .Where(p => p.CodHorario == id)
                .ToList();
        }

        public override HorarioAula SelecionarPorId(int id)
        {
            return _contexto.Set<HorarioAula>()
                .Include(p => p.Horario)
                .SingleOrDefault(p => p.CodHorarioAula == id);
        }
    }
}

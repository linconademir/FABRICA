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
    public class RepositorioContaBancaria : RepositorioGenericoEntity<ContaBancaria, int>
    {
        public RepositorioContaBancaria(DbContext contexto) : base(contexto)
        {
        }

        public override List<ContaBancaria> Selecionar(int idEscola)
        {
            var lista = _contexto.Set<ContaBancaria>()
                .Include(p => p.Escola)
                .Where(p => p.CodEscola == idEscola)
                .ToList();

            return lista;
        }

        public override ContaBancaria SelecionarPorId(int id)
        {
            return _contexto.Set<ContaBancaria>()
                .Include(p => p.Escola)
                .Include("Mensalidades.AlunoMatricula")
                .Include("Mensalidades.AlunoMatricula.Aluno")
                .SingleOrDefault(p => p.CodContaBancaria == id);
        }
    }
}

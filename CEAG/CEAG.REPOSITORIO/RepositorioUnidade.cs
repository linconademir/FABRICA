using CEAG.ACESSODADOS.Context;
using CEAG.DOMINIO;
using CEAG.REPOSITORIO.Genericos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CEAG.REPOSITORIO
{
    public class RepositorioUnidade : RepositorioGenericoEntity<Unidade, int>
    {
        public RepositorioUnidade(DbContext contexto) : base(contexto)
        {
        }


        public override List<Unidade> Selecionar(int id)
        {
            var listaAluno = _contexto.Set<Unidade>()
                 .Include(p => p.Escola)
                 .Where(p => p.CodEscola == id)
                 .ToList();

            return listaAluno;
        }

        public override Unidade SelecionarPorId(int id)
        {
            return _contexto.Set<Unidade>()
                .Include(p => p.Escola)
                .SingleOrDefault(p => p.CodUnidade == id);
        }

        public List<Unidade> SelecionarPorAnoEscola(int ano, int codEscola)
        {
            return _contexto.Set<Unidade>()
                .Include(p => p.Escola)
                .Where(p => p.CodEscola == codEscola && p.Ano == ano)
                .ToList();
        }
    }
}

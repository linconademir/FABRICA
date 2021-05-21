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
    public class RepositorioDebito : RepositorioGenericoEntity<Debito, int>
    {
        public RepositorioDebito(DbContext contexto) : base(contexto)
        {
        }

        public override List<Debito> Selecionar(int idEscola)
        {
            var listaMensalidade = _contexto.Set<Debito>()
                .Include(p => p.AlunoMatricula)
                .Include(p => p.DebitoHistoricos)
                .Include("AlunoMatricula.Aluno")
                .Where(p => p.AlunoMatricula.Aluno.CodEscola == idEscola)
                .ToList();

            return listaMensalidade;
        }

        public List<Debito> SelecionarDebitosAno(int idEscola, int ano)
        {
            var lista = _contexto.Set<Debito>()
                .Include(p => p.AlunoMatricula)
                .Include(p => p.DebitoHistoricos)
                .Include("AlunoMatricula.Aluno")
                .Where(p => p.AlunoMatricula.Aluno.CodEscola == idEscola
                && p.Periodo.Year == ano)
                .ToList();

            return lista;
        }

        public List<Debito> SelecionarDebitosAberto(int idEscola)
        {
            var listaDebito = _contexto.Set<Debito>()
                .Include(p => p.AlunoMatricula)
                .Include(p => p.DebitoHistoricos)
                .Include("AlunoMatricula.Aluno")
                .Include("AlunoMatricula.Aluno.Responsavels")
                .Where(p => p.AlunoMatricula.Aluno.CodEscola == idEscola
                && (!p.Pagamento.HasValue)).ToList();

            return listaDebito;
        }

        public int SelecionarQuantidadeRegistros(int idEscola)
        {
            var quantidadePagos = _contexto.Set<Debito>()
                .Count(p => p.AlunoMatricula.Aluno.CodEscola == idEscola && (p.Pagamento.HasValue && p.Pagamento.Value.Year == DateTime.Now.Year));
                

            return quantidadePagos + 1;
        }

        public override Debito SelecionarPorId(int id)
        {
            return _contexto.Set<Debito>()
                .Include(p => p.AlunoMatricula)
                .Include(p => p.DebitoHistoricos)
                 .Include("AlunoMatricula.Aluno")
                .SingleOrDefault(p => p.CodDebito == id);
        }

        public List<Debito> SelecionarPorAlunoMatricula(int id)
        {
            return _contexto.Set<Debito>()
                .Include(p => p.AlunoMatricula)
                .Include(p => p.DebitoHistoricos)
                .Include("AlunoMatricula.Aluno")
                .Include("AlunoMatricula.Aluno.Responsavels")
                .Where(p => p.CodAlunoMatricula == id).ToList();
        }

        public List<Debito> SelecionarPorAluno(int id)
        {
            return _contexto.Set<Debito>()
                .Include(p => p.AlunoMatricula)
             //   .Include(p => p.DebitoHistoricos)
                .Include("AlunoMatricula.Aluno")
                .Include("AlunoMatricula.Aluno.Responsavels")
                .Where(p => p.AlunoMatricula.CodAluno == id).ToList();
        }

        public List<Debito> SelecionarPorCodigoHexa(int idEscola, string codigoHexa)
        {
            return _contexto.Set<Debito>()
                .Include(p => p.AlunoMatricula)
                .Include(p => p.DebitoHistoricos)
                .Include("AlunoMatricula.Aluno")
                .Where(p => p.AlunoMatricula.Aluno.CodEscola == idEscola && p.IdenficadorHexa.Equals(codigoHexa)).ToList();
        }

        public List<AlunoReciboMensal> EmitirReciboMensal(int codDebito)
        {
            string sql = "STP_EMITIR_RECIBO @COD_DEBITO";
            var parameter = new List<object>
            {
                new SqlParameter
                {
                    ParameterName = "@COD_DEBITO",
                    SqlDbType = SqlDbType.Int,
                    Value = codDebito
                }
            };

            var retornoProcedure = _contexto.Database.SqlQuery<AlunoReciboMensal>(sql, parameter.ToArray());
            return retornoProcedure.ToList();
        }
    }
}

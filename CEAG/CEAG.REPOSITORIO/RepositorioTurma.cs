using CEAG.ACESSODADOS.Context;
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
    public class RepositorioTurma : RepositorioGenericoEntity<Turma, int>
    {
        public RepositorioTurma(CeagDbContext contexto) : base(contexto)
        {
        }

        public override List<Turma> Selecionar(int id)
        {
            var listaAluno = _contexto.Set<Turma>()
                 .Include(p => p.AlunoMatriculas)
                 .Include(p => p.Escola)
                 .Where(p => p.CodEscola == id)
                 .ToList();

            return listaAluno;
        }

        public override Turma SelecionarPorId(int id)
        {
            return _contexto.Set<Turma>()
                .Include(p => p.AlunoMatriculas)
                .Include("AlunoMatriculas.Aluno")
                .Include("TurmaFuncionarioDisciplinas.Disciplina")
                .Include("TurmaFuncionarioDisciplinas.Funcionario")
                .Include(p => p.Escola)
                .SingleOrDefault(p => p.CodTurma == id);
        }

        public void DuplicarTurma(int codTurma, int novoAnoLetivo, int codAcesso)
        {
            string sql = "STP_DUPLICAR_TURMAS_PERIODO_LETIVO @COD_TURMA_ANTERIOR, @ANO_LETIVO, @COD_ACESSO";
            var parameter = new List<object>
            {
                new SqlParameter
                {
                    ParameterName = "@COD_TURMA_ANTERIOR",
                    SqlDbType = SqlDbType.Int,
                    Value = codTurma
                },
                new SqlParameter
                {
                    ParameterName = "@ANO_LETIVO",
                    SqlDbType = SqlDbType.Int,
                    Value = novoAnoLetivo
                },
                new SqlParameter
                {
                    ParameterName = "@COD_ACESSO",
                    SqlDbType = SqlDbType.Int,
                    Value = codAcesso
                }
            };
            

            _contexto.Database.ExecuteSqlCommand(sql, parameter.ToArray());
        }

        public List<AlunosTurma> EmitirRelatorioAlunos(int codTurma)
        {
            string sql = "STP_LISTAR_ALUNOS_TURMA @COD_IN_TURMA";
            var parameter = new List<object>
            {
                new SqlParameter
                {
                    ParameterName = "@COD_IN_TURMA",
                    SqlDbType = SqlDbType.Int,
                    Value = codTurma
                }
            };

            var retornoProcedure = _contexto.Database.SqlQuery<AlunosTurma>(sql, parameter.ToArray());
            return retornoProcedure.ToList();
        }
    }
}

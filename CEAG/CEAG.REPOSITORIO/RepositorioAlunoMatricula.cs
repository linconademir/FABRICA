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
    public class RepositorioAlunoMatricula : RepositorioGenericoEntity<AlunoMatricula, int>
    {
        public RepositorioAlunoMatricula(CeagDbContext contexto) : base(contexto)
        {
        }

        public override List<AlunoMatricula> Selecionar(int id)
        {
           var listaAluno = _contexto.Set<AlunoMatricula>()
                .Include(p => p.Aluno)
                .Include(p => p.Turma)
                .Where(p => p.Turma.CodEscola == id)
                .ToList();

            return listaAluno;
        }

        public List<AlunoMatricula> SelecionarPorIdTurma(int id)
        {
            var listaAluno = _contexto.Set<AlunoMatricula>()
                 .Include(p => p.Aluno)
                 .Include(p => p.Turma)
                 .Where(p => p.CodTurma == id)
                 .ToList();

            return listaAluno;
        }

        public override AlunoMatricula SelecionarPorId(int id)
        {
            return _contexto.Set<AlunoMatricula>()
                .Include(p => p.Aluno)
                .Include(p => p.Turma)
                .SingleOrDefault(p => p.CodAlunoMatricula == id);
        }

        public List<AlunoMatricula> SelecionarPorIdAluno(int id)
        {
            var lista = _contexto.Set<AlunoMatricula>()
                .Include(p => p.Aluno)
                .Include(p => p.Turma)
                .Where(p => p.CodAluno == id).ToList();

            return lista;
        }

        public List<AlunoContrato> EmitirContratoAluno(int codAlunoMatricula)
        {
            string sql = "STP_EMITIR_CONTRATO_ALUNO_MATRICULA @COD_ALUNO_MATRICULA";
            var parameter = new List<object>
            {
                new SqlParameter
                {
                    ParameterName = "@COD_ALUNO_MATRICULA",
                    SqlDbType = SqlDbType.Int,
                    Value = codAlunoMatricula
                }
            };

            var retornoProcedure = _contexto.Database.SqlQuery<AlunoContrato>(sql, parameter.ToArray());
            return retornoProcedure.ToList();
        }
    }
}

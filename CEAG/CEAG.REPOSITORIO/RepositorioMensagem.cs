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
    public class RepositorioMensagem : RepositorioGenericoEntity<Mensagem, int>
    {
        public RepositorioMensagem(DbContext contexto) : base(contexto)
        {
        }

        public override List<Mensagem> Selecionar(int id)
        {
            return _contexto.Set<Mensagem>()
                 .Include(p => p.AlunoMatricula)
                 .Include("AlunoMatricula.Aluno")
                 .Include("AlunoMatricula.Turma")
                 .Where(p => p.AlunoMatricula.Turma.CodEscola == id).ToList();
        }

        public override Mensagem SelecionarPorId(int id)
        {
            return _contexto.Set<Mensagem>()
                .Include(p => p.AlunoMatricula)
                .Include("AlunoMatricula.Aluno")
                .Include("AlunoMatricula.Turma")
                .SingleOrDefault(p => p.CodMensagem == id);
        }

        public List<Mensagem> SelecionarPorAluno(int id)
        {
            return _contexto.Set<Mensagem>()
                .Include(p => p.AlunoMatricula)
                .Include("AlunoMatricula.Aluno")
                .Include("AlunoMatricula.Turma")
                .Where(p => p.AlunoMatricula.CodAluno == id).ToList();
        }
    }
}

using CEAG.ACESSODADOS.Context;
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
    public class RepositorioAluno : RepositorioGenericoEntity<Aluno, int>
    {
        public RepositorioAluno(CeagDbContext contexto) : base(contexto)
        {
        }

        public override List<Aluno> Selecionar(int id)
        {
            var listaAluno = _contexto.Set<Aluno>()
                 .Include(p => p.Telefones)
                 .Include(p => p.Logradouro)
                 .Include(p => p.Responsavels)
                 .Include(p => p.AlunoMatriculas)
                 .Include("AlunoMatriculas.Turma")
                 .Where(p => p.CodEscola == id)
                 .ToList();

            return listaAluno;
        }

        public override Aluno SelecionarPorId(int id)
        {
            return _contexto.Set<Aluno>()
                .Include(p => p.Telefones)
                .Include(p => p.Logradouro)
                .Include(p => p.Responsavels)
                .Include(p => p.AlunoMatriculas)
                .Include("AlunoMatriculas.Turma")
                .SingleOrDefault(p => p.CodAluno == id);
        }

        public Aluno SelecionarPorCpf(string cpf)
        {
            List<Aluno> alunos = new List<Aluno>();

            alunos = _contexto.Set<Aluno>()
                .Include(p => p.Telefones)
                .Include(p => p.Logradouro)
                .Include(p => p.Responsavels)
                .Where(p => p.Cpf == cpf).ToList();

            if (alunos.Any())
            {
                return alunos[0];
            }
            return new Aluno();
        }
    }
}

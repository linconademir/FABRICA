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
    public class RepositorioFuncionario : RepositorioGenericoEntity<Funcionario, int>
    {
        public RepositorioFuncionario(DbContext contexto) : base(contexto)
        {
        }

        public override List<Funcionario> Selecionar(int id)
        {
            var listaAluno = _contexto.Set<Funcionario>()
                 .Include(p => p.Telefones)
                 .Include(p => p.Logradouro)
                 .Where(p => p.CodEscola == id && p.Cancelamento == null)
                 .ToList();

            return listaAluno;
        }

        public override Funcionario SelecionarPorId(int id)
        {
            return _contexto.Set<Funcionario>()
                .Include(p => p.Telefones)
                .Include(p => p.Logradouro)
                .SingleOrDefault(p => p.CodFuncionario == id && p.Cancelamento == null);
        }

        public Funcionario SelecionarPorCpf(string cpf)
        {
            return _contexto.Set<Funcionario>()
                .Include(p => p.Telefones)
                .Include(p => p.Logradouro)
                .SingleOrDefault(p => p.Cpf.Equals(cpf) && p.Cancelamento == null);
        }
    }
}

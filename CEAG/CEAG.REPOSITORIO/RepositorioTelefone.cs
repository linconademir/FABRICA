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
    public class RepositorioTelefone : RepositorioGenericoEntity<Telefone, int>
    {
        public RepositorioTelefone(CeagDbContext contexto) : base(contexto)
        {
        }
       
        public override Telefone SelecionarPorId(int id)
        {
            return _contexto.Set<Telefone>()
                .SingleOrDefault(p => p.CodTelefone == id);
        }

        public void InserirTelefoneAluno(Telefone telefone)
        {
            telefone.CodResponsavel = null;
            telefone.CodFuncionario = null;
            _contexto.Set<Telefone>().Add(telefone);
            _contexto.SaveChanges();
        }

        public void InserirTelefoneResponsavel(Telefone telefone)
        {
            telefone.CodAluno = null;
            telefone.CodFuncionario = null;
            _contexto.Set<Telefone>().Add(telefone);
            _contexto.SaveChanges();
        }
        public void InserirTelefoneFuncionario(Telefone telefone)
        {
            telefone.CodAluno = null;
            telefone.CodResponsavel = null;
            _contexto.Set<Telefone>().Add(telefone);
            _contexto.SaveChanges();
        }

        public List<Telefone> SelecionarTelefoneAluno(int id)
        {
            return _contexto.Set<Telefone>()
                .Where(p => p.CodAluno == id)
                .ToList();
        }
        public List<Telefone> SelecionarTelefoneResponsavel(int id)
        {
            return _contexto.Set<Telefone>()
                .Where(p => p.CodResponsavel == id)
                .ToList();
        }
        public List<Telefone> SelecionarTelefoneFuncionario(int id)
        {
            return _contexto.Set<Telefone>()
                .Where(p => p.CodFuncionario == id)
                .ToList();
        }
        public override List<Telefone> Selecionar(int id)
        {
            return _contexto.Set<Telefone>()
                .Where(p => p.CodAluno == id)
                .ToList();
        }
    }
}

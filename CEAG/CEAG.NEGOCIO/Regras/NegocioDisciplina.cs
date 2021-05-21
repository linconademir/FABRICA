using CEAG.ACESSODADOS.Context;
using CEAG.DOMINIO;
using CEAG.REPOSITORIO.Classes;
using CEAG.REPOSITORIO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.NEGOCIO.Regras
{
    public sealed class NegocioDisciplina
    {
        private static IDisciplinaRepositorio _disciplinaRepositorio = null;
        private static readonly object SyncObj = new object();
        //int _codEscola;
        public NegocioDisciplina()
        {
            //Cria uma instancia
            ObterInstancia();
        }

        private static void ObterInstancia()
        {
            lock (SyncObj)
            {
                if (_disciplinaRepositorio != null)
                {
                    _disciplinaRepositorio.Dispose();
                }

                _disciplinaRepositorio = new DisciplinaRepositorio(new CeagDbContext());
            }
        }

        public Disciplina ObterDisciplina(int codDisciplina)
        {
            return _disciplinaRepositorio
                .Selecionar(codDisciplina);
        }

        public List<Disciplina> ObterDisciplina(Acesso acesso, string searchString)
        {
            return _disciplinaRepositorio
                .Selecionar(
                    e => e.CodEscola == acesso.CodEscola
                    &&
                    (string.IsNullOrEmpty(searchString)
                        || (e.Descricao.ToUpper().Contains(searchString.ToUpper())
                            || e.Tipo.ToUpper().Contains(searchString.ToUpper())
                         )
                    )).ToList();

        }

        public void Inserir(Disciplina disciplina, Acesso acesso)
        {
            disciplina.Inclusao = DateTime.Now;
            disciplina.CodAcesso = acesso.CodAcesso;
            disciplina.CodEscola = acesso.CodEscola.Value;

            _disciplinaRepositorio.Inserir(disciplina);
            // _empregadoRepositorio.Commit();
        }
        public void Alterar(Disciplina disciplina, Acesso acesso)
        {
            disciplina.CodAcesso = acesso.CodAcesso;
            _disciplinaRepositorio.Alterar(disciplina);
        }
        public void Excluir(Disciplina disciplina)
        {
            _disciplinaRepositorio.Excluir(c => c == disciplina);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.REPOSITORIO.Genericos
{
    public interface IRepositorioGenericoEf<TEntidade, TChave> where TEntidade : class
    {
        void Dispose();
        //IQueryable<TEntidade> SelecionarTodos();
        IQueryable<TEntidade> Selecionar(Expression<Func<TEntidade, bool>> predicate);
        TEntidade Selecionar(params object[] key);
        TEntidade First(Expression<Func<TEntidade, bool>> predicate);
        void Alterar(TEntidade entidade);
        void Excluir(Func<TEntidade, bool> predicate);
        void Inserir(TEntidade entidade);
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.REPOSITORIO.Genericos
{
    public class RepositorioGenericoEf<TEntidade, TChave> : IRepositorioGenericoEf<TEntidade, TChave>, IDisposable
        where TEntidade : class
    {
        private DbContext _contexto;

        protected RepositorioGenericoEf(DbContext contexto)
        {
            _contexto = contexto;
        }
        //public IQueryable<TEntidade> SelecionarTodos()
        //{
        //    return _contexto.Set<TEntidade>();
        //}
        public virtual IQueryable<TEntidade> Selecionar(Expression<Func<TEntidade, bool>> predicate)
        {
            return _contexto.Set<TEntidade>().Where(predicate);
        }

        public void Inserir(TEntidade entidade)
        {
            _contexto.Set<TEntidade>().Add(entidade);
            _contexto.SaveChanges();
        }

        public void Alterar(TEntidade entidade)
        {
            _contexto.Set<TEntidade>().Attach(entidade);
            _contexto.Entry(entidade).State = EntityState.Modified;
            _contexto.SaveChanges();
        }

        //public void Excluir(TEntidade entidade)
        //{
        //    _contexto.Set<TEntidade>().Attach(entidade);
        //    _contexto.Entry(entidade).State = EntityState.Deleted;
        //    _contexto.SaveChanges();
        //}

        public void Excluir(Func<TEntidade, bool> predicate)
        {
            _contexto.Set<TEntidade>()
               .Where(predicate).ToList()
               .ForEach(del => _contexto.Set<TEntidade>().Remove(del));

            _contexto.SaveChanges();
        }

        public TEntidade Selecionar(params object[] key)
        {
            return _contexto.Set<TEntidade>().Find(key);
        }

        public TEntidade First(Expression<Func<TEntidade, bool>> predicate)
        {
            return _contexto.Set<TEntidade>().Where(predicate).FirstOrDefault();
        }

       

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RepositorioGenericoEntity() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

        #endregion


    }
}

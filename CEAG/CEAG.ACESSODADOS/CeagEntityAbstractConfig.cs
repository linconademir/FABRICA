using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.ACESSODADOS
{
    public abstract class CeagEntityAbstractConfig<TEntidade> : EntityTypeConfiguration<TEntidade> 
        where TEntidade : class
        //GOTO: Verificar - INDICACAO TARCISIO, new()
    {
        // ReSharper disable once PublicConstructorInAbstractClass
        public CeagEntityAbstractConfig()
        {
            ConfigurarNomeTabela();
            ConfigurarCamposTabela();
            ConfigurarChavePrimaria();
            ConfigurarChaveEstrangeira();
        }

        protected abstract void ConfigurarCamposTabela();
        protected abstract void ConfigurarChaveEstrangeira();
        protected abstract void ConfigurarChavePrimaria();
        protected abstract void ConfigurarNomeTabela();


    }
}

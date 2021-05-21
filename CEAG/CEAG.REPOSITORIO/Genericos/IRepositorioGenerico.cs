using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.REPOSITORIO.Genericos
{
    public interface IRepositorioGenerico<TEntidade, TChave> where TEntidade : class
    {
        void Alterar(TEntidade entidade);
        void Excluir(TEntidade entidade);
        void ExcluirPorId(TChave id);
        void Inserir(TEntidade entidade);
        List<TEntidade> Selecionar(TChave idEscola);
        TEntidade SelecionarPorId(TChave id);
       
        List<TEntidade> SelecionarPorIdPai(TChave id);
    }
}

using AutoMapper;
using CEAG.DOMINIO;
using CEAG.WEB.ViewModel.Acesso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEAG.WEB.Constants
{
    public static class UsuarioEscola
    {
      //  private static Models.ApplicationUser _constanteUsuarioViewModel = null;
        private static int _idEscola = 0;
        private static Escola _escola;
        private static AcessoViewModel _acessoViewModel;


        public static void AdicionarUsuarioConstante(int id, Escola escola, AcessoViewModel acessoViewModel)
        {
            if (id != 0)
            {
                _idEscola = id;
            }
            if (escola != null)
            {
                _escola = escola;
            }
            if (acessoViewModel != null)
            {
                _acessoViewModel = acessoViewModel;
            }

        }

        public static int ResgatarCodigoEscolaSelecionada()
        {
            return _idEscola;
        }

        public static AcessoViewModel ResgatarUsuarioLogado()
        {
            return _acessoViewModel;
        }

        public static Acesso ResgatarUsuarioLogadoAcesso()
        {
            Mapper mapper = Constants.Utils.ViewModelParaDominio();
            return mapper.Map<AcessoViewModel, Acesso>(_acessoViewModel);
        }

        public static Escola ResgatarEscolaLogada()
        {
            return _escola;
        }

        public static void LimparConstantes()
        {
          //  _constanteUsuarioViewModel = null;
            _idEscola = 0;
            _acessoViewModel = null;
            _escola = null;
        }
    }
}
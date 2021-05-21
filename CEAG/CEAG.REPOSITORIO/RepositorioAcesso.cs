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
    public class RepositorioAcesso : RepositorioGenericoEntity<Acesso, int>
    {
        public RepositorioAcesso(DbContext contexto) : base(contexto)
        {
        }

        public override List<Acesso> Selecionar(int id)
        {
            List<Acesso> retorno = _contexto.Set<Acesso>()
                  .Include(p => p.AcessoRoles)
                  .Include("AcessoRoles.Role")
                  .Where(p => p.CodEscola == id).ToList();

            foreach (var item in retorno)
            {
                if (item.CodEscola != null)
                {
                    item.Escola = _contexto.Set<Escola>().Include(p => p.Logradouro).SingleOrDefault(p => p.CodEscola == item.CodEscola);
                }
                if (item.CodEscolaGrupo != null)
                {
                    item.EscolaGrupo = _contexto.Set<EscolaGrupo>().SingleOrDefault(p => p.CodEscolaGrupo == item.CodEscolaGrupo);
                }
            }


            return retorno;
        }

        public List<Acesso> SelecionarPorEmail(string email)
        {
            List<Acesso> retorno = _contexto.Set<Acesso>()
                 .Include(p => p.AcessoRoles)
                 .Include("AcessoRoles.Role")
                 
                 .Where(p => p.Email.ToUpper().Equals(email)).ToList();

            foreach (var item in retorno)
            {
                if (item.CodEscola != null)
                {
                    item.Escola = _contexto.Set<Escola>().Include(p => p.Logradouro).SingleOrDefault(p => p.CodEscola == item.CodEscola);
                }
                if (item.CodEscolaGrupo != null)
                {
                    item.EscolaGrupo = _contexto.Set<EscolaGrupo>().SingleOrDefault(p => p.CodEscolaGrupo == item.CodEscolaGrupo);
                }
            }


            return retorno;
        }

        public override Acesso SelecionarPorId(int id)
        {
            Acesso retorno = _contexto.Set<Acesso>()
                 .Include(p => p.AcessoRoles)
                 .Include("AcessoRoles.Role")
                .SingleOrDefault(p => p.CodAcesso == id);


            if (retorno.CodEscola != null)
            {
                retorno.Escola = _contexto.Set<Escola>().SingleOrDefault(p => p.CodEscola == retorno.CodEscola);
            }
            if (retorno.CodEscolaGrupo != null)
            {
                retorno.EscolaGrupo = _contexto.Set<EscolaGrupo>().SingleOrDefault(p => p.CodEscolaGrupo == retorno.CodEscolaGrupo);
            }

            return retorno;
        }
    }
}

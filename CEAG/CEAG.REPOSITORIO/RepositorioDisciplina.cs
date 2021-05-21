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
    public class RepositorioDisciplina : RepositorioGenericoEntity<Disciplina, int>
    {
        public RepositorioDisciplina(DbContext contexto) : base(contexto)
        {
        }
    }
}

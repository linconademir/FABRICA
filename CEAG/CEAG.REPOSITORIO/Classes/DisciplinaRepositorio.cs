using CEAG.DOMINIO;
using CEAG.REPOSITORIO.Genericos;
using CEAG.REPOSITORIO.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.REPOSITORIO.Classes
{
    public class DisciplinaRepositorio: RepositorioGenericoEf<Disciplina, int>, IDisciplinaRepositorio
    {
        public DisciplinaRepositorio(DbContext contexto) : base(contexto)
        {

        }
    }
}

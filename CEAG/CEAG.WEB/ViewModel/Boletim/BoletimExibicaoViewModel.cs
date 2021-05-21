using CEAG.DOMINIO;
using CEAG.WEB.ViewModel.AlunoMatricula;
using CEAG.WEB.ViewModel.AlunoMatriculaUnidade;
using CEAG.WEB.ViewModel.Disciplina;
using CEAG.WEB.ViewModel.Turma;
using CEAG.WEB.ViewModel.TurmaFuncionarioDisciplinas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEAG.WEB.ViewModel.Boletim
{
    public class BoletimExibicaoViewModel
    {
        public int CodAluno { get; set; }
        public int CodAlunoMatricula { get; set; }
        public List<DisciplinaExibicaoViewModel> DisciplinaExibicaoViewModels { get; set; }
        public List<AlunoMatriculaUnidadeExibicaoViewModel> PrimeiraUnidade { get; set; }
        public List<AlunoMatriculaUnidadeExibicaoViewModel> SegundaUnidade { get; set; }
        public List<AlunoMatriculaUnidadeExibicaoViewModel> TerceiraUnidade { get; set; }
        public List<AlunoMatriculaUnidadeExibicaoViewModel> QuartaUnidade { get; set; }
        
    }
}
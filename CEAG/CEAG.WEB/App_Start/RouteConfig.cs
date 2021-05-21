using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CEAG.WEB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
          name: "ErroGeneral",
          url: "ErroFinal",
          defaults: new { controller = "Home", action = "ErroFinal" }
          );

            #region Reports


            routes.MapRoute(
              name: "ImpressaoFichaAluno",
              url: "Alunos/Ficha/{codAlunoMatricula}",
              defaults: new { controller = "Reports", action = "EmitirFichaAluno", codAlunoMatricula = UrlParameter.Optional }
              );

            routes.MapRoute(
              name: "ImpressaoContrato",
              url: "Alunos/Contrato/{codAlunoMatricula}",
              defaults: new { controller = "EducacionalReports", action = "EmitirContrato", codAlunoMatricula = UrlParameter.Optional }
              );
            #endregion

            #region Aluno


            routes.MapRoute(
              name: "PesquisaAlunos",
              url: "Alunos/FiltrarPorDsc/{pesquisa}",
              defaults: new { controller = "Alunos", action = "FiltrarPorDsc", pesquisa = UrlParameter.Optional }
              );

            routes.MapRoute(
             name: "AlunoCreate",
             url: "Alunos/Incluir",
             defaults: new { controller = "Alunos", action = "Create" }
             );

            routes.MapRoute(
             name: "AlunoEdit",
             url: "Alunos/Editar/{codAluno}",
             defaults: new { controller = "Alunos", action = "Edit", codAluno = UrlParameter.Optional }
             );

            routes.MapRoute(
             name: "AlunoDetails",
             url: "Alunos/Visualizar/{codAluno}",
             defaults: new { controller = "Alunos", action = "Details", codAluno = UrlParameter.Optional }
             );



            #endregion

            #region Telefone

            routes.MapRoute(
            name: "TelefoneCreate",
            url: "Telefones/Incluir/{codAluno}",
            defaults: new { controller = "Telefones", action = "Create", codAluno = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "TelefoneEdit",
             url: "Telefones/Alterar/{codTelefone}",
             defaults: new { controller = "Telefones", action = "Edit", codTelefone = UrlParameter.Optional }
             );

            routes.MapRoute(
           name: "TelefoneDelete",
           url: "Telefones/Excluir/{codTelefone}",
           defaults: new { controller = "Telefones", action = "Delete", codTelefone = UrlParameter.Optional }
           );
            #endregion

            #region AlunoMatriculas

            routes.MapRoute(
              name: "AlunosMatricula",
              url: "Matriculas",
              defaults: new { controller = "AlunoMatriculas", action = "Index" }
              );

            routes.MapRoute(
              name: "AlunosMatriculaDividirValorAnual",
              url: "Matriculas/CalcularValor/{valor}/{ddlQtdParcelas}",
              defaults: new { controller = "AlunoMatriculas", action = "DividirValorAnual", valor = UrlParameter.Optional, ddlQtdParcelas = UrlParameter.Optional }
              );

            routes.MapRoute(
              name: "AlunosMatriculaTurmaCreate",
              url: "Matriculas/Turma/{searchString}/{searchAno}",
              defaults: new { controller = "AlunoMatriculas", action = "MatricularTurma", searchString = UrlParameter.Optional, searchAno = UrlParameter.Optional }
              );

            routes.MapRoute(
             name: "AlunosMatriculaAlunoCreate",
             url: "Matriculas/Aluno/{codTurma}",
             defaults: new { controller = "AlunoMatriculas", action = "MatricularAluno", codTurma = UrlParameter.Optional }
             );

            routes.MapRoute(
             name: "AlunosMatriculaCreate",
             url: "Matriculas/Matricular/{codAluno}/{codTurma}",
             defaults: new { controller = "AlunoMatriculas", action = "Create", codAluno = UrlParameter.Optional, codTurma = UrlParameter.Optional }
             );

            routes.MapRoute(
             name: "AlunosMatriculaEdit",
             url: "Matriculas/Alterar/{codAlunoMatricula}/{codNovaTurma}",
             defaults: new { controller = "AlunoMatriculas", action = "Edit", codAlunoMatricula = UrlParameter.Optional, codNovaTurma = UrlParameter.Optional }
             );

            #endregion

            #region Turmas

            routes.MapRoute(
            name: "VisualizarHorarioTurma",
            url: "Turmas/VisualizarHorario/{codTurma}",
            defaults: new { controller = "Turmas", action = "VisualizarHorario", codTurma = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "TurmasCreate",
            url: "Turmas/Incluir",
            defaults: new { controller = "Turmas", action = "Create" }
            );

            routes.MapRoute(
            name: "TurmasListarSegmento",
            url: "Turmas/Lista/{nivel}",
            defaults: new { controller = "Turmas", action = "ListarSegmento", nivel = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "TurmasRemoverProfessor",
            url: "Turmas/RemoverProfessor/{codTurma}/{codFuncionario}",
            defaults: new { controller = "Turmas", action = "RemoverProfessor", codTurma = UrlParameter.Optional, codFuncionario = UrlParameter.Optional }
            );


            routes.MapRoute(
             name: "TurmasIndex",
             url: "Turmas/{page}",
             defaults: new { controller = "Turmas", action = "Index", page = UrlParameter.Optional }
             );

            routes.MapRoute(
             name: "ListaAno",
             url: "Turmas/ListaAno/{codTurma}",
             defaults: new { controller = "Turmas", action = "ListaAno", codTurma = UrlParameter.Optional }
             );

            routes.MapRoute(
             name: "DuplicarTurma",
             url: "Turmas/Duplicar/{codTurma}/{anoLetivo}",
             defaults: new { controller = "Turmas", action = "DuplicarTurma", codTurma = UrlParameter.Optional, anoLetivo = UrlParameter.Optional }
             );


            routes.MapRoute(
              name: "ListaTurmas",
              url: "Turmas/ListaTurma/{codAlunoMatricula}",
              defaults: new { controller = "Turmas", action = "ListaTurma", codAlunoMatricula = UrlParameter.Optional }
              );


            routes.MapRoute(
             name: "TurmaEdit",
             url: "Turmas/Alterar/{codTurma}",
             defaults: new { controller = "Turmas", action = "Edit", codTurma = UrlParameter.Optional }
             );

            routes.MapRoute(
            name: "AdicionarProfessorTurma",
            url: "Turmas/Professor/{codTurma}/{codFuncionario}",
            defaults: new { controller = "Turmas", action = "AdicionarProfessor", codTurma = UrlParameter.Optional, codFuncionario = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "TurmaDetails",
             url: "Turmas/Visualizar/{codTurma}",
             defaults: new { controller = "Turmas", action = "Details", codTurma = UrlParameter.Optional }
             );

            routes.MapRoute(
           name: "TurmasDelete",
           url: "Turmas/Excluir/{codTurma}",
           defaults: new { controller = "Turmas", action = "Delete", codTurma = UrlParameter.Optional }
           );

            #endregion

            #region Funcionarios

            routes.MapRoute(
            name: "FuncionarioIndex",
            url: "Funcionarios",
            defaults: new { controller = "Funcionarios", action = "Index" }
            );

            routes.MapRoute(
            name: "FuncionarioCreate",
            url: "Funcionarios/Incluir",
            defaults: new { controller = "Funcionarios", action = "Create" }
            );

            routes.MapRoute(
            name: "FuncionarioEdit",
            url: "Funcionarios/Alterar/{codFuncionario}",
            defaults: new { controller = "Funcionarios", action = "Edit", codFuncionario = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "FuncionarioDetails",
            url: "Funcionarios/Visualizar/{codFuncionario}",
            defaults: new { controller = "Funcionarios", action = "Details", codFuncionario = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "FuncionarioDelete",
            url: "Funcionarios/Excluir/{codFuncionario}",
            defaults: new { controller = "Funcionarios", action = "DeletarFuncionario", codFuncionario = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "ListaProfessor",
            url: "Funcionarios/ListaProfessor/{codTurma}",
            defaults: new { controller = "Funcionarios", action = "ListaProfessor", codTurma = UrlParameter.Optional }
            );

            #endregion

            #region AlunoMatriculaUnidades

            //routes.MapRoute(
            //name: "UnidadesNota",
            //url: "AlunoMatriculaUnidades/IncluirNotaPorUnidade/{codAlunoMatricula}",
            //defaults: new { controller = "AlunoMatriculaUnidades", action = "IncluirNotaPorUnidade", codAlunoMatricula = UrlParameter.Optional }
            //);

            routes.MapRoute(
            name: "AlunoMatriculaUnidadesBoletim",
            url: "Boletim/{codAlunoMatricula}",
            defaults: new { controller = "AlunoMatriculaUnidades", action = "Boletim", codAlunoMatricula = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "AlunoMatriculaUnidadesCreate",
            url: "Notas/Incluir/{numeroUnidade}/{codTurma}",
            defaults: new { controller = "AlunoMatriculaUnidades", action = "Create", numeroUnidade = UrlParameter.Optional, codTurma = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "AlunoMatriculaUnidadesIndex",
            url: "Notas/{codTurma}",
            defaults: new { controller = "AlunoMatriculaUnidades", action = "Index", codTurma = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "ListaUnidades",
            url: "AlunoMatriculaUnidades/ListaUnidade/{codTurma}",
            defaults: new { controller = "AlunoMatriculaUnidades", action = "ListaUnidade", codTurma = UrlParameter.Optional }
            );

            #endregion

            #region Aulas

            routes.MapRoute(
            name: "AulasIndex",
            url: "Aulas/{codTurma}",
            defaults: new { controller = "Aulas", action = "Index", codTurma = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "AulasCreate",
            url: "Aulas/Incluir/{codTurma}/{codDisciplina}/{codHorarioAula}",
            defaults: new { controller = "Aulas", action = "Create"
            , codTurma = UrlParameter.Optional
            , codDisciplina = UrlParameter.Optional
            , codHorarioAula = UrlParameter.Optional}
            );

            routes.MapRoute(
           name: "ListaDia",
           url: "Aulas/Dias/{codTurma}/{codDisciplina}",
           defaults: new
           {
               controller = "Aulas",
               action = "ListaDia",
               codTurma = UrlParameter.Optional,
               codDisciplina = UrlParameter.Optional
           }
           );

            routes.MapRoute(
                name: "ListaHorarioDia",
                url: "Aulas/HorarioDias/{codTurma}/{contador}/{codDisciplina}",
                defaults: new
                {
                   controller = "Aulas",
                   action = "ListaHorarioDia",
                   codTurma = UrlParameter.Optional,
                   codDisciplina = UrlParameter.Optional,
                   contador = UrlParameter.Optional
                }
                );

            routes.MapRoute(
          name: "AulasEdit",
          url: "Aulas/Alterar/{codAula}",
          defaults: new { controller = "Aulas", action = "Edit", codAula = UrlParameter.Optional }
          );

            routes.MapRoute(
        name: "AulasDelete",
        url: "Aulas/Excluir/{codAula}",
        defaults: new { controller = "Aulas", action = "DeleteConfirmed", codAula = UrlParameter.Optional }
        );

            routes.MapRoute(
           name: "AulasDetails",
           url: "Aulas/Visualizar/{codAula}",
           defaults: new { controller = "Aulas", action = "Details", codAula = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "ListaDisciplinas",
              url: "Aulas/ListaDisciplina/{codTurma}",
              defaults: new { controller = "Aulas", action = "ListaDisciplina", codTurma = UrlParameter.Optional }
          );

            #endregion

            #region Responsavels

            routes.MapRoute(
              name: "ResponsavelsCreate",
              url: "Responsavel/Incluir/{codAluno}",
              defaults: new { controller = "Responsavels", action = "Create", codAluno = UrlParameter.Optional }
              );

            routes.MapRoute(
             name: "ResponsavelsEdit",
             url: "Responsavel/Alterar/{codResponsavel}",
             defaults: new { controller = "Responsavels", action = "Edit", codResponsavel = UrlParameter.Optional }
             );

            routes.MapRoute(
             name: "ResponsavelsDelete",
             url: "Responsavel/Excluir/{codResponsavel}",
             defaults: new { controller = "Responsavels", action = "Delete", codResponsavel = UrlParameter.Optional }
             );

            #endregion

            #region Parentesco

            routes.MapRoute(
              name: "ParentescoCreate",
              url: "Parentesco/Incluir/{codAluno}",
              defaults: new { controller = "ParentescoAlunos", action = "Create", codAluno = UrlParameter.Optional }
              );

           

            #endregion

            #region Informacoes

            routes.MapRoute(
              name: "InformacoesCreate",
              url: "Informacoes/Incluir/{codAluno}",
              defaults: new { controller = "Informacaos", action = "Create", codAluno = UrlParameter.Optional }
              );

            routes.MapRoute(
             name: "InformacoesEdit",
             url: "Informacoes/Alterar/{codInformacao}",
             defaults: new { controller = "Informacaos", action = "Edit", codInformacao = UrlParameter.Optional }
             );

            routes.MapRoute(
             name: "InformacoesDelete",
             url: "Informacoes/Excluir/{codInformacao}",
             defaults: new { controller = "Informacaos", action = "Delete", codInformacao = UrlParameter.Optional }
             );



            #endregion

            #region Professor

            routes.MapRoute(
              name: "FuncionarioDisciplinasIndex",
              url: "Professor",
              defaults: new { controller = "FuncionarioDisciplinas", action = "Index" }
              );

            routes.MapRoute(
              name: "FuncionarioDisciplinasCreate",
              url: "Professor/Incluir/{codFuncionario}",
              defaults: new { controller = "FuncionarioDisciplinas", action = "Create", codFuncionario = UrlParameter.Optional }
              );

            routes.MapRoute(
              name: "FuncionarioDisciplinasEdit",
              url: "Professor/Alterar/{codFuncionario}",
              defaults: new { controller = "FuncionarioDisciplinas", action = "Edit", codFuncionario = UrlParameter.Optional }
              );


            routes.MapRoute(
              name: "FuncionarioDisciplinasDelete",
              url: "Professor/Excluir/{codFuncionario}",
              defaults: new { controller = "FuncionarioDisciplinas", action = "DeletarProfessor", codFuncionario = UrlParameter.Optional }
              );

            #endregion

            #region Disciplina

            routes.MapRoute(
              name: "DisciplinasIndex",
              url: "Disciplina",
              defaults: new { controller = "Disciplinas", action = "Index" }
              );

            routes.MapRoute(
              name: "DisciplinasCreate",
              url: "Disciplina/Incluir",
              defaults: new { controller = "Disciplinas", action = "Create" }
              );

            routes.MapRoute(
              name: "DisciplinasEdit",
              url: "Disciplina/Alterar/{codDisciplina}",
              defaults: new { controller = "Disciplinas", action = "Edit", codDisciplina = UrlParameter.Optional }
              );

            routes.MapRoute(
              name: "DisciplinasDelete",
              url: "Disciplina/Excluir/{codDisciplina}",
              defaults: new { controller = "Disciplinas", action = "Delete", codDisciplina = UrlParameter.Optional }
              );

            #endregion

            #region Horarios

            routes.MapRoute(
            name: "HorarioIndex",
            url: "Horarios",
            defaults: new { controller = "Horarios", action = "Index" }
            );

            routes.MapRoute(
            name: "HorarioCreate",
            url: "Horarios/Incluir",
            defaults: new { controller = "Horarios", action = "Create" }
            );

            routes.MapRoute(
               name: "HorarioEdit",
               url: "Horarios/AlterarHorario/{codHorario}",
               defaults: new { controller = "Horarios", action = "Edit", codHorario = UrlParameter.Optional }
               );

            routes.MapRoute(
              name: "HorarioDelete",
              url: "Horarios/Excluir/{codHorario}",
              defaults: new { controller = "Horarios", action = "Delete", codHorario = UrlParameter.Optional }
              );

            routes.MapRoute(
            name: "HorarioAulasCreate",
            url: "Aulas/IncluirHorario/{codHorario}",
            defaults: new { controller = "HorarioAulas", action = "Create", codHorario = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //name: "HorarioAulaIncluir",
            //url: "Horarios/Excluir/{codHorario}",
            //defaults: new { controller = "Horarios", action = "Delete", codHorario = UrlParameter.Optional }
            //);
            #endregion

            #region Feriados

            routes.MapRoute(
               name: "FeriadosIndex",
               url: "Feriados",
               defaults: new { controller = "Feriados", action = "Index" }
               );

            routes.MapRoute(
              name: "FeriadosCreate",
              url: "Feriados/Incluir",
              defaults: new { controller = "Feriados", action = "Create" }
              );

            routes.MapRoute(
              name: "FeriadosEdit",
              url: "Feriados/Alterar/{codFeriado}",
              defaults: new { controller = "Feriados", action = "Edit", codFeriado = UrlParameter.Optional }
              );

            routes.MapRoute(
             name: "FeriadosDelete",
             url: "Feriados/Excluir/{codFeriado}",
             defaults: new { controller = "Feriados", action = "Delete", codFeriado = UrlParameter.Optional }
             );
            #endregion

            #region Unidades

            routes.MapRoute(
             name: "UnidadesIndex",
             url: "Unidades",
             defaults: new { controller = "Unidades", action = "Index" }
             );

            routes.MapRoute(
             name: "UnidadesCreate",
             url: "Unidades/Incluir/{erro}",
             defaults: new { controller = "Unidades", action = "Create", erro = UrlParameter.Optional }
             );

            routes.MapRoute(
             name: "UnidadesEdit",
             url: "Unidades/Alterar/{codUnidade}/{erro}",
             defaults: new { controller = "Unidades", action = "Edit", codUnidade = UrlParameter.Optional, erro = UrlParameter.Optional }
             );

            #endregion

            #region AlunoQuestionario

            routes.MapRoute(
           name: "AlunoQuestionarioCreate",
           url: "Ficha/Preenchimento/{codAluno}",
           defaults: new { controller = "AlunoQuestionarios", action = "Create", codAluno = UrlParameter.Optional }
           );

            #endregion

            #region Taxas

            routes.MapRoute(
              name: "TaxaIndex",
              url: "Taxa",
              defaults: new { controller = "Taxas", action = "Index" }
              );

            routes.MapRoute(
              name: "TaxaCreate",
              url: "Taxa/Incluir",
              defaults: new { controller = "Taxas", action = "Create" }
              );

            routes.MapRoute(
              name: "TaxaEdit",
              url: "Taxa/Alterar/{codTaxa}",
              defaults: new { controller = "Taxas", action = "Edit", codTaxa = UrlParameter.Optional }
              );

            routes.MapRoute(
              name: "TaxaCancel",
              url: "Taxa/Excluir/{codTaxa}",
              defaults: new { controller = "Taxas", action = "CancelarTaxa", codTaxa = UrlParameter.Optional }
              );


            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}

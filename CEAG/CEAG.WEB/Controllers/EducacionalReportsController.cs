using AutoMapper;
using CEAG.ACESSODADOS.Context;
using CEAG.DOMINIO;
using CEAG.DOMINIO.Procedure;
using CEAG.NEGOCIO.Classes;
using CEAG.REPOSITORIO;
using CEAG.WEB.Constants;
using CEAG.WEB.Reports;
using QRCoder;
using Rotativa;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CEAG.WEB.Controllers
{
    public class EducacionalReportsController : Controller
    {
        #region Emitir Contrato

        public ActionResult EmitirReciboNovo(int? codDebito)
        {

            #region Mapper

            Mapper mapper = Constants.Utils.DominioParaViewModel();

            #endregion

            using (RepositorioDebito _repoLocal = new RepositorioDebito(new CeagDbContext()))
            {
                var debito = _repoLocal.SelecionarPorId(codDebito.Value);
                if (debito.AlunoMatricula.Aluno.CodEscola != UsuarioEscola.ResgatarCodigoEscolaSelecionada())
                {
                    return CarregarMensagemDeErro("OPERAÇÃO ILEGAL - Não é possivel Imprimir o recibo indicado.");
                }
            }

            #region 
            List<AlunoReciboMensal> debitoProc = new List<AlunoReciboMensal>();
            using (RepositorioDebito _repoLocal = new RepositorioDebito(new CeagDbContext()))
            {
                debitoProc = _repoLocal.EmitirReciboMensal(codDebito.Value);
            }

            if (!debitoProc.Any())
            {
                return CarregarMensagemDeErro("OPERAÇÃO ILEGAL - Não é possivel Imprimir o recibo indicado.");
            }

            #endregion

            CarregarQrCode(UsuarioEscola.ResgatarEscolaLogada(),debitoProc[0].IDENTIFICADOR_VC_DEBITO);

            var pdf = new ViewAsPdf
            {
                ViewName = "EmitirReciboNovo",
                //PageSize = Size.A4,
                // IsGrayScale = true,
                Model = debitoProc[0],
                //Se quiser mostrar na tela, suprimir a linha abaixo
                //FileName = "Recibo" + debitoProc[0].NOME_VC_ALUNO + ".pdf"
            };
            return pdf;
        }

        

        #endregion

        #region Geração do relatório

        public ActionResult EmitirContrato(int codAlunoMatricula)
        {
            using (RepositorioAlunoMatricula _repositorioAlunoMatriculaLocal = new RepositorioAlunoMatricula(new CeagDbContext()))
            {
                List<AlunoContrato> alunoContrato = _repositorioAlunoMatriculaLocal.EmitirContratoAluno(codAlunoMatricula);
                DataTable table = GenericToDataTable.ConvertToDataTable(alunoContrato);
                string urlPdfGerada = RelatorioUtil.GerarRelatorioPDF(table, new RelContratoMatricula());
                return Redirect(urlPdfGerada);
            }
        }

        public ActionResult EmitirReciboMensal(int codDebito)
        {
            using (RepositorioDebito _repositorioDebitoLocal = new RepositorioDebito(new CeagDbContext()))
            {
                List<AlunoReciboMensal> alunoReciboMensal = _repositorioDebitoLocal.EmitirReciboMensal(codDebito);
                DataTable table = GenericToDataTable.ConvertToDataTable(alunoReciboMensal);
                string urlPdfGerada = RelatorioUtil.GerarRelatorioPDF(table, new RelReciboPagamento());
                return Redirect(urlPdfGerada);
            }
        }

        public ActionResult EmitirRelatorioAlunos(int codTurma)
        {
            using (RepositorioTurma _repositorioTurmaLocal = new RepositorioTurma(new CeagDbContext()))
            {
                List<AlunosTurma> alunosTurma = _repositorioTurmaLocal.EmitirRelatorioAlunos(codTurma);
                DataTable table = GenericToDataTable.ConvertToDataTable(alunosTurma);
                string urlPdfGerada = RelatorioUtil.GerarRelatorioPDF(table, new RelListaAluno());
                return Redirect(urlPdfGerada);
            }   
        }

        #endregion

        #region Gerar QrCode para Html

        private void CarregarQrCode(Escola escola, string codificacao)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(escola.Fantasia+"["+ codificacao+"]", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ViewBag.imageBytes = ms.ToArray();
                }
            }
        }


        #endregion

        private ActionResult CarregarMensagemDeErro(string message)
        {
            Erro erroParametro = new Erro
            {
                CodErro = 404,
                statusCode = HttpStatusCode.BadRequest,
                MensagemErro = message,
                UrlChamada = System.Web.HttpContext.Current.Request.UrlReferrer == null ? "" : System.Web.HttpContext.Current.Request.UrlReferrer.ToString()
            };
            Session.Add("erroParametro", erroParametro);
            return RedirectToAction("ErroFinal", "Home");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;

namespace CEAG.WEB.Reports
{
    public class RelatorioUtil
    {

        #region Métodos

        public static String GerarRelatorioPDF(DataTable ds, ReportClass arquivoRelatorio)
        {
            return GerarRelatorioPDF(ds, arquivoRelatorio, null, null, null, null);
        }

        public static String GerarRelatorioPDF(DataTable ds, ReportClass arquivoRelatorio,
            List<ParametroRelatorio> parametros)
        {
            return GerarRelatorioPDF(ds, arquivoRelatorio, null, null, parametros, null);
        }

        public static String GerarRelatorioPDF(DataTable ds, ReportClass arquivoRelatorio,
            List<ParametroRelatorio> parametros, ConexaoRelatorio conexaoRelatorio)
        {
            return GerarRelatorioPDF(ds, arquivoRelatorio, null, null, parametros, conexaoRelatorio);
        }

        /// <summary>
        /// Gera o relatório em PDF
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="nomeArquivoRPT"></param>
        /// <returns></returns>
        public static String GerarRelatorioPDF(DataTable ds, ReportClass arquivoRelatorio, string pastaEspecifica,
            string nomeEspecificoArquivo, List<ParametroRelatorio> parametros, ConexaoRelatorio conexaoRelatorio)
        {
            Random r = new Random();
            int numeroRandomico = r.Next(99999);

            string pastaDestino = "Reports";
            if (!string.IsNullOrEmpty(pastaEspecifica)) pastaDestino = pastaEspecifica;

            string nomeArquivoRPT = arquivoRelatorio.ResourceName;
            string extensaoParaExportacao = ".pdf";

            string nomeIsoladoArquivo = string.Concat("Documento_" + nomeArquivoRPT + "_", DateTime.Now.ToString("yyyyMMddHHmmss") + "_", numeroRandomico.ToString(), extensaoParaExportacao);
            if (!string.IsNullOrEmpty(nomeEspecificoArquivo))
            {
                nomeIsoladoArquivo = string.Concat(nomeEspecificoArquivo + "_", DateTime.Now.ToString("yyyyMMddHHmmss") + "_", numeroRandomico.ToString(), extensaoParaExportacao);
            }
            string caminhoFullArquivo = AppDomain.CurrentDomain.BaseDirectory + @pastaDestino + @"\" + nomeIsoladoArquivo;

            try
            {
                Report reportFile = new Report();
                reportFile.FileName = AppDomain.CurrentDomain.BaseDirectory + @"Reports\" + nomeArquivoRPT;

                CrystalReportSource crystalReportsSource = new CrystalReportSource();
                crystalReportsSource.CacheDuration = 0;
                crystalReportsSource.EnableCaching = false;
                crystalReportsSource.Report = reportFile;
                crystalReportsSource.ReportDocument.SetDataSource(ds);

                #region Manipulando os parâmetros externos do relátório
                if (parametros != null)
                {
                    foreach (ParametroRelatorio parametro in parametros)
                    {
                        crystalReportsSource.ReportDocument.SetParameterValue(parametro.NomeParametro, parametro.ValorParametro);
                    }
                }
                #endregion

                ExportFormatType formatoTipo = ExportFormatType.PortableDocFormat;
                ExportOptions opcoesExportacao = new ExportOptions();
                DiskFileDestinationOptions opcoesDestinoArquivo = new DiskFileDestinationOptions();
                opcoesDestinoArquivo.DiskFileName = caminhoFullArquivo;
                opcoesExportacao.ExportDestinationType = ExportDestinationType.DiskFile; //Define o destino do arquivo o qual será exportado
                opcoesExportacao.ExportFormatType = formatoTipo; //Define o tipo a ser exportado
                opcoesExportacao.ExportDestinationOptions = opcoesDestinoArquivo; //Informa a abstração do relatorio qual será as opções de destino da exportação através do objeto ProductsOptionsFile

                // Adicionando informações do banco de dados
                if (conexaoRelatorio != null) crystalReportsSource.ReportDocument.SetDatabaseLogon(conexaoRelatorio.Login, conexaoRelatorio.Senha, conexaoRelatorio.Servidor, conexaoRelatorio.BancoDados);

                crystalReportsSource.ReportDocument.Export(opcoesExportacao); //Efetua a exportação
                crystalReportsSource.Dispose();

                // Montando a URL virtual do arquivo
                Uri requestURI = HttpContext.Current.Request.Url;
                string pathVirtual = requestURI.Scheme + "://" + requestURI.Host + ":" + requestURI.Port + HttpContext.Current.Request.ApplicationPath;
                if (!pathVirtual.EndsWith("/")) pathVirtual = pathVirtual + "/";
                caminhoFullArquivo = pathVirtual + pastaDestino.Replace("\\", "/").Replace(@"\", "/") + "/" + nomeIsoladoArquivo;

                return caminhoFullArquivo;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        #endregion

    }
}
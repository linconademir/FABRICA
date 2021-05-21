using AutoMapper;
using CEAG.ACESSODADOS.Context;
using CEAG.DOMINIO;
using CEAG.NEGOCIO.Enum;
using CEAG.REPOSITORIO;
using CEAG.WEB.AutoMapper;
using CEAG.WEB.ViewModel.Acesso;
using CEAG.WEB.ViewModel.Horario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace CEAG.WEB.Constants
{
    public static class Utils
    {
        public static void EnviarEmail(int codAlunoMatricula)
        {
            Models.SendEmail.EnviarNotificacaoProfessor(null);
        }

        public static void VerificarNomeDiaSemana(HorarioAulaViewModel itemAula)
        {
            var nomedia = string.Empty;
            switch (itemAula.DiaSemana)
            {
                case 2:
                    nomedia = "Segunda";
                    break;
                case 3:
                    nomedia = "Terça";
                    break;
                case 4:
                    nomedia = "Quarta";
                    break;
                case 5:
                    nomedia = "Quinta";
                    break;
                case 6:
                    nomedia = "Sexta";
                    break;
                default:
                    nomedia = "Todos os dias";
                    break;
            }

            itemAula.NomeDiaSemana = nomedia;
        }

        public static Retorno Validar()
        {
            Retorno ret = new Retorno
            {
                Sucesso = false,
                Mensagem = "Erro na leitura do arquvivo."
            };

            //Verificando se o arquivo existe
            string curFile = "C:\\Aplicacao\\OLIE.txt";
            if (File.Exists(curFile))
            {
                using (StreamReader reader = new StreamReader("C:\\Aplicacao\\OLIE.txt", true))
                {
                    string retorno;
                    using (AesManaged aes = new AesManaged())
                    {
                        while ((retorno = reader.ReadLine()) != null)
                        {
                            if (!string.IsNullOrEmpty(retorno))
                            {
                                var decript = CriptografiaHelper.Decriptar("vocepula,eupulo.", "nicoleteamoamor.", retorno);
                                DateTime data = new DateTime(Convert.ToInt32(decript.Substring(0, 4)), Convert.ToInt32(decript.Substring(4, 2)), Convert.ToInt32(decript.Substring(6, 2)));
                                if (data.AddDays(10) >= DateTime.Now)
                                {
                                    ret = new Retorno
                                    {
                                        Sucesso = true,
                                        Mensagem = "Vencimento em " + data.AddDays(10).ToString()
                                    };
                                }
                            }
                        }
                    }
                }
                File.Delete(curFile);

                using (StreamWriter writer = new StreamWriter("C:\\Aplicacao\\OLIE.txt", true))
                {
                    using (AesManaged aes = new AesManaged())
                    {
                        var cript = CriptografiaHelper.Encriptar("vocepula,eupulo.", "nicoleteamoamor.", DateTime.Now.ToString("yyyyMMdd"));
                        writer.WriteLine(cript);
                    }
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter("C:\\Aplicacao\\OLIE.txt", true))
                {
                    using (AesManaged aes = new AesManaged())
                    {
                        var cript = CriptografiaHelper.Encriptar("vocepula,eupulo.", "nicoleteamoamor.", DateTime.Now.ToString("yyyyMMdd"));
                        writer.WriteLine(cript);
                    }
                }
            }

            return ret;
        }


        #region Mapeamento
        public static Mapper DominioParaViewModel()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DominioParaViewModelProfile>());
            var mapper = new Mapper(config);
            return mapper;
        }

        public static Mapper ViewModelParaDominio()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelParaDominioProfile>());
            var mapper = new Mapper(config);
            return mapper;
        }

        #endregion

        #region Lista com Enums

        public static SelectList CarregarFormasDePagamento()
        {
            List<SelectListItem> listaFormaPagamento = (from EnumComum.FormaPagamento d in Enum.GetValues(typeof(EnumComum.FormaPagamento))
                                                        select new SelectListItem
                                                        {
                                                            Text = d.ToString(),
                                                            Value = d.ToString()
                                                        }).ToList();
            var dropdownFormaPagamento = new SelectList(listaFormaPagamento, "Value", "Text");
            return dropdownFormaPagamento;
        }

        public static SelectList CarregarTipoDebito()
        {
            List<SelectListItem> listaTipoDebito = (from EnumComum.TipoDebito d in Enum.GetValues(typeof(EnumComum.TipoDebito))
                                                    select new SelectListItem
                                                    {
                                                        Text = d.ToString(),
                                                        Value = d.ToString()
                                                    }).ToList();
            var dropdownTipoDebito = new SelectList(listaTipoDebito, "Value", "Text");
            return dropdownTipoDebito;
        }

        public static SelectList CarregarTipoAdvertencia()
        {
            List<SelectListItem> lista = (from EnumComum.TiposAdvertencia d in Enum.GetValues(typeof(EnumComum.TiposAdvertencia))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarTipoParentesco()
        {
            List<SelectListItem> lista = (from EnumComum.TipoParentesco d in Enum.GetValues(typeof(EnumComum.TipoParentesco))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarMotivoAdvertencia()
        {
            List<SelectListItem> lista = (from EnumComum.MotivoAdvertencia d in Enum.GetValues(typeof(EnumComum.MotivoAdvertencia))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarTipoUrgenciaAdvertencia()
        {
            List<SelectListItem> lista = (from EnumComum.TiposUrgenciaAdvertencia d in Enum.GetValues(typeof(EnumComum.TiposUrgenciaAdvertencia))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarLocalTelefone()
        {
            List<SelectListItem> lista = (from EnumComum.TipoLocalTelefone d in Enum.GetValues(typeof(EnumComum.TipoLocalTelefone))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarTipoTelefone()
        {
            List<SelectListItem> lista = (from EnumComum.TipoTelefone d in Enum.GetValues(typeof(EnumComum.TipoTelefone))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarListaSexo()
        {
            List<SelectListItem> lista = (from EnumComum.Sexo d in Enum.GetValues(typeof(EnumComum.Sexo))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarListaSimNao()
        {
            List<SelectListItem> lista = (from EnumComum.SimNao d in Enum.GetValues(typeof(EnumComum.SimNao))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarListaNaoSim()
        {
            List<SelectListItem> lista = (from EnumComum.SimNao d in Enum.GetValues(typeof(EnumComum.SimNao))
                                          orderby d descending
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarListaBolsista()
        {
            List<SelectListItem> lista = (from EnumComum.Bolsista d in Enum.GetValues(typeof(EnumComum.Bolsista))
                                          orderby d descending
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarTipoResponsavel()
        {
            List<SelectListItem> lista = (from EnumComum.TipoResponsavel d in Enum.GetValues(typeof(EnumComum.TipoResponsavel))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarTipoContato()
        {
            List<SelectListItem> lista = (from EnumComum.TipoContato d in Enum.GetValues(typeof(EnumComum.TipoContato))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarTipoPessoaContato()
        {
            List<SelectListItem> lista = (from EnumComum.TipoPessoaContato d in Enum.GetValues(typeof(EnumComum.TipoPessoaContato))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarTipoFeriado()
        {
            List<SelectListItem> lista = (from EnumComum.TipoFeriado d in Enum.GetValues(typeof(EnumComum.TipoFeriado))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarTitulacao()
        {
            List<SelectListItem> lista = (from EnumComum.Titulacao d in Enum.GetValues(typeof(EnumComum.Titulacao))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }


        public static SelectList CarregarTurnos()
        {
            List<SelectListItem> lista = (from EnumComum.Turno d in Enum.GetValues(typeof(EnumComum.Turno))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList CarregarTipoTaxas()
        {
            List<SelectListItem> lista = (from EnumComum.TipoTaxa d in Enum.GetValues(typeof(EnumComum.TipoTaxa))
                                          select new SelectListItem
                                          {
                                              Text = d.ToString(),
                                              Value = d.ToString()
                                          }).ToList();
            return new SelectList(lista, "Value", "Text");
        }
        //public static SelectList CarregarListaTipoEnum(List<Enum listaPreDefinida)
        //{
        //    List<SelectListItem> lista = (from EnumComum.TipoFeriado d in Enum.GetValues(typeof(EnumComum.TipoFeriado))
        //                                  select new SelectListItem
        //                                  {
        //                                      Text = d.ToString(),
        //                                      Value = d.ToString()
        //                                  }).ToList();
        //    return new SelectList(lista, "Value", "Text");
        //}

        public static SelectList CarregarListaUf()
        {
            var listaUF = CarregarListaUfSimples();
            return new SelectList(listaUF, "Value", "Text");
        }
        #endregion

        #region Listas Simples
        private static List<SelectListItem> CarregarListaUfSimples()
        {
            var lista = new List<SelectListItem>
            {
                new SelectListItem { Value = "RO", Text = "RO" },
                new SelectListItem { Value = "AC", Text = "AC" },
                new SelectListItem { Value = "AM", Text = "AM" },
                new SelectListItem { Value = "RR", Text = "RR" },
                new SelectListItem { Value = "PA", Text = "PA" },
                new SelectListItem { Value = "AP", Text = "AP" },
                new SelectListItem { Value = "TO", Text = "TO" },
                new SelectListItem { Value = "MA", Text = "MA" },
                new SelectListItem { Value = "PI", Text = "PI" },
                new SelectListItem { Value = "CE", Text = "CE" },
                new SelectListItem { Value = "RN", Text = "RN" },
                new SelectListItem { Value = "PB", Text = "PB" },
                new SelectListItem { Value = "PE", Text = "PE" },
                new SelectListItem { Value = "AL", Text = "AL" },
                new SelectListItem { Value = "SE", Text = "SE" },
                new SelectListItem { Value = "BA", Text = "BA" },
                new SelectListItem { Value = "MG", Text = "MG" },
                new SelectListItem { Value = "ES", Text = "ES" },
                new SelectListItem { Value = "RJ", Text = "RJ" },
                new SelectListItem { Value = "SP", Text = "SP" },
                new SelectListItem { Value = "PR", Text = "PR" },
                new SelectListItem { Value = "SC", Text = "SC" },
                new SelectListItem { Value = "RS", Text = "RS" },
                new SelectListItem { Value = "MS", Text = "MS" },
                new SelectListItem { Value = "MT", Text = "MT" },
                new SelectListItem { Value = "GO", Text = "GO" },
                new SelectListItem { Value = "DF", Text = "DF" }
            };

            return lista;

        }

        public static SelectList CarregarInformacoesAdicionais()
        {
            var listaInformacoes = new List<SelectListItem>();
            listaInformacoes.Add(new SelectListItem { Value = "Alergia", Text = "Alergia" });
            listaInformacoes.Add(new SelectListItem { Value = "Alimento não permitido", Text = "Alimento não permitido" });
            listaInformacoes.Add(new SelectListItem { Value = "Problema de saúde", Text = "Problema de saúde" });
            listaInformacoes.Add(new SelectListItem { Value = "Febre alta", Text = "Febre alta" });
            listaInformacoes.Add(new SelectListItem { Value = "Outro", Text = "Outro" });

            var dropdownListaInformacoes = new SelectList(listaInformacoes, "Value", "Text");

            return dropdownListaInformacoes;
        }

        public static SelectList CarregarListaEstadoCivil()
        {
            var listaEstadoCivil = new List<SelectListItem>
            {
                new SelectListItem { Value = "NÃO INFORMADO", Text = "NÃO INFORMADO" },
                new SelectListItem { Value = "SOLTEIRO(A)", Text = "SOLTEIRO(A)" },
                new SelectListItem { Value = "CASADO(A)", Text = "CASADO(A)" },
                new SelectListItem { Value = "DIVORCIADO(A)", Text = "DIVORCIADO(A)" },
                new SelectListItem { Value = "VIÚVO(A)", Text = "VIÚVO(A)" },
            };


            var dropdownEstadoCivil = new SelectList(listaEstadoCivil, "Value", "Text");

            return dropdownEstadoCivil;
        }

        public static SelectList CarregarListaNivel()
        {
            var listaNivel = new List<SelectListItem>();
            listaNivel.Add(new SelectListItem { Value = "1º Ano", Text = "1º Ano" });
            listaNivel.Add(new SelectListItem { Value = "2º Ano", Text = "2º Ano" });
            listaNivel.Add(new SelectListItem { Value = "3º Ano", Text = "3º Ano" });
            listaNivel.Add(new SelectListItem { Value = "4º Ano", Text = "4º Ano" });
            listaNivel.Add(new SelectListItem { Value = "5º Ano", Text = "5º Ano" });
            listaNivel.Add(new SelectListItem { Value = "6º Ano", Text = "6º Ano" });
            listaNivel.Add(new SelectListItem { Value = "7º Ano", Text = "7º Ano" });
            listaNivel.Add(new SelectListItem { Value = "8º Ano", Text = "8º Ano" });
            listaNivel.Add(new SelectListItem { Value = "9º Ano", Text = "9º Ano" });
            listaNivel.Add(new SelectListItem { Value = "MATERNAL I", Text = "MATERNAL I" });
            listaNivel.Add(new SelectListItem { Value = "MATERNAL II", Text = "MATERNAL II" });
            listaNivel.Add(new SelectListItem { Value = "JARDIM I", Text = "JARDIM I" });
            listaNivel.Add(new SelectListItem { Value = "JARDIM II", Text = "JARDIM II" });

            var dropdownListaNivel = new SelectList(listaNivel, "Value", "Text");

            return dropdownListaNivel;
        }

        public static SelectList CarregarDiasSemana()
        {
            var listaDiaSemana = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "TODOS OS DIAS" },
                new SelectListItem { Value = "2", Text = "SEGUNDA" },
                new SelectListItem { Value = "3", Text = "TERÇA" },
                new SelectListItem { Value = "4", Text = "QUARTA" },
                new SelectListItem { Value = "5", Text = "QUINTA" },
                new SelectListItem { Value = "6", Text = "SEXTA" }
            };

            var dropdownDiaSemana = new SelectList(listaDiaSemana, "Value", "Text");

            return dropdownDiaSemana;
        }

        public static SelectList CarregarAno()
        {
            var listaAnoLetivo = new List<SelectListItem>
            {
                new SelectListItem { Value = "TODOS", Text = "TODOS" },
                new SelectListItem { Value = (DateTime.Now.Year+1).ToString(), Text = (DateTime.Now.Year+1).ToString() },
                new SelectListItem { Value = DateTime.Now.Year.ToString(), Text = (DateTime.Now.Year).ToString()},
                new SelectListItem { Value = (DateTime.Now.Year-1).ToString(), Text = (DateTime.Now.Year-1).ToString() }
            };

            var dropdownAno = new SelectList(listaAnoLetivo, "Value", "Text");
            return dropdownAno;
        }

        public static SelectList CarregarMeses()
        {
            var listaMeses = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "TODOS" }

            };

            listaMeses.AddRange(CultureInfo.CurrentCulture.DateTimeFormat.MonthNames
               .TakeWhile(m => m != String.Empty)
               .Select((monthName, index) => new SelectListItem
               {
                   Value = (index + 1).ToString(),
                   Text = monthName.ToUpper(),
                   Selected = DateTime.Today.Month == (index + 1)
               }));


            var dropdownAno = new SelectList(listaMeses, "Value", "Text");
            return dropdownAno;
        }

        public static SelectList CarregarQuantidadeParcelasPadrao()
        {
            var listaQuantidadeParcelas = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "SELECIONE AS PARCELAS" },
                new SelectListItem { Value = "1", Text = "1 PARCELA" },
                new SelectListItem { Value = "2", Text = "2 PARCELAS" },
                new SelectListItem { Value = "3", Text = "3 PARCELAS" },
                new SelectListItem { Value = "4", Text = "4 PARCELAS" },
                new SelectListItem { Value = "5", Text = "5 PARCELAS" },
                new SelectListItem { Value = "6", Text = "6 PARCELAS" },
                new SelectListItem { Value = "7", Text = "7 PARCELAS" },
                new SelectListItem { Value = "8", Text = "8 PARCELAS" },
                new SelectListItem { Value = "9", Text = "9 PARCELAS" },
                new SelectListItem { Value = "10", Text = "10 PARCELAS" },
                new SelectListItem { Value = "11", Text = "11 PARCELAS" },
                new SelectListItem { Value = "12", Text = "12 PARCELAS" },
            };


            var dropdownQuantidadeParcelas = new SelectList(listaQuantidadeParcelas, "Value", "Text");
            return dropdownQuantidadeParcelas;
        }


        public static SelectList CarregarQuantidadeParcelasMatricula()
        {
            var listaQuantidadeParcelas = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "SELECIONE AS PARCELAS" },
                new SelectListItem { Value = "1", Text = "1 PARCELA" },
                new SelectListItem { Value = "2", Text = "MATRICULA + 1 PARCELA" },
                new SelectListItem { Value = "3", Text = "MATRICULA + 2 PARCELAS" },
                new SelectListItem { Value = "4", Text = "MATRICULA + 3 PARCELAS" },
                new SelectListItem { Value = "5", Text = "MATRICULA + 4 PARCELAS" },
                new SelectListItem { Value = "6", Text = "MATRICULA + 5 PARCELAS" },
                new SelectListItem { Value = "7", Text = "MATRICULA + 6 PARCELAS" },
                new SelectListItem { Value = "8", Text = "MATRICULA + 7 PARCELAS" },
                new SelectListItem { Value = "9", Text = "MATRICULA + 8 PARCELAS" },
                new SelectListItem { Value = "10", Text = "MATRICULA + 9 PARCELAS" },
                new SelectListItem { Value = "11", Text = "MATRICULA + 10 PARCELAS", Selected = true },
                new SelectListItem { Value = "12", Text = "MATRICULA + 11 PARCELAS" },
            };


            var dropdownQuantidadeParcelas = new SelectList(listaQuantidadeParcelas, "Value", "Text");
            return dropdownQuantidadeParcelas;
        }

        public static SelectList CarregarFormaPagamento()
        {

            var listaFormaPagamento = new List<SelectListItem>
            {
                new SelectListItem { Value = "BOLETO", Text = "BOLETO" },
                new SelectListItem { Value = "CARNÊ", Text = "CARNÊ" },
                new SelectListItem { Value = "DINHEIRO", Text = "DINHEIRO" },
                new SelectListItem { Value = "CARTÃO DE CRÉDITO", Text = "CARTÃO DE CRÉDITO" },
                new SelectListItem { Value = "CARTÃO DE DÉBITO", Text = "CARTÃO DE DÉBITO" },
                new SelectListItem { Value = "CHEQUE", Text = "CHEQUE" },
                new SelectListItem { Value = "TRANFERÊNCIA BANCÁRIA", Text = "TRANFERÊNCIA BANCÁRIA" }
            };


            var dropdownFormaPagamento = new SelectList(listaFormaPagamento, "Value", "Text");

            return dropdownFormaPagamento;
        }
        #endregion

        public static string GetDisplayNameEnum(Enum enumValue)
        {
            var displayName = enumValue.GetType().GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()?
                        .Name;

            return displayName ?? enumValue.ToString();
        }
    }
}
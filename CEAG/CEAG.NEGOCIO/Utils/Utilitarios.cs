using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEAG.NEGOCIO.Utils
{
    public static class Utilitarios
    {
        public static DateTime CalculaDiasUteis(DateTime dataInicio, int dias)
        {
            DateTime resultado = dataInicio;
            while (dias > -1)
            {
                //Se é sábado, domingo ou feriado, ando um dia pra frente
                if (resultado.DayOfWeek == DayOfWeek.Saturday || resultado.DayOfWeek == DayOfWeek.Sunday)// || feriados.Contains(resultado))
                {
                    resultado = resultado.AddDays(1);
                }
                //Ou se quiser adicionar um dia útil (X horas trabalhadas = 1 dia útil)
                else if (dias > 0)
                {
                    resultado = resultado.AddDays(1);
                    dias -= 1;
                }
                //Se a data final for no fim de semana ou feriado
                else if (dias == 0)
                {
                    while (resultado.DayOfWeek == DayOfWeek.Saturday || resultado.DayOfWeek == DayOfWeek.Sunday) // || feriados.Contains(resultado))
                    {
                        resultado = resultado.AddDays(1);
                    }
                    dias = -1;
                }
            }
            return resultado;
        }

        public static int VerificarQuantasSemanaMes(DateTime data)
        {
            int qtdSemanas = 5;
            DateTime diaInicio = new DateTime(data.Year, data.Month, 1);
            var dias = DateTime.DaysInMonth(data.Year, data.Month);
            if (dias == 28)
            {
                if (new DateTime(data.Year, data.Month, 1).DayOfWeek == DayOfWeek.Sunday)
                {
                    qtdSemanas = 4;
                }
            }

            if (dias == 30)
            {
                if (new DateTime(data.Year, data.Month, 1).DayOfWeek == DayOfWeek.Saturday)
                {
                    qtdSemanas = 6;
                }
            }
            if (dias == 31)
            {
                if (new DateTime(data.Year, data.Month, 1).DayOfWeek == DayOfWeek.Saturday || new DateTime(data.Year, data.Month, 1).DayOfWeek == DayOfWeek.Friday)
                {
                    qtdSemanas = 6;
                }
            }

            return qtdSemanas;
        }


    }
}

using CEAG.DOMINIO;
using System;
using System.Collections.Generic;
using System.Linq;

using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Helper;
using System.Drawing;
using CEAG.NEGOCIO.Classes;

namespace CEAG.NEGOCIO.Utils
{
    public class Security : IDisposable
    {

        public string GeradorIdentificador(int tamanho, string concatenaInicio, string concatenaFim, Escola escola)
        {
            string identificador = Identificador(tamanho, concatenaInicio, concatenaFim, escola);

            return identificador;
        }
        
        #region Métodos Privados

        private string Identificador(int tamanho, string concatenaInicio, string concatenaFim, Escola escola)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = escola.Fantasia.Substring(1, 3) + concatenaInicio + new string(
                Enumerable.Repeat(chars, tamanho)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray()) + concatenaFim;
            return result;
        }

       
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

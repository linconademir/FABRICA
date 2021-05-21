using CEAG.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEAG.WEB.Anotations
{
    public class CpfvalidadeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var cpf = Convert.ToString(value);

            return string.IsNullOrEmpty(cpf) || Validations.ValidaCpf(cpf);
        }
    }
}
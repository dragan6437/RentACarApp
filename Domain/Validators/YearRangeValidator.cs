using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class YearRangeValidator : ValidationAttribute
    {
        public override bool IsValid(object text)
        {
            return (int)text >= DateTime.Now.AddYears(-10).Year && (int)text <= DateTime.Now.Year;
        }
    }
}

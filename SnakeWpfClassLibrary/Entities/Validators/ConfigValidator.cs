using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SnakeWpfClassLibrary.Entities.Validators
{
    public class ConfigValidator : ValidationAttribute
    {
        public ConfigValidator()
        {
           
        }

        public ConfigValidator(String errorMessage, int min, int max) : this(min, max)
        {
            this.ErrorMessage = errorMessage;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace aula_5.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="O E-mail é origatório!", AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email {get; set; }

        [Required(ErrorMessage ="A Senha é obrigatória!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Manter logado!")]
        public bool flManterLogado { get; set; }
    }
}
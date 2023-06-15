using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace aula_5.Models
{
    public class Aluno
    {
        public int alunoId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Nome do Aluno é obrigatório!", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O tamanho mínimo é de 3 e o máximo de 50 caracteres!")]
        public string nome { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O CPF do Aluno é obrigatório!", AllowEmptyStrings = false)]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O tamanho do CPF deve ser de 11 dígitos!")]
        public string cpf { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O E-mail do Aluno é obrigatório!", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public string email { get; set; }

        // Chave Estrangeira
        [Display(Name = "Estado Civil")]
        [Required(ErrorMessage = "O Estado Civil do Aluno é obrigatório!", AllowEmptyStrings = false)]
        public int EstadoCivilId { get; set; }
        // Entidade
        public EstadoCivil EstadoCivil { get; set; }

        // Chave Estrangeira
        [Display(Name = "Sexo")]
         [Required(ErrorMessage = "O Sexo do Aluno é obrigatório!", AllowEmptyStrings = false)]
        public int SexoId { get; set; }
        // Entidade
        public Sexo Sexo { get; set; }
    }
}
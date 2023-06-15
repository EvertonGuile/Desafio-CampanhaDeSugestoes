using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aula_5.Models
{
    public class EstadoCivil
    {
        public int EstadoCivilId { get; set; }
        public string Nome { get; set; }

        // A propriedade "Alunos" é uma lista da classe Aluno, fazendo o relacionamento de 1 para muitos.
        //Além disso, foi inserida na propriedade, a palavra "virtual", o que possibilita uma sobrescrita,
        //que é usada pelo EF Core no processo de Lazy loading.
        public virtual IList<Aluno> Alunos { get; set; }
    }
}
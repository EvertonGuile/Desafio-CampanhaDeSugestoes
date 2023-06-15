using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Resolve o "Aluno"
using aula_5.Models;
// Resolve o "IAplicationBuilder"
using Microsoft.AspNetCore.Builder;
// Resolve o ".CreateScope()"
using Microsoft.Extensions.DependencyInjection;
// Resolve o ".Migrate"
using Microsoft.EntityFrameworkCore;

namespace aula_5.Context
{
    public class GerarDadosDB
    {
        public static void IncluiDadosDB(IApplicationBuilder app)
        {
           using (var serviceScope = app.ApplicationServices.CreateScope())
           {
            var context = serviceScope.ServiceProvider.GetService<MyContext>();

            context.Database.Migrate();
            
            if (!context.Alunos.Any())
            {
                context.Alunos.AddRange(
                    new Aluno(){ nome = "Aluno 1", cpf = "123", email = "123@gmail.com", SexoId=1, EstadoCivilId=1},
                    new Aluno(){ nome = "Aluno 2", cpf = "456", email = "456@gmail.com", SexoId=1, EstadoCivilId=2},
                    new Aluno(){ nome = "Aluno 3", cpf = "789", email = "789@gmail.com", SexoId=2, EstadoCivilId=3},
                    new Aluno(){ nome = "Aluno 4", cpf = "012", email = "012@gmail.com", SexoId=1, EstadoCivilId=3}
                );
                context.SaveChanges();
                
                context.Sexo.AddRange(
                    new Sexo(){ Nome="Masculino" },
                    new Sexo(){ Nome="Feminino"},
                    new Sexo(){ Nome="Não Informado"}
                );
                context.SaveChanges();

                context.EstadoCivil.AddRange(
                    new EstadoCivil(){ Nome="Viúvo(a)" },
                    new EstadoCivil(){ Nome="Casado(a)" },
                    new EstadoCivil(){ Nome="Solteiro(a)" }
                );
                context.SaveChanges();
            }
           } 
        }
    }
}
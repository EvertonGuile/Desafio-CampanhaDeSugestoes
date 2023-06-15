using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aula_5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace aula_5.Context
{
    public class MyContext : IdentityDbContext<Usuario>
    {
        public DbSet<Aluno> Alunos {get; set;}

        // Incluí as novas entidades EstadoCivil e Sexo, nas propriedades DbSet
        public DbSet<EstadoCivil> EstadoCivil { get; set; }
        public DbSet<Sexo> Sexo { get; set; }

        // TALVES RETIRE "<MyContext>"
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Aluno>().ToTable("Aluno");

            //modelBuilder.Entity<Aluno>().HasKey(e => e.alunoId);
            //modelBuilder.Entity<Aluno>().Property(e => e.alunoId).ValueGeneratedOnAdd().UseIdentityColumn();

            // Incluí as Entidades EstadoCivil e Sexo
            modelBuilder.Entity<EstadoCivil>().ToTable("EstadoCivil");
            modelBuilder.Entity<Sexo>().ToTable("Sexo");
        }

        

    }
}
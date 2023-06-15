using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//
using Microsoft.EntityFrameworkCore;
using aula_5.Context;
using aula_5.Models;


namespace aula_5.Controllers
{
    public class AlunoController : Controller
    {
        private readonly MyContext _MyContext;

        public AlunoController(MyContext myContext)
        {
            this._MyContext = myContext;
        }

        // ...
        public async Task<IActionResult> Index()
        {
            return View(await _MyContext.Alunos.Include(i => i.EstadoCivil)
                                               .Include(s => s.Sexo)
                                               .OrderBy(c => c.alunoId) .ToArrayAsync());
        }

//        public IActionResult Create()
//        {
//            return View();
//        }

        // ...
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.EstadoCivil = await _MyContext.EstadoCivil.OrderBy(c => c.Nome)
                                .Select( c=> new SelectListItem()
                                {   Text = c.Nome, Value = c.EstadoCivilId.ToString() })
                                .ToArrayAsync();
            ViewBag.Sexo = await _MyContext.Sexo.OrderBy(c => c.Nome)
                            .ToArrayAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Aluno pAluno)
        {
            _MyContext.Add(pAluno);
            await _MyContext.SaveChangesAsync();
                TempData["type"] = "success";
                TempData["title"] = "Inserção:";
                TempData["body"] = "Cadastrado com sucesso!";

            return RedirectToAction("Index");
        }


        // ...
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var alunoAux = await _MyContext.Alunos.SingleOrDefaultAsync(m => m.alunoId == id);
            if (alunoAux == null)
            {
                return NotFound();
            }
            ViewBag.EstadoCivil = await _MyContext.EstadoCivil.OrderBy(c => c.Nome)
                                .Select(c => new SelectListItem()
                                { Text = c.Nome, Value = c.EstadoCivilId.ToString() })
                                .ToArrayAsync();
            ViewBag.Sexo = await _MyContext.Sexo.OrderBy(c => c.Nome)
                                .ToArrayAsync();
            return View(alunoAux);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Aluno pAluno)
        {
            _MyContext.Update(pAluno);
            await _MyContext.SaveChangesAsync();
                TempData["type"] = "success";
                TempData["title"] = "Edição:";
                TempData["body"] = "Editado com sucesso!";
            return RedirectToAction("Index");
        }

        // ...
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var aluno = await _MyContext.Alunos.SingleOrDefaultAsync(m => m.alunoId == id);
            
            if (aluno == null)
            {
                return NotFound();
            }
            
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var aluno = await _MyContext.Alunos.FindAsync(id);
            
            if (aluno == null)
            {
                return NotFound();
            }
            
            _MyContext.Alunos.Remove(aluno);
            await _MyContext.SaveChangesAsync();
                TempData["type"] = "success";
                TempData["title"] = "Deleção:";
                TempData["body"] = "Deletado com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
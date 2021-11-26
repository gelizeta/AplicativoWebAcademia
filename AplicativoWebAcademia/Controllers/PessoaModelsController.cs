using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicativoWebAcademia.Data;
using AplicativoWebAcademia.Models;
using AplicativoWebAcademia.regras;

namespace AplicativoWebAcademia.Controllers
{
    public class PessoaModelsController : Controller
    {
        private readonly AplicativoWebAcademiaContext _context;

        public PessoaModelsController(AplicativoWebAcademiaContext context)
        {
            _context = context;
        }

        // GET: PessoaModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PessoaModel.ToListAsync());
        }

        // GET: PessoaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // GET: PessoaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PessoaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,QuantidadeFilhos,Email,Salario,DataNascimento")] PessoaModel pessoaModel)
        {
            pessoaModel.Alterar = "Ativo";
            _context.Update(pessoaModel);
            return View(pessoaModel);
        }
       
        // GET: PessoaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel == null)
            {
                return NotFound();
            }
            return View(pessoaModel);
        }

        // POST: PessoaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,QuantidadeFilhos,Email,Salario,DataNascimento,Alterar")] PessoaModel pessoaModel)
        {
            if (id != pessoaModel.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (PessoaRegras.VerificaFilhos(pessoaModel.QuantidadeFilhos))
                {
                    ModelState.AddModelError("regras", "A quantidade minima de filhos é zero!");
                    return View(pessoaModel);
                }
                if (PessoaRegras.VerificaNascimento(pessoaModel.DataNascimento))
                {
                    ModelState.AddModelError("regras", "A data de nascimento deve ser igual ou superior a 01/01/1990");
                    return View(pessoaModel);
                }
                if (PessoaRegras.VerificaSalarioMin(pessoaModel.Salario))
                {
                    ModelState.AddModelError("regras", "O salário não pode ser inferior a R$ 1.200");
                    return View(pessoaModel);
                }
                if (PessoaRegras.VerificaSalarioMax(pessoaModel.Salario))
                {
                    ModelState.AddModelError("regras", "O salário não pode ser superior a R$ 13.000");
                    return View(pessoaModel);
                }
                var pessoaEmail = _context.PessoaModel.Where(x => x.Email.Equals(pessoaModel.Email) && x.Codigo != pessoaModel.Codigo);
                if (pessoaEmail.Count() > 0)
                {
                    ModelState.AddModelError("regras", "O Email já está cadastrado!");
                    return View(pessoaModel);
                }
                if (PessoaRegras.VerificaInativo(pessoaModel.Alterar))
                {
                    ModelState.AddModelError("regras", "Não é possivel editar uma pessoa na situação 'Inativo'");
                    return View(pessoaModel);
                }
                try
                {
                    _context.Update(pessoaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaModelExists(pessoaModel.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pessoaModel);
        }
        [HttpGet]
        public async Task<IActionResult> Alterar(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel == null)
            {
                return NotFound();
            }
            return View(pessoaModel);
        }
        // GET: PessoaModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // POST: PessoaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            
            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (PessoaRegras.VerificaAtivo(pessoaModel.Alterar))
            {
                ModelState.AddModelError("regras", "Não é possivel excluir uma pessoa na situação 'Ativo'");
                return View(pessoaModel);
            }
            _context.PessoaModel.Remove(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool PessoaModelExists(int id)
        {
            return _context.PessoaModel.Any(e => e.Codigo == id);
        }

        // GET: PessoaModels/VerificaAlteração/5
        [HttpGet]
        public async Task<IActionResult> VerificaAlteração(int id)
        {
            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel.Alterar.Equals("Ativo"))
            {
                pessoaModel.Alterar = "Ativo";
            }
            else
            {
                pessoaModel.Alterar = "Inativo";
            }
            _context.Update(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

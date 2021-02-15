using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crud.Models;

namespace Crud.Controllers
{
    public class ProdutoesController : Controller
    {
        private readonly Context _context;

        public ProdutoesController(Context context)
        {
            _context = context;
        }

        // GET: Produtoes
        public IActionResult Index()
        {
            try
            {
                var stdList = from a in _context.tbl_Produto
                              join b in _context.tbl_Categoria
                              on a.CategoriaId equals b.ID
                              into Categorias
                              from b in Categorias.DefaultIfEmpty()

                              select new Produto
                              {
                                  ProdutoId = a.ProdutoId,
                                  Nome = a.Nome,
                                  Descricao = a.Descricao,
                                  Valor = a.Valor,
                                  CategoriaId = a.CategoriaId,
                                  Categoria = b == null ? "" : b.Categoria
                              };


                return View(stdList);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Produtoes/Create
        public IActionResult Create()
        {
            LoadDDL();
            return View();
        }

        // POST: Produtoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdutoId,Nome,Descricao,CategoriaId,Valor")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.tbl_Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            LoadDDL();
            return View(produto);
            
        }

        // POST: Produtoes/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoId,Nome,Descricao,CategoriaId,Valor")] Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.ProdutoId))
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
            
            return View(produto);
        }

        // GET: Produtoes/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.tbl_Produto
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }
            
            return View(produto);
        }

        // POST: Produtoes/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.tbl_Produto.FindAsync(id);
            _context.tbl_Produto.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.tbl_Produto.Any(e => e.ProdutoId == id);
        }

        private void LoadDDL()
        {
            try
            {
                List<Categorias> catList = new List<Categorias>();
                catList = _context.tbl_Categoria.ToList();
                catList.Insert(0, new Categorias { ID = 0, Categoria = "Selecione a categoria" });

                ViewBag.CatList = catList;
            }
            catch (Exception ex)
            {

            }
        }

    }
}

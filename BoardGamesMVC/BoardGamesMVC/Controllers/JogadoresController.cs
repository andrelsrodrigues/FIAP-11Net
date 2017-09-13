using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoardGamesMVC.Data;
using BoardGamesMVC.Models;

namespace BoardGamesMVC.Controllers
{
    public class JogadoresController : Controller
    {
        private readonly BoardGamesContext _context;

        public JogadoresController(BoardGamesContext context)
        {
            _context = context;    
        }

        // GET: Jogadores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jogadores.ToListAsync());
        }

        // GET: Jogadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogador = await _context.Jogadores
                .SingleOrDefaultAsync(m => m.IdJogador == id);
            if (jogador == null)
            {
                return NotFound();
            }

            return View(jogador);
        }

        // GET: Jogadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jogadores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdJogador,Nome,DataNascimento")] Jogador jogador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jogador);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(jogador);
        }

        // GET: Jogadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogador = await _context.Jogadores.SingleOrDefaultAsync(m => m.IdJogador == id);
            if (jogador == null)
            {
                return NotFound();
            }
            return View(jogador);
        }

        // POST: Jogadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdJogador,Nome,DataNascimento")] Jogador jogador)
        {
            if (id != jogador.IdJogador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogadorExists(jogador.IdJogador))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(jogador);
        }

        // GET: Jogadores/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var jogador = await _context.Jogadores.Where(b => b.IdJogador == id).FirstAsync();

            if (jogador == null)
            {
                return NotFound();
            }

            _context.Jogadores.Remove(jogador);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool JogadorExists(int id)
        {
            return _context.Jogadores.Any(e => e.IdJogador == id);
        }
    }
}

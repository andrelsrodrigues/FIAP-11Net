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
    public class PartidasController : Controller
    {
        private readonly BoardGamesContext _context;

        public PartidasController(BoardGamesContext context)
        {
            _context = context;    
        }

        // GET: Partidas
        public async Task<IActionResult> Index()
        {
            var boardGamesContext = _context.Partidas.Include(p => p.InfoBoardGame);
            return View(await boardGamesContext.ToListAsync());
        }

        // GET: Partidas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partida = await _context.Partidas
                .Include(p => p.InfoBoardGame)
                .SingleOrDefaultAsync(m => m.IdPartida == id);
            if (partida == null)
            {
                return NotFound();
            }

            return View(partida);
        }

        // GET: Partidas/Create
        public IActionResult Create()
        {
            ViewData["IdBoardGame"] = new SelectList(_context.BoardGames, "IdBoardGame", "IdBoardGame");
            return View();
        }

        // POST: Partidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPartida,QuantidadeJogadores,DuracaoPartidaEmMinutos,Data,IdBoardGame")] Partida partida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partida);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["IdBoardGame"] = new SelectList(_context.BoardGames, "IdBoardGame", "IdBoardGame", partida.IdBoardGame);
            return View(partida);
        }

        // GET: Partidas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partida = await _context.Partidas.SingleOrDefaultAsync(m => m.IdPartida == id);
            if (partida == null)
            {
                return NotFound();
            }
            ViewData["IdBoardGame"] = new SelectList(_context.BoardGames, "IdBoardGame", "IdBoardGame", partida.IdBoardGame);
            return View(partida);
        }

        // POST: Partidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPartida,QuantidadeJogadores,DuracaoPartidaEmMinutos,Data,IdBoardGame")] Partida partida)
        {
            if (id != partida.IdPartida)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartidaExists(partida.IdPartida))
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
            ViewData["IdBoardGame"] = new SelectList(_context.BoardGames, "IdBoardGame", "IdBoardGame", partida.IdBoardGame);
            return View(partida);
        }

        // GET: Partidas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partida = await _context.Partidas
                .Include(p => p.InfoBoardGame)
                .SingleOrDefaultAsync(m => m.IdPartida == id);
            if (partida == null)
            {
                return NotFound();
            }

            return View(partida);
        }

        // POST: Partidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partida = await _context.Partidas.SingleOrDefaultAsync(m => m.IdPartida == id);
            _context.Partidas.Remove(partida);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PartidaExists(int id)
        {
            return _context.Partidas.Any(e => e.IdPartida == id);
        }
    }
}

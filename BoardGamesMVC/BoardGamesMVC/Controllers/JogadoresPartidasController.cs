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
    public class JogadoresPartidasController : Controller
    {
        private readonly BoardGamesContext _context;

        public JogadoresPartidasController(BoardGamesContext context)
        {
            _context = context;    
        }

        // GET: JogadoresPartidas
        public async Task<IActionResult> Index()
        {
            var boardGamesContext = _context.JogadoresPartidas.Include(j => j.InfoJogador).Include(j => j.InfoPartida);
            return View(await boardGamesContext.ToListAsync());
        }

        // GET: JogadoresPartidas/Details/5
        public async Task<IActionResult> Details(int? idPartida, int? idJogador)
        {
            if (idPartida == null || idJogador == null)
            {
                return NotFound();
            }

            var jogadorPartida = await _context.JogadoresPartidas
                .Include(j => j.InfoJogador)
                .Include(j => j.InfoPartida)
                .SingleOrDefaultAsync(m => m.IdJogador == idJogador && m.IdPartida == idPartida);
            if (jogadorPartida == null)
            {
                return NotFound();
            }

            return View(jogadorPartida);
        }

        // GET: JogadoresPartidas/Create
        public IActionResult Create()
        {
            ViewData["IdJogador"] = new SelectList(_context.Jogadores, "IdJogador", "IdJogador");
            ViewData["IdPartida"] = new SelectList(_context.Partidas, "IdPartida", "IdPartida");
            return View();
        }

        // POST: JogadoresPartidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pontuacao,PosicaoFinalPartida,IdPartida,IdJogador")] JogadorPartida jogadorPartida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jogadorPartida);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["IdJogador"] = new SelectList(_context.Jogadores, "IdJogador", "IdJogador", jogadorPartida.IdJogador);
            ViewData["IdPartida"] = new SelectList(_context.Partidas, "IdPartida", "IdPartida", jogadorPartida.IdPartida);
            return View(jogadorPartida);
        }

        // GET: JogadoresPartidas/Edit/5
        public async Task<IActionResult> Edit(int? idPartida, int? idJogador)
        {
            if (idPartida == null || idJogador == null)
            {
                return NotFound();
            }

            var jogadorPartida = await _context.JogadoresPartidas.SingleOrDefaultAsync(m => m.IdJogador == idJogador && m.IdPartida == idPartida);
            if (jogadorPartida == null)
            {
                return NotFound();
            }
            ViewData["IdJogador"] = new SelectList(_context.Jogadores, "IdJogador", "IdJogador", jogadorPartida.IdJogador);
            ViewData["IdPartida"] = new SelectList(_context.Partidas, "IdPartida", "IdPartida", jogadorPartida.IdPartida);
            return View(jogadorPartida);
        }

        // POST: JogadoresPartidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idPartida, int idJogador, [Bind("Pontuacao,PosicaoFinalPartida,IdPartida,IdJogador")] JogadorPartida jogadorPartida)
        {
            if (idPartida != jogadorPartida.IdPartida && idJogador != jogadorPartida.IdJogador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogadorPartida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogadorPartidaExists(jogadorPartida.IdJogador, jogadorPartida.IdPartida))
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
            ViewData["IdJogador"] = new SelectList(_context.Jogadores, "IdJogador", "IdJogador", jogadorPartida.IdJogador);
            ViewData["IdPartida"] = new SelectList(_context.Partidas, "IdPartida", "IdPartida", jogadorPartida.IdPartida);
            return View(jogadorPartida);
        }

        // GET: JogadoresPartidas/Delete/5
        public async Task<IActionResult> Delete(int? idPartida, int? IdJogador)
        {
            if (idPartida == null && IdJogador == null)
            {
                return NotFound();
            }

            var jogadorPartida = await _context.JogadoresPartidas
                .Include(j => j.InfoJogador)
                .Include(j => j.InfoPartida)
                .SingleOrDefaultAsync(m => m.IdJogador == IdJogador && m.IdPartida == idPartida);
            if (jogadorPartida == null)
            {
                return NotFound();
            }

            return View(jogadorPartida);
        }

        // POST: JogadoresPartidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idPartida , int idJogador)
        {
            var jogadorPartida = await _context.JogadoresPartidas.SingleOrDefaultAsync(m => m.IdJogador == idJogador && m.IdPartida == idPartida);
            _context.JogadoresPartidas.Remove(jogadorPartida);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool JogadorPartidaExists(int idPartida, int idJogador)
        {
            return _context.JogadoresPartidas.Any(e => e.IdJogador == idJogador && e.IdPartida == idPartida);
        }
    }
}

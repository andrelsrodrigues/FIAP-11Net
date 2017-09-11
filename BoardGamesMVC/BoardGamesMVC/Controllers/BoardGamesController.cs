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
    public class BoardGamesController : Controller
    {
        private readonly BoardGamesContext _context;

        public BoardGamesController(BoardGamesContext context)
        {
            _context = context;    
        }

        // GET: BoardGames
        public async Task<IActionResult> Index()
        {
            var boardGamesContext = _context.BoardGames.Include(b => b.InfoJogador);
            return View(await boardGamesContext.ToListAsync());
        }

        // GET: BoardGames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardGame = await _context.BoardGames
                .Include(b => b.InfoJogador)
                .SingleOrDefaultAsync(m => m.IdBoardGame == id);
            if (boardGame == null)
            {
                return NotFound();
            }

            return View(boardGame);
        }

        // GET: BoardGames/Create
        public IActionResult Create()
        {
            ViewData["IdJogador"] = new SelectList(_context.Jogadores, "IdJogador", "IdJogador");
            return View();
        }

        // POST: BoardGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBoardGame,Nome,Fabricante,QuantidadeMinimaJogadores,QuantidadeMaximaJogadores,DuracaoMediaPartidaEmMinutos,IdJogador")] BoardGame boardGame)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boardGame);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["IdJogador"] = new SelectList(_context.Jogadores, "IdJogador", "IdJogador", boardGame.IdJogador);
            return View(boardGame);
        }

        // GET: BoardGames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardGame = await _context.BoardGames.SingleOrDefaultAsync(m => m.IdBoardGame == id);
            if (boardGame == null)
            {
                return NotFound();
            }
            ViewData["IdJogador"] = new SelectList(_context.Jogadores, "IdJogador", "IdJogador", boardGame.IdJogador);
            return View(boardGame);
        }

        // POST: BoardGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBoardGame,Nome,Fabricante,QuantidadeMinimaJogadores,QuantidadeMaximaJogadores,DuracaoMediaPartidaEmMinutos,IdJogador")] BoardGame boardGame)
        {
            if (id != boardGame.IdBoardGame)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boardGame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardGameExists(boardGame.IdBoardGame))
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
            ViewData["IdJogador"] = new SelectList(_context.Jogadores, "IdJogador", "IdJogador", boardGame.IdJogador);
            return View(boardGame);
        }

        // GET: BoardGames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardGame = await _context.BoardGames
                .Include(b => b.InfoJogador)
                .SingleOrDefaultAsync(m => m.IdBoardGame == id);
            if (boardGame == null)
            {
                return NotFound();
            }

            return View(boardGame);
        }

        // POST: BoardGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boardGame = await _context.BoardGames.SingleOrDefaultAsync(m => m.IdBoardGame == id);
            _context.BoardGames.Remove(boardGame);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BoardGameExists(int id)
        {
            return _context.BoardGames.Any(e => e.IdBoardGame == id);
        }
    }
}

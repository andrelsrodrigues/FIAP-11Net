using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using BoardGamesMVC.Models;

namespace BoardGamesMVC.Data
{
    public class BoardGamesContext : DbContext
    {
        /*
        public BoardGamesContext()
            : base(new DbContextOptionsBuilder<BoardGamesContext>()
                                    .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=DBBoardGames;Trusted_Connection=True;MultipleActiveResultSets=true")
                                    .Options){ }
        */
        
        public BoardGamesContext(DbContextOptions<BoardGamesContext> options)
            : base(options) { }


        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        //public DbSet<JogadorPartida> JogadoresPartidas { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardGame>().ToTable("TBBoardGames");
            modelBuilder.Entity<Partida>().ToTable("TBPartidas");
            modelBuilder.Entity<JogadorPartida>().ToTable("TBJogadoresPartidas");
            modelBuilder.Entity<Jogador>().ToTable("TBJogadores");

            modelBuilder.Entity<JogadorPartida>().HasKey(jp => new { jp.IdJogador, jp.IdPartida });
            modelBuilder.Entity<Jogador>().HasMany(j => j.Partidas);
            modelBuilder.Entity<Partida>().HasMany(p => p.Jogadores);
            modelBuilder.Entity<Partida>().HasOne(p => p.InfoBoardGame);
            modelBuilder.Entity<BoardGame>().HasMany(p => p.Partidas);
        }
    }
}

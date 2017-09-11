using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGamesMVC.Models
{
    public class Jogador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdJogador { get; set; }

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }


        public IEnumerable<JogadorPartida> JogadoresPartidas { get; set; }
        public IEnumerable<BoardGame> ListaBoardGames { get; set; }
    }
}

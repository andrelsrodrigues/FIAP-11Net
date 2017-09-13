using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGamesMVC.Models
{
    public class Partida
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPartida { get; set; }

        public int DuracaoPartidaEmMinutos { get; set; }
        public DateTime Data { get; set; }


        public int IdBoardGame { get; set; }
        public BoardGame InfoBoardGame { get; set; }

        public IEnumerable<JogadorPartida> Jogadores { get; set; }

    }
}

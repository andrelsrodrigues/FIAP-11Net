using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGamesMVC.Models
{
    public class BoardGame
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBoardGame { get; set; }

        public string Nome { get; set; }
        public string Fabricante { get; set; }

        public int QuantidadeMinimaJogadores { get; set; }
        public int QuantidadeMaximaJogadores { get; set; }
        public int DuracaoMediaPartidaEmMinutos { get; set; }

        public IEnumerable<Partida> PartidasBoardGame { get; set; }

    }
}

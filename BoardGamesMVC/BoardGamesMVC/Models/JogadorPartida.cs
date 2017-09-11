using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGamesMVC.Models
{
    public class JogadorPartida
    {
        public int PosicaoFinalPartida { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdPartida { get; set; }
        public Partida InfoPartida { get; set; }

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdJogador { get; set; }
        public Jogador InfoJogador { get; set; }
    }
}

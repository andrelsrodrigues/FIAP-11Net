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
        
        [Required]
        [StringLength(30)]
        public string Nome { get; set; }

        [Required]
        [StringLength(30)]
        public string Fabricante { get; set; }

        [Required]
        [Range(1,99)]
        [Display(Name = "Mínimo de Jogadores")]
        public int QuantidadeMinimaJogadores { get; set; }

        [Required]
        [Range(1,99)]
        [Display(Name = "Máximo de Jogadores")]
        public int QuantidadeMaximaJogadores { get; set; }

        [Required]
        [Display(Name = "Duração")]
        public int DuracaoMediaPartidaEmMinutos { get; set; }

        public IEnumerable<Partida> Partidas { get; set; }
    }
}

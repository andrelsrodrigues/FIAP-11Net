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

        [Required]
        public string Nome { get; set; }

        [Display(Name ="Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        public IEnumerable<JogadorPartida> Partidas { get; set; }
    }
}

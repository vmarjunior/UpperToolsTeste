using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }

        [NotMapped]
        public int ArquivosTotal { get; set; }
        public List<Arquivo> Arquivos { get; set; }
    }
}

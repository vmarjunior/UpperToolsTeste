using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class Arquivo
    {
        public int IdArquivo { get; set; }
        public int IdCliente { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime DataEnvio { get; set; }

        [NotMapped]
        public int DespesasTotal { get; set; }

        public Cliente Cliente { get; set; }
        public List<Despesa> Despesas { get; set; }
    }
}

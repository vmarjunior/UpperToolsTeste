using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public enum TipoDespesa
    {
        Nao_Identificado,
        Conta_Luz,
        Conta_Agua,
        Conta_Gas,
        Conta_Internet,
        Conta_Locacao,
        Conta_Hospedagem
    }

    public class Despesa
    {
        public int IdDespesa { get; set; }
        public int IdArquivo { get; set; }
        public TipoDespesa TipoDespesa { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal? ValorCobrado { get; set; }
        public decimal? ValorPago { get; set; }
        public decimal? ValorMulta { get; set; }

        public Arquivo Arquivo { get; set; }
    }
}

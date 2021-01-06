export interface Despesa {
  idDespesa: number;
  idArquivo: number;
  tipoDespesa: TipoDespesa;
  valorDespesa: number;
  dataVencimento: Date;
  dataPagamento: Date;
  valorCobrado: number;
  valorPago: number;
  valorMulta: number;
}

export enum TipoDespesa {
  Conta_Luz = 1,
  Conta_Agua = 2,
  Conta_Gas = 3,
  Conta_Internet = 4,
  Conta_Locacao = 5,
  Conta_Hospedagem = 6
}

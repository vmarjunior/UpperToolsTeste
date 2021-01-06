import { Cliente } from "./cliente";
import { Despesa } from "./despesa";

export interface Arquivo {
  idArquivo: number;
  idCliente: number;
  nomeArquivo: string;
  dataEnvio: Date;
  despesasTotal: number;
  despesas: Despesa[];
  cliente: Cliente;
}

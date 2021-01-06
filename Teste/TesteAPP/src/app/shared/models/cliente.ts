import { Arquivo } from './arquivo';

export interface Cliente {
  idCliente: number;
  nomeCliente: string;
  arquivos: Arquivo[];
  arquivosTotal: number;
}

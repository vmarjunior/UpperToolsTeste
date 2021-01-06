import { Arquivo } from './../../../shared/models/arquivo';
import { ArquivoService } from './../../services/arquivo.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-arquivo',
  templateUrl: './arquivo.component.html',
  styleUrls: ['./arquivo.component.css']
})
export class ArquivoComponent implements OnDestroy {

  clientes: Arquivo[];
  clientesFiltrados: Arquivo[];
  subscription: Subscription;

  constructor(private clienteService: ArquivoService) {
    this.subscription = this.clienteService.GetAll().subscribe(clientes => {
      this.clientes = this.clientesFiltrados = clientes;
    });
  }

  filter(query: string) {
    this.clientesFiltrados = (query) ?
      this.clientes.filter(p => (p.nomeArquivo.toLowerCase().includes(query.toLowerCase()))) :
      this.clientes;
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}

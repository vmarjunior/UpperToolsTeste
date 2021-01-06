import { Component, OnDestroy } from '@angular/core';
import { ClienteService } from 'app/core/services/cliente.service';
import { Subscription } from 'rxjs';
import { Cliente } from 'shared/models/cliente';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnDestroy {

  clientes: Cliente[];
  clientesFiltrados: Cliente[];
  subscription: Subscription;

  constructor(private clienteService: ClienteService) {
    this.subscription = this.clienteService.GetAll().subscribe(clientes => {
      this.clientes = this.clientesFiltrados = clientes;
    });
  }

  filter(query: string) {
    this.clientesFiltrados = (query) ?
      this.clientes.filter(p => (p.nomeCliente.toLowerCase().includes(query.toLowerCase()))) :
      this.clientes;
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}

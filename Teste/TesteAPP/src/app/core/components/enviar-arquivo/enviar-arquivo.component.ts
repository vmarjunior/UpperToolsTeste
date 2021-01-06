import { ToastrService } from 'ngx-toastr';
import { ClienteService } from 'app/core/services/cliente.service';
import { Component, OnInit } from '@angular/core';
import { Cliente } from 'shared/models/cliente';

@Component({
  selector: 'app-enviar-arquivo',
  templateUrl: './enviar-arquivo.component.html',
  styleUrls: ['./enviar-arquivo.component.css']
})
export class EnviarArquivoComponent {

  clientes: Cliente[];
  clienteSelecionado: Cliente;
  mostrarUpload: boolean = false;

  constructor(
    private clienteService: ClienteService,
    private toastr: ToastrService) {
   }

  filter(query: string) {
    if(!query) {
      this.toastr.warning("Informe um nome para procurar.");
      return;
    }

    this.clienteService.GetAllIncludeFilesByFilter(query).subscribe(clientes => {
      this.clientes = clientes;
    });
  }

  selecionarCliente(cliente: Cliente){
    this.mostrarUpload = true;
    this.clienteSelecionado = cliente;
    this.clientes = null;
  }

}

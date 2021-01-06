import { ClienteService } from "./../../services/cliente.service";
import { Cliente } from "shared/models/cliente";
import { ToastrService } from "ngx-toastr";
import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: "app-cliente-form",
  templateUrl: "./cliente-form.component.html",
  styleUrls: ["./cliente-form.component.css"],
})
export class ClienteFormComponent {
  idCliente: string;

  cliente: Cliente = {
    idCliente: 0,
    nomeCliente: "",
    arquivos: null,
    arquivosTotal: 0
  };

  constructor(
    private clienteService: ClienteService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.idCliente = this.route.snapshot.paramMap.get("id");

    if (this.idCliente)
      this.clienteService.Get(+this.idCliente).subscribe((data) => {
        this.cliente = data;
      });
  }

  save() {
    if (this.cliente.idCliente != 0)
      this.clienteService
        .Update(this.cliente)
        .subscribe((x) => {
          this.toastr.success("Atualizado com sucesso");
          this.router.navigate(["/cliente"]);
        });
    else
      this.clienteService.Add(this.cliente).subscribe((x) => {
        this.toastr.success("Inserido com sucesso");
        this.router.navigate(["/cliente"]);
      });
  }

  delete() {
    if (!confirm("Tem certeza que deseja excluir este cliente?"))
      return;

    this.clienteService
      .Delete(this.cliente.idCliente)
      .subscribe((x) => {
        this.toastr.success("Removido com sucesso");
        this.router.navigate(["/cliente"]);
      });
  }
}

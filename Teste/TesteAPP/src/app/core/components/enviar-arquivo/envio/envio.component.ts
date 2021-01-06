import { ArquivoService } from "./../../../services/arquivo.service";
import { ToastrService } from "ngx-toastr";
import { HttpClient, HttpEventType, HttpResponse } from "@angular/common/http";
import { Component, Input } from "@angular/core";
import { Cliente } from "shared/models/cliente";
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: "app-envio",
  templateUrl: "./envio.component.html",
  styleUrls: ["./envio.component.css"],
})
export class EnvioComponent {
  @Input("cliente") cliente: Cliente;

  progress: { percentage: string } = { percentage: "0%" };
  selectedFile: File;
  fileName: string;

  constructor(
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private arquivoService: ArquivoService
  ) {}

  onFileSelected(event) {
    this.fileName = "";
    this.progress.percentage = "0%";
    this.selectedFile = event.target.files[0];

    this.onUpload();
  }

  onUpload() {

    this.fileName = this.selectedFile.name;
    this.arquivoService
      .AddFileToCliente(this.cliente.idCliente, this.selectedFile)
      .subscribe(
        (event) => {
          if (event.type === HttpEventType.UploadProgress) {
            this.progress.percentage =
              Math.round((100 * event.loaded) / event.total) + "%";

            if(this.progress.percentage == "100%")
              this.spinner.show();

          } else if (event instanceof HttpResponse) {
            this.spinner.hide();
            this.toastr.success("Arquivo enviado com sucesso!");
          }
        },
        (error) => {
          this.spinner.hide();
          this.toastr.error(
            "Ocorreu um erro no envio do arquivo. Erro: " + error.message
          )}
      );
  }
}

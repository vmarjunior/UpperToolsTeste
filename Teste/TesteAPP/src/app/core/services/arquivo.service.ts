import { Arquivo } from "./../../shared/models/arquivo";
import { HttpClient, HttpEvent, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "environments/environment";
import { UtilService } from "shared/services/util/util.service";
import { Cliente } from "shared/models/cliente";
import { FormControl } from "@angular/forms";

@Injectable({
  providedIn: "root",
})
export class ArquivoService {
  private URL = {
    Get: environment.API + "Arquivo/Get/{0}",
    GetAll: environment.API + "Arquivo/GetAll",
    Add: environment.API + "Arquivo/Add",
    Update: environment.API + "Arquivo/Update",
    Delete: environment.API + "Arquivo/Delete/{0}",
    AddFileToCliente: environment.API + "Arquivo/AddFileToCliente/{0}",
  };

  constructor(private http: HttpClient, private utilService: UtilService) {}

  //Methods
  Get(id: number) {
    return this.http.get<Arquivo>(
      this.utilService.formatString(this.URL.Get, id.toString())
    );
  }

  GetAll() {
    return this.http.get<Arquivo[]>(this.URL.GetAll);
  }

  Add(arquivo: Arquivo) {
    return this.http.post<Arquivo>(this.URL.Add, arquivo);
  }

  Update(arquivo: Arquivo) {
    return this.http.put<Arquivo>(this.URL.Update, arquivo);
  }

  Delete(id: number) {
    return this.http.delete<Arquivo>(
      this.utilService.formatString(this.URL.Delete, id.toString())
    );
  }

  AddFileToCliente(idCliente: number, file: File) {

    let formData = new FormData();
    formData.append("File", file);

    return this.http.post<HttpEvent<object>>(
      this.utilService.formatString(this.URL.AddFileToCliente, idCliente.toString()),
      formData,
      {
        reportProgress: true,
        observe: "events",
      }
    );
  }
}

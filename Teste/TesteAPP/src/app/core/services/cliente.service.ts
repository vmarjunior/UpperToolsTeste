import { Cliente } from 'shared/models/cliente';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from "environments/environment";
import { switchMap } from 'rxjs/operators';
import { UtilService } from 'shared/services/util/util.service';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  private URL = {
    Get: environment.API + 'Cliente/Get/{0}',
    GetAll: environment.API + 'Cliente/GetAll',
    Add: environment.API + 'Cliente/Add',
    Update: environment.API + 'Cliente/Update',
    Delete: environment.API + 'Cliente/Delete/{0}',
    GetAllIncludeFilesByFilter: environment.API + 'Cliente/GetAllIncludeFilesByFilter/{0}'
  };

  constructor(
    private http: HttpClient,
    private utilService: UtilService) { }

  //Methods
  Get(id: number){
    return this.http.get<Cliente>(this.utilService.formatString(this.URL.Get, id.toString()));
  }

  GetAll() {
    return this.http.get<Cliente[]>(this.URL.GetAll);
  }

  Add(cliente: Cliente){
    return this.http.post<Cliente>(this.URL.Add, cliente);
  }

  Update(cliente: Cliente){
    return this.http.put<Cliente>(this.URL.Update, cliente);
  }

  Delete(id: number){
    return this.http.delete<Cliente>(this.utilService.formatString(this.URL.Delete, id.toString()));
  }

  GetAllIncludeFilesByFilter(query: string) {
    return this.http.get<Cliente[]>(this.utilService.formatString(this.URL.GetAllIncludeFilesByFilter, query));
  }
}

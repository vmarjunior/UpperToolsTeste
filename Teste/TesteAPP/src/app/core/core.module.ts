import { ClienteService } from './services/cliente.service';
import { SharedModule } from 'shared/shared.module';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { BsNavbarComponent } from './components/bs-navbar/bs-navbar.component';
import { HomeComponent } from './components/home/home.component';
import { ClienteComponent } from './components/cliente/cliente.component';
import { ClienteFormComponent } from './components/cliente-form/cliente-form.component';
import { EnviarArquivoComponent } from './components/enviar-arquivo/enviar-arquivo.component';
import { EnvioComponent } from './components/enviar-arquivo/envio/envio.component';
import { ArquivoComponent } from './components/arquivo/arquivo.component';

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild([
      { path: '', component: HomeComponent },
      { path: 'cliente', component: ClienteComponent },
      { path: 'cliente/novo', component: ClienteFormComponent },
      { path: 'cliente/editar/:id', component: ClienteFormComponent },
      { path: 'arquivo', component: ArquivoComponent },
      { path: 'enviar-arquivo', component: EnviarArquivoComponent }
    ])
  ],
  declarations: [
    BsNavbarComponent,
    HomeComponent,
    ClienteComponent,
    ClienteFormComponent,
    EnviarArquivoComponent,
    EnvioComponent,
    ArquivoComponent
  ],
  exports: [
    BsNavbarComponent
  ],
  providers: [
    ClienteService
  ]
})
export class CoreModule { }

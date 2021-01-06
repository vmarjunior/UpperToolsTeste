import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';

import { CustomFormsModule } from 'ngx-custom-validators';
import { DataTableModule } from 'angular-6-datatable';
import { UtilService } from './services/util/util.service';

@NgModule({
  imports: [
    CommonModule,
    NgbModule,
    CustomFormsModule,
    DataTableModule,
    FormsModule,
    HttpClientModule,
    RouterModule
  ],
  declarations: [
  ],
  exports: [
    CommonModule,
    NgbModule,
    FormsModule,
    CustomFormsModule,
    DataTableModule,
    RouterModule
  ],
  providers: [
    UtilService
  ],
})
export class SharedModule { }

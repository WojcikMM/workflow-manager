import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SimpleDialogComponent} from './components/dialogs';
import {MatDialogModule} from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';
import {AbilitiesService} from './abilities';
import {ClientsModule} from './clients/clients.module';


@NgModule({
  declarations: [ SimpleDialogComponent ],
  imports: [
    CommonModule,
    ClientsModule,
    MatDialogModule,
    MatButtonModule
  ],
  exports: [
    SimpleDialogComponent,
    ClientsModule
  ],
  providers: [
    AbilitiesService
  ]
})
export class SharedModule {
}

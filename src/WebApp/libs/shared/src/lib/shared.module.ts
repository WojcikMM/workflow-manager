import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimpleDialogComponent } from './components/dialogs';
import { ClientsModule } from './clients/clients.module';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { AbilitiesService } from './abilities';
import { FlexLayoutModule } from '@angular/flex-layout';

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
    ClientsModule,
    FlexLayoutModule
  ],
  providers: [
    AbilitiesService
  ]
})
export class SharedModule {}

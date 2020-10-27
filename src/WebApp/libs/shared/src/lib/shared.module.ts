import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimpleDialogComponent } from './components/dialogs';
import { ClientsModule } from './clients';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';
import { UnauthorizedComponent } from './components/unauthorized';

@NgModule({
  declarations: [
    SimpleDialogComponent,
    UnauthorizedComponent
  ],
  imports: [
    CommonModule,
    ClientsModule,
    MatDialogModule,
    MatButtonModule
  ],
  exports: [
    SimpleDialogComponent,
    UnauthorizedComponent,
    ClientsModule,
    FlexLayoutModule
  ],
  providers: [
    {
      provide: MAT_SNACK_BAR_DEFAULT_OPTIONS,
      useValue: {
        horizontalPosition: 'end',
        verticalPosition: 'bottom',
        duration: 2500,
      }
    }
  ]
})
export class SharedModule {
}

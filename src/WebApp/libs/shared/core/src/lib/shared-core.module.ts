import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimpleDialogComponent, UnauthorizedComponent } from './components';
import { ClientsModule } from './clients';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';

@NgModule({
  declarations: [
    SimpleDialogComponent,
    UnauthorizedComponent
  ],
  imports: [
    CommonModule,
    ClientsModule,
    MatDialogModule,
    MatButtonModule,
    RouterModule
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
export class SharedCoreModule {
}

import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import {SimpleDialogData} from './simple-dialog.data';

@Component({
  templateUrl: './simple-dialog.component.html'
})
export class SimpleDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public model: SimpleDialogData) {
  }
}

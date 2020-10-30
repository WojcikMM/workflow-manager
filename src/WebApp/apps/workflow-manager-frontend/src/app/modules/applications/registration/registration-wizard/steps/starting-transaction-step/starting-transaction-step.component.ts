import {Component, Input, OnInit} from '@angular/core';
import {FormControl} from '@angular/forms';

@Component({
  selector: 'app-starting-transaction-step',
  templateUrl: './starting-transaction-step.component.html',
  styleUrls: ['./starting-transaction-step.component.scss']
})
export class StartingTransactionStepComponent implements OnInit {
  @Input() startingProcessKey: FormControl;

  ngOnInit(): void {
  }

}

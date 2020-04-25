import {Component, Input, OnInit} from '@angular/core';
import {FormControl} from '@angular/forms';
import {TransactionsService} from '../../../../../management/transactions/transactions.service';

@Component({
  selector: 'app-starting-transaction-step',
  templateUrl: './starting-transaction-step.component.html',
  styleUrls: ['./starting-transaction-step.component.scss']
})
export class StartingTransactionStepComponent implements OnInit {
  @Input() startingProcessKey: FormControl;

  constructor(transactionService: TransactionsService) {
  }

  ngOnInit(): void {
  }

}

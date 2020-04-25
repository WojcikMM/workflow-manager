import {Injectable} from '@angular/core';
import {FirebaseCollectionService} from '../firebase-collection.service';
import {TransactionViewModel} from './transaction.view-model';
import {AngularFirestore, CollectionReference} from '@angular/fire/firestore';

@Injectable()
export class TransactionsCollectionService extends FirebaseCollectionService<TransactionViewModel> {

  constructor(firestore: AngularFirestore) {
    super(firestore, 'transactions');
  }

  setStatusKey(statusKey: string) {
    this.subject$.next(statusKey);
  }

  getTransactionsByIncomingStatusKey() {
    return this.getWithQuery((query: CollectionReference, value: any) => query
      .where('incomingStatusKey', '==', value)
      .where('outgoingStatusKey', '==', value)
      .orderBy('name'));
  }
}

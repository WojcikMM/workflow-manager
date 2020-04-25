import {Injectable} from '@angular/core';
import {FirebaseCollectionService} from '../firebase-collection.service';
import {StatusViewModel} from './status.view-model';
import {AngularFirestore} from '@angular/fire/firestore';

@Injectable({
  providedIn: 'root'
})
export class StatusesCollectionService extends FirebaseCollectionService<StatusViewModel> {

  constructor(firestore: AngularFirestore) {
    super(firestore, 'statuses');
  }

  setProcessKey(processKey: string) {
    this.subject$.next(processKey);
  }

  getByProcessId() {
    return this.getWithQuery(this.queryFn);
  }

  private queryFn = (query, value) => {
    return query
      .where('processKey', '==', value)
      .orderBy('name');
  }

}

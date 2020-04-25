import {AngularFirestore, AngularFirestoreCollection, CollectionReference, Query} from '@angular/fire/firestore';
import {BehaviorSubject, Observable} from 'rxjs';
import {FirebaseDocument} from './firebase.document';
import {switchMap} from 'rxjs/operators';


export abstract class FirebaseCollectionService<T extends FirebaseDocument> {
  collection$: Observable<T[]>;
  private readonly collection: AngularFirestoreCollection<unknown>;
  private readonly idFieldKey = '$key';
  protected readonly subject$: BehaviorSubject<any> = new BehaviorSubject(null);

  protected constructor(private _firestoreService: AngularFirestore,
                        private _collectionName: string,
                        orderByField: string = 'name') {
    this.collection = _firestoreService.collection(_collectionName);
    this.collection$ = _firestoreService.collection<T>(_collectionName, q => q.orderBy(orderByField))
      .valueChanges({idField: this.idFieldKey});
  }

  addOrUpdate(model: T) {
    if (!model) {
      throw new Error('Cannot add or update empty model.');
    }
    const {$key, ...document} = model;

    return $key ?
      this._updateDocument($key, document) :
      this._addDocument(document);
  }

  remove($key): Promise<void> {
    return this.collection.doc($key).delete();
  }

  getWithQuery(queryFn: (query: CollectionReference, value: any) => Query) {
    return this.subject$.pipe(
      switchMap(result =>
        this._firestoreService.collection<T>(this._collectionName, query => queryFn(query, result))
          .valueChanges({idField: this.idFieldKey})
      ));
  }

  private _addDocument(document: unknown): Promise<void> {
    return this.collection.add(document)
      .then(_ => {
      });
  }

  private _updateDocument($key: string, document: unknown) {
    return this.collection.doc($key).set(document, {merge: true});
  }
}

import {FirebaseDocument} from '../firebase.document';

export interface TransactionViewModel extends FirebaseDocument {
  name: string;
  isInitial: boolean;
  processKey: string;
  incomingStatusKey: string;
  incomingStatusName: string;
  outgoingStatusKey: string;
  outgoingStatusName: string;
}

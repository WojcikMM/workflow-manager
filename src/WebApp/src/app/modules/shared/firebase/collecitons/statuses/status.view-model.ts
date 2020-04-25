import {FirebaseDocument} from '../firebase.document';

export interface StatusViewModel extends FirebaseDocument {
  name: string;
  description: string;
  processKey: string;
  processName: string;
}

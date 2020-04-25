import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AngularFireModule} from '@angular/fire';
import {environment} from '../../../environments/environment';
import {AngularFirestoreModule} from '@angular/fire/firestore';
import {StatusesCollectionService, TransactionsCollectionService} from './firebase/collecitons';
import {SimpleDialogComponent} from './components/dialogs';
import {MatDialogModule} from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';
import {AbilitiesService} from './abilities';
import {ClientsModule} from './clients/clients.module';


@NgModule({
  declarations: [SimpleDialogComponent],
  imports: [
    CommonModule,
    ClientsModule,
    AngularFireModule.initializeApp(environment.firebase),
    AngularFirestoreModule,
    MatDialogModule,
    MatButtonModule
  ],
  exports: [
    SimpleDialogComponent,
    ClientsModule
  ],
  providers: [
    AbilitiesService,
    StatusesCollectionService,
    TransactionsCollectionService
  ]
})
export class SharedModule {
}

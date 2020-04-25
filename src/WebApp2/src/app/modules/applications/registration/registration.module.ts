import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RegistrationWizardComponent} from './registration-wizard/registration-wizard.component';
import {MatStepperModule} from '@angular/material/stepper';
import {MatButtonModule} from '@angular/material/button';
import {RouterModule} from '@angular/router';
import {ProcessInfoStepComponent} from './registration-wizard/steps/process-info-step/process-info-step.component';
import {ApplicationInfoStepComponent} from './registration-wizard/steps/application-info-step/application-info-step.component';
import {SummaryStepComponent} from './registration-wizard/steps/summary-step/summary-step.component';
import {MatSelectModule} from '@angular/material/select';
import {ReactiveFormsModule} from '@angular/forms';
import {StartingTransactionStepComponent} from './registration-wizard/steps/starting-transaction-step/starting-transaction-step.component';


@NgModule({
  declarations: [
    RegistrationWizardComponent,
    ProcessInfoStepComponent,
    ApplicationInfoStepComponent,
    SummaryStepComponent,
    StartingTransactionStepComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: '', pathMatch: 'full', component: RegistrationWizardComponent},
      {path: '**', redirectTo: ''}
    ]),
    MatStepperModule,
    MatButtonModule,
    MatSelectModule,
    ReactiveFormsModule
  ]
})
export class RegistrationModule {
}

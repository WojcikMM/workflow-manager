import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-registration-wizard',
  templateUrl: './registration-wizard.component.html',
  styleUrls: ['./registration-wizard.component.scss']
})
export class RegistrationWizardComponent implements OnInit {

  firstStepForm: FormControl;

  constructor(private readonly _fb: FormBuilder) {
  }

  ngOnInit(): void {
    this.firstStepForm = this._fb.control('', Validators.required);
  }

}

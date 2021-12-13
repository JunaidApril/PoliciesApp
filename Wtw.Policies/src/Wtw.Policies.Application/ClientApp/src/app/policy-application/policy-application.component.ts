import { Component } from "@angular/core";
import { FormBuilder, FormControl, Validators } from "@angular/forms";
import { Observable } from "rxjs";
import { Gender } from "../core/enums/gender-type";
import { PolicyHttpService } from "../core/http/policy-http.service";
import { ApplicationDto } from "../core/models/application-dto";
import { PolicyDto } from "../core/models/policy-dto";
import { ValidatorMessagesConstants } from "../shared/constants/ValidatorMessagesConstants";

@Component({
  selector: 'wtw-policy-application',
  templateUrl: './policy-application.component.html',
  styleUrls: ['./policy-application.component.scss'],
})
export class PolicyApplicationComponent {

  validatorMessages = ValidatorMessagesConstants;
  policyUuid$: Observable<string>;
  applicationDto: ApplicationDto;
  policyDto$: Observable<PolicyDto[]>;
  applicantName: string;
  applicationAge: number;

  applicationForm = this.formBuilder.group({
    name: [null, Validators.required],
    age: [null, Validators.required],
    gender: '',
  });

  constructor(
    private formBuilder: FormBuilder, private policyHttpService: PolicyHttpService
  ) { }

  onSubmit(): void {
    // Process checkout data here
    this.applicationDto = {
      name: this.applicantName,
      age: this.applicationAge,
      genderType: 0,
    };

    this.policyUuid$ = this.policyHttpService.createPolicy(this.applicationDto);
    this.policyDto$ = this.policyHttpService.getAll();
  }

  get name(): FormControl {
    return this.applicationForm.get('name') as FormControl;
  }

  get age(): FormControl {
    return this.applicationForm.get('age') as FormControl;
  }
}

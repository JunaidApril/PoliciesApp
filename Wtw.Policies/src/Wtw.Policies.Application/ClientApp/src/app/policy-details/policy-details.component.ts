import { Component, OnDestroy, OnInit } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { PolicyHttpService } from "../core/http/policy-http.service";
import { PolicyDto } from "../core/models/policy-dto";

@Component({
  selector: 'wtw-policy-details',
  templateUrl: './policy-details.component.html',
  styleUrls: ['./policy-details.component.scss'],
})
export class PolicyDetailsComponent implements OnInit, OnDestroy {

  policies$: Observable<PolicyDto[]>;
  policies: PolicyDto[];
  private destroy$ = new Subject<void>();

  constructor(
    private policyHttpService: PolicyHttpService
  ) { }

  ngOnInit(): void {
    this.policies$ = this.policyHttpService.getAll();
    this.policies$.subscribe((policies) => {
      this.policies = policies;
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  remove(policy: string): void {
    const response = this.policyHttpService.remove(policy);
  }
}

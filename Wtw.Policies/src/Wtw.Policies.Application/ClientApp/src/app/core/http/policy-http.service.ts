import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { ApplicationDto } from "../models/application-dto";
import { PolicyDto } from "../models/policy-dto";

@Injectable({
  providedIn: 'root',
})
export class PolicyHttpService {
  private readonly url = environment.url;

  constructor(private http: HttpClient) { }

  createPolicy(applicationDto: ApplicationDto): Observable<string> {
    return this.http.post<string>(`${this.url}/Create`, applicationDto);
  }

  getAll(): Observable<PolicyDto[]> {
    return this.http.get<PolicyDto[]>(`${this.url}/GetAll`);
  }

  Update(policyDto: PolicyDto): Observable<boolean> {
    return this.http.post<boolean>(`${this.url}/Update`, policyDto);
  }

  get(policyUuid: string): Observable<PolicyDto> {
    return this.http.put<PolicyDto>(`${this.url}/Get`, policyUuid);
  }

  remove(policyUuid: string): Observable<boolean> {
    return this.http.put<boolean>(`${this.url}/Remove`, policyUuid);
  }

}

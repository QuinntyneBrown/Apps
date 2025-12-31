import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { InsuranceInfo } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InsuranceInfoService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _insuranceInfosSubject = new BehaviorSubject<InsuranceInfo[]>([]);

  public insuranceInfos$ = this._insuranceInfosSubject.asObservable();

  getInsuranceInfos(): Observable<InsuranceInfo[]> {
    return this._http.get<InsuranceInfo[]>(`${this._baseUrl}/api/insuranceinfos`).pipe(
      tap(infos => this._insuranceInfosSubject.next(infos))
    );
  }

  getInsuranceInfo(id: string): Observable<InsuranceInfo> {
    return this._http.get<InsuranceInfo>(`${this._baseUrl}/api/insuranceinfos/${id}`);
  }

  createInsuranceInfo(info: Partial<InsuranceInfo>): Observable<InsuranceInfo> {
    return this._http.post<InsuranceInfo>(`${this._baseUrl}/api/insuranceinfos`, info).pipe(
      tap(() => this.getInsuranceInfos().subscribe())
    );
  }

  updateInsuranceInfo(id: string, info: Partial<InsuranceInfo>): Observable<InsuranceInfo> {
    return this._http.put<InsuranceInfo>(`${this._baseUrl}/api/insuranceinfos/${id}`, info).pipe(
      tap(() => this.getInsuranceInfos().subscribe())
    );
  }

  deleteInsuranceInfo(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/insuranceinfos/${id}`).pipe(
      tap(() => this.getInsuranceInfos().subscribe())
    );
  }
}

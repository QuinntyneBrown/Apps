import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';
import { Household, CreateHouseholdRequest, UpdateHouseholdRequest } from '../models';

@Injectable({ providedIn: 'root' })
export class HouseholdsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.baseUrl;

  getHouseholds(): Observable<Household[]> {
    return this.http.get<Household[]>(`${this.baseUrl}/api/households`);
  }

  getHouseholdById(householdId: string): Observable<Household> {
    return this.http.get<Household>(`${this.baseUrl}/api/households/${householdId}`);
  }

  createHousehold(request: CreateHouseholdRequest): Observable<Household> {
    return this.http.post<Household>(`${this.baseUrl}/api/households`, request);
  }

  updateHousehold(request: UpdateHouseholdRequest): Observable<Household> {
    return this.http.put<Household>(`${this.baseUrl}/api/households/${request.householdId}`, request);
  }

  deleteHousehold(householdId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/households/${householdId}`);
  }
}

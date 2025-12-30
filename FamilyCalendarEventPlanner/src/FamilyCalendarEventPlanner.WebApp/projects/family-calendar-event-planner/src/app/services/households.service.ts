import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Household } from './models';

@Injectable({ providedIn: 'root' })
export class HouseholdsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getHouseholds(): Observable<Household[]> {
    return this.http.get<Household[]>(`${this.baseUrl}/api/households`);
  }

  getHouseholdById(householdId: string): Observable<Household> {
    return this.http.get<Household>(`${this.baseUrl}/api/households/${householdId}`);
  }
}

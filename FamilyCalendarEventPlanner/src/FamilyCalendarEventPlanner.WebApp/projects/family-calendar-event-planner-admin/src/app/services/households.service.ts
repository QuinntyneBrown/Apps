// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';
import { HouseholdDto } from '../models/household-dto';
import { CreateHouseholdCommand } from '../models/create-household-command';

@Injectable({ providedIn: 'root' })
export class HouseholdsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getHouseholds(): Observable<HouseholdDto[]> {
    return this.http.get<HouseholdDto[]>(`${this.baseUrl}/api/households`);
  }

  getHouseholdById(householdId: string): Observable<HouseholdDto> {
    return this.http.get<HouseholdDto>(`${this.baseUrl}/api/households/${householdId}`);
  }

  createHousehold(command: CreateHouseholdCommand): Observable<HouseholdDto> {
    return this.http.post<HouseholdDto>(`${this.baseUrl}/api/households`, command);
  }

  updateHousehold(householdId: string, command: Partial<CreateHouseholdCommand> & { householdId: string }): Observable<HouseholdDto> {
    return this.http.put<HouseholdDto>(`${this.baseUrl}/api/households/${householdId}`, command);
  }

  deleteHousehold(householdId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/households/${householdId}`);
  }
}

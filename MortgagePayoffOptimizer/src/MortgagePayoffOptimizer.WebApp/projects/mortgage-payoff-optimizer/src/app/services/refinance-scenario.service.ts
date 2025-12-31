import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { RefinanceScenario, CreateRefinanceScenario, UpdateRefinanceScenario } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RefinanceScenarioService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/RefinanceScenario`;

  private refinanceScenariosSubject = new BehaviorSubject<RefinanceScenario[]>([]);
  public refinanceScenarios$ = this.refinanceScenariosSubject.asObservable();

  private currentRefinanceScenarioSubject = new BehaviorSubject<RefinanceScenario | null>(null);
  public currentRefinanceScenario$ = this.currentRefinanceScenarioSubject.asObservable();

  getRefinanceScenarios(): Observable<RefinanceScenario[]> {
    return this.http.get<RefinanceScenario[]>(this.baseUrl).pipe(
      tap(scenarios => this.refinanceScenariosSubject.next(scenarios))
    );
  }

  getRefinanceScenarioById(id: string): Observable<RefinanceScenario> {
    return this.http.get<RefinanceScenario>(`${this.baseUrl}/${id}`).pipe(
      tap(scenario => this.currentRefinanceScenarioSubject.next(scenario))
    );
  }

  createRefinanceScenario(scenario: CreateRefinanceScenario): Observable<RefinanceScenario> {
    return this.http.post<RefinanceScenario>(this.baseUrl, scenario).pipe(
      tap(() => this.getRefinanceScenarios().subscribe())
    );
  }

  updateRefinanceScenario(scenario: UpdateRefinanceScenario): Observable<RefinanceScenario> {
    return this.http.put<RefinanceScenario>(`${this.baseUrl}/${scenario.refinanceScenarioId}`, scenario).pipe(
      tap(() => this.getRefinanceScenarios().subscribe())
    );
  }

  deleteRefinanceScenario(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getRefinanceScenarios().subscribe())
    );
  }
}

import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { RetirementScenario, CreateRetirementScenario, UpdateRetirementScenario } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RetirementScenarioService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/retirement-scenarios`;

  private scenariosSubject = new BehaviorSubject<RetirementScenario[]>([]);
  public scenarios$ = this.scenariosSubject.asObservable();

  private selectedScenarioSubject = new BehaviorSubject<RetirementScenario | null>(null);
  public selectedScenario$ = this.selectedScenarioSubject.asObservable();

  loadScenarios(): Observable<RetirementScenario[]> {
    return this.http.get<RetirementScenario[]>(this.baseUrl).pipe(
      tap(scenarios => this.scenariosSubject.next(scenarios))
    );
  }

  getScenario(id: string): Observable<RetirementScenario> {
    return this.http.get<RetirementScenario>(`${this.baseUrl}/${id}`).pipe(
      tap(scenario => this.selectedScenarioSubject.next(scenario))
    );
  }

  createScenario(scenario: CreateRetirementScenario): Observable<RetirementScenario> {
    return this.http.post<RetirementScenario>(this.baseUrl, scenario).pipe(
      tap(newScenario => {
        const current = this.scenariosSubject.value;
        this.scenariosSubject.next([...current, newScenario]);
      })
    );
  }

  updateScenario(scenario: UpdateRetirementScenario): Observable<RetirementScenario> {
    return this.http.put<RetirementScenario>(`${this.baseUrl}/${scenario.retirementScenarioId}`, scenario).pipe(
      tap(updatedScenario => {
        const current = this.scenariosSubject.value;
        const index = current.findIndex(s => s.retirementScenarioId === updatedScenario.retirementScenarioId);
        if (index !== -1) {
          current[index] = updatedScenario;
          this.scenariosSubject.next([...current]);
        }
        this.selectedScenarioSubject.next(updatedScenario);
      })
    );
  }

  deleteScenario(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.scenariosSubject.value;
        this.scenariosSubject.next(current.filter(s => s.retirementScenarioId !== id));
        if (this.selectedScenarioSubject.value?.retirementScenarioId === id) {
          this.selectedScenarioSubject.next(null);
        }
      })
    );
  }
}

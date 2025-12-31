import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { Intake, CreateIntakeCommand, UpdateIntakeCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class IntakeService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.baseUrl}/api/intake`;

  private intakesSubject = new BehaviorSubject<Intake[]>([]);
  public intakes$ = this.intakesSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getIntakes(): Observable<Intake[]> {
    this.loadingSubject.next(true);
    return this.http.get<Intake[]>(this.apiUrl).pipe(
      tap(intakes => {
        this.intakesSubject.next(intakes);
        this.loadingSubject.next(false);
      })
    );
  }

  getIntakeById(id: string): Observable<Intake> {
    return this.http.get<Intake>(`${this.apiUrl}/${id}`);
  }

  createIntake(command: CreateIntakeCommand): Observable<Intake> {
    return this.http.post<Intake>(this.apiUrl, command).pipe(
      tap(intake => {
        const currentIntakes = this.intakesSubject.value;
        this.intakesSubject.next([...currentIntakes, intake]);
      })
    );
  }

  updateIntake(id: string, command: UpdateIntakeCommand): Observable<Intake> {
    return this.http.put<Intake>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedIntake => {
        const currentIntakes = this.intakesSubject.value;
        const index = currentIntakes.findIndex(i => i.intakeId === id);
        if (index !== -1) {
          currentIntakes[index] = updatedIntake;
          this.intakesSubject.next([...currentIntakes]);
        }
      })
    );
  }

  deleteIntake(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentIntakes = this.intakesSubject.value;
        this.intakesSubject.next(currentIntakes.filter(i => i.intakeId !== id));
      })
    );
  }
}

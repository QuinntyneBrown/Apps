import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ValueAssessment } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ValueAssessmentService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _assessmentsSubject = new BehaviorSubject<ValueAssessment[]>([]);

  assessments$ = this._assessmentsSubject.asObservable();

  getValueAssessments(): Observable<ValueAssessment[]> {
    return this._http.get<ValueAssessment[]>(`${this._baseUrl}/api/valueassessments`).pipe(
      tap(assessments => this._assessmentsSubject.next(assessments))
    );
  }

  getValueAssessmentById(id: string): Observable<ValueAssessment> {
    return this._http.get<ValueAssessment>(`${this._baseUrl}/api/valueassessments/${id}`);
  }

  createValueAssessment(assessment: Partial<ValueAssessment>): Observable<ValueAssessment> {
    return this._http.post<ValueAssessment>(`${this._baseUrl}/api/valueassessments`, assessment).pipe(
      tap(() => this.getValueAssessments().subscribe())
    );
  }

  updateValueAssessment(id: string, assessment: Partial<ValueAssessment>): Observable<ValueAssessment> {
    return this._http.put<ValueAssessment>(`${this._baseUrl}/api/valueassessments/${id}`, assessment).pipe(
      tap(() => this.getValueAssessments().subscribe())
    );
  }

  deleteValueAssessment(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/valueassessments/${id}`).pipe(
      tap(() => this.getValueAssessments().subscribe())
    );
  }

  getValueAssessmentsByVehicle(vehicleId: string): Observable<ValueAssessment[]> {
    return this._http.get<ValueAssessment[]>(`${this._baseUrl}/api/valueassessments?vehicleId=${vehicleId}`);
  }
}

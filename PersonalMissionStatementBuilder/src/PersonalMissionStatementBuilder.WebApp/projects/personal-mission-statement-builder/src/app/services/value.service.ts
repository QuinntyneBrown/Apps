import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Value, CreateValue, UpdateValue } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ValueService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/values`;

  private valuesSubject = new BehaviorSubject<Value[]>([]);
  public values$ = this.valuesSubject.asObservable();

  private currentValueSubject = new BehaviorSubject<Value | null>(null);
  public currentValue$ = this.currentValueSubject.asObservable();

  getAll(): Observable<Value[]> {
    return this.http.get<Value[]>(this.baseUrl).pipe(
      tap(values => this.valuesSubject.next(values))
    );
  }

  getById(id: string): Observable<Value> {
    return this.http.get<Value>(`${this.baseUrl}/${id}`).pipe(
      tap(value => this.currentValueSubject.next(value))
    );
  }

  create(value: CreateValue): Observable<Value> {
    return this.http.post<Value>(this.baseUrl, value).pipe(
      tap(newValue => {
        const current = this.valuesSubject.value;
        this.valuesSubject.next([...current, newValue]);
      })
    );
  }

  update(value: UpdateValue): Observable<Value> {
    return this.http.put<Value>(`${this.baseUrl}/${value.valueId}`, value).pipe(
      tap(updated => {
        const current = this.valuesSubject.value;
        const index = current.findIndex(v => v.valueId === updated.valueId);
        if (index !== -1) {
          current[index] = updated;
          this.valuesSubject.next([...current]);
        }
        this.currentValueSubject.next(updated);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.valuesSubject.value;
        this.valuesSubject.next(current.filter(v => v.valueId !== id));
        if (this.currentValueSubject.value?.valueId === id) {
          this.currentValueSubject.next(null);
        }
      })
    );
  }
}

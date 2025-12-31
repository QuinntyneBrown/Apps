import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Trigger } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TriggerService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _triggersSubject = new BehaviorSubject<Trigger[]>([]);

  triggers$ = this._triggersSubject.asObservable();

  getTriggers(): Observable<Trigger[]> {
    return this._http.get<Trigger[]>(`${this._baseUrl}/api/triggers`).pipe(
      tap(triggers => this._triggersSubject.next(triggers))
    );
  }

  getTriggerById(id: string): Observable<Trigger> {
    return this._http.get<Trigger>(`${this._baseUrl}/api/triggers/${id}`);
  }

  createTrigger(trigger: Partial<Trigger>): Observable<Trigger> {
    return this._http.post<Trigger>(`${this._baseUrl}/api/triggers`, trigger).pipe(
      tap(newTrigger => {
        const currentTriggers = this._triggersSubject.value;
        this._triggersSubject.next([...currentTriggers, newTrigger]);
      })
    );
  }

  updateTrigger(id: string, trigger: Partial<Trigger>): Observable<void> {
    return this._http.put<void>(`${this._baseUrl}/api/triggers/${id}`, trigger).pipe(
      tap(() => {
        const currentTriggers = this._triggersSubject.value;
        const index = currentTriggers.findIndex(t => t.triggerId === id);
        if (index !== -1) {
          currentTriggers[index] = { ...currentTriggers[index], ...trigger } as Trigger;
          this._triggersSubject.next([...currentTriggers]);
        }
      })
    );
  }

  deleteTrigger(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/triggers/${id}`).pipe(
      tap(() => {
        const currentTriggers = this._triggersSubject.value;
        this._triggersSubject.next(currentTriggers.filter(t => t.triggerId !== id));
      })
    );
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Modification, CreateModificationCommand, UpdateModificationCommand } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class ModificationsService {
  private readonly apiUrl = `${environment.baseUrl}/api/modifications`;
  private modificationsSubject = new BehaviorSubject<Modification[]>([]);
  public modifications$ = this.modificationsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(): Observable<Modification[]> {
    return this.http.get<Modification[]>(this.apiUrl).pipe(
      tap(modifications => this.modificationsSubject.next(modifications))
    );
  }

  getById(id: string): Observable<Modification> {
    return this.http.get<Modification>(`${this.apiUrl}/${id}`);
  }

  getByCategory(category: string): Observable<Modification[]> {
    return this.http.get<Modification[]>(`${this.apiUrl}/category/${category}`).pipe(
      tap(modifications => this.modificationsSubject.next(modifications))
    );
  }

  create(command: CreateModificationCommand): Observable<Modification> {
    return this.http.post<Modification>(this.apiUrl, command).pipe(
      tap(modification => {
        const current = this.modificationsSubject.value;
        this.modificationsSubject.next([...current, modification]);
      })
    );
  }

  update(id: string, command: UpdateModificationCommand): Observable<Modification> {
    return this.http.put<Modification>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedModification => {
        const current = this.modificationsSubject.value;
        const index = current.findIndex(m => m.modificationId === id);
        if (index !== -1) {
          current[index] = updatedModification;
          this.modificationsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.modificationsSubject.value;
        this.modificationsSubject.next(current.filter(m => m.modificationId !== id));
      })
    );
  }
}

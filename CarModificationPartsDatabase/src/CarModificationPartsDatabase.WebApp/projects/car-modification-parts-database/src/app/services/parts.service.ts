import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Part, CreatePartCommand, UpdatePartCommand } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class PartsService {
  private readonly apiUrl = `${environment.baseUrl}/api/parts`;
  private partsSubject = new BehaviorSubject<Part[]>([]);
  public parts$ = this.partsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(): Observable<Part[]> {
    return this.http.get<Part[]>(this.apiUrl).pipe(
      tap(parts => this.partsSubject.next(parts))
    );
  }

  getById(id: string): Observable<Part> {
    return this.http.get<Part>(`${this.apiUrl}/${id}`);
  }

  getByManufacturer(manufacturer: string): Observable<Part[]> {
    return this.http.get<Part[]>(`${this.apiUrl}/manufacturer/${manufacturer}`).pipe(
      tap(parts => this.partsSubject.next(parts))
    );
  }

  create(command: CreatePartCommand): Observable<Part> {
    return this.http.post<Part>(this.apiUrl, command).pipe(
      tap(part => {
        const current = this.partsSubject.value;
        this.partsSubject.next([...current, part]);
      })
    );
  }

  update(id: string, command: UpdatePartCommand): Observable<Part> {
    return this.http.put<Part>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedPart => {
        const current = this.partsSubject.value;
        const index = current.findIndex(p => p.partId === id);
        if (index !== -1) {
          current[index] = updatedPart;
          this.partsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.partsSubject.value;
        this.partsSubject.next(current.filter(p => p.partId !== id));
      })
    );
  }
}

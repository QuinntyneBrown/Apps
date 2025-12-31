import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Part, CreatePartCommand, UpdatePartCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class PartService {
  private readonly apiUrl = `${environment.baseUrl}/api/Parts`;
  private partsSubject = new BehaviorSubject<Part[]>([]);
  private currentPartSubject = new BehaviorSubject<Part | null>(null);

  public parts$ = this.partsSubject.asObservable();
  public currentPart$ = this.currentPartSubject.asObservable();

  constructor(private http: HttpClient) {}

  getParts(projectId?: string, userId?: string): Observable<Part[]> {
    const params: any = {};
    if (projectId) params.projectId = projectId;
    if (userId) params.userId = userId;

    return this.http.get<Part[]>(this.apiUrl, { params }).pipe(
      tap(parts => this.partsSubject.next(parts))
    );
  }

  getPartById(id: string): Observable<Part> {
    return this.http.get<Part>(`${this.apiUrl}/${id}`).pipe(
      tap(part => this.currentPartSubject.next(part))
    );
  }

  createPart(command: CreatePartCommand): Observable<Part> {
    return this.http.post<Part>(this.apiUrl, command).pipe(
      tap(part => {
        const parts = this.partsSubject.value;
        this.partsSubject.next([...parts, part]);
      })
    );
  }

  updatePart(id: string, command: UpdatePartCommand): Observable<Part> {
    return this.http.put<Part>(`${this.apiUrl}/${id}`, command).pipe(
      tap(part => {
        const parts = this.partsSubject.value;
        const index = parts.findIndex(p => p.partId === id);
        if (index !== -1) {
          parts[index] = part;
          this.partsSubject.next([...parts]);
        }
        this.currentPartSubject.next(part);
      })
    );
  }

  deletePart(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const parts = this.partsSubject.value;
        this.partsSubject.next(parts.filter(p => p.partId !== id));
        if (this.currentPartSubject.value?.partId === id) {
          this.currentPartSubject.next(null);
        }
      })
    );
  }
}

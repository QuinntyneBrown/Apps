import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Technique, CreateTechnique, UpdateTechnique } from '../models';
import { environment } from '../../environments';

@Injectable({
  providedIn: 'root'
})
export class TechniqueService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/techniques`;

  private techniquesSubject = new BehaviorSubject<Technique[]>([]);
  public techniques$ = this.techniquesSubject.asObservable();

  getTechniques(): Observable<Technique[]> {
    return this.http.get<Technique[]>(this.baseUrl).pipe(
      tap(techniques => this.techniquesSubject.next(techniques))
    );
  }

  getTechniqueById(id: string): Observable<Technique> {
    return this.http.get<Technique>(`${this.baseUrl}/${id}`);
  }

  createTechnique(technique: CreateTechnique): Observable<Technique> {
    return this.http.post<Technique>(this.baseUrl, technique).pipe(
      tap(newTechnique => {
        const current = this.techniquesSubject.value;
        this.techniquesSubject.next([...current, newTechnique]);
      })
    );
  }

  updateTechnique(technique: UpdateTechnique): Observable<Technique> {
    return this.http.put<Technique>(`${this.baseUrl}/${technique.techniqueId}`, technique).pipe(
      tap(updated => {
        const current = this.techniquesSubject.value;
        const index = current.findIndex(t => t.techniqueId === updated.techniqueId);
        if (index !== -1) {
          current[index] = updated;
          this.techniquesSubject.next([...current]);
        }
      })
    );
  }

  deleteTechnique(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.techniquesSubject.value;
        this.techniquesSubject.next(current.filter(t => t.techniqueId !== id));
      })
    );
  }
}

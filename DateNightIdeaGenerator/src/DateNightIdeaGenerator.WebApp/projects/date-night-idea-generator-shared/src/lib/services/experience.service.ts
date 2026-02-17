import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { Experience, CreateExperience, UpdateExperience } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ExperienceService {
  private readonly apiUrl = `${environment.baseUrl}/api/Experiences`;
  private experiencesSubject = new BehaviorSubject<Experience[]>([]);
  public experiences$ = this.experiencesSubject.asObservable();

  private selectedExperienceSubject = new BehaviorSubject<Experience | null>(null);
  public selectedExperience$ = this.selectedExperienceSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(dateIdeaId?: string, userId?: string): Observable<Experience[]> {
    let params = new HttpParams();
    if (dateIdeaId) params = params.set('dateIdeaId', dateIdeaId);
    if (userId) params = params.set('userId', userId);

    return this.http.get<Experience[]>(this.apiUrl, { params }).pipe(
      tap(experiences => this.experiencesSubject.next(experiences))
    );
  }

  getById(id: string): Observable<Experience> {
    return this.http.get<Experience>(`${this.apiUrl}/${id}`).pipe(
      tap(experience => this.selectedExperienceSubject.next(experience))
    );
  }

  create(experience: CreateExperience): Observable<Experience> {
    return this.http.post<Experience>(this.apiUrl, experience).pipe(
      tap(newExperience => {
        const current = this.experiencesSubject.value;
        this.experiencesSubject.next([...current, newExperience]);
      })
    );
  }

  update(id: string, experience: UpdateExperience): Observable<Experience> {
    return this.http.put<Experience>(`${this.apiUrl}/${id}`, experience).pipe(
      tap(updatedExperience => {
        const current = this.experiencesSubject.value;
        const index = current.findIndex(e => e.experienceId === id);
        if (index !== -1) {
          current[index] = updatedExperience;
          this.experiencesSubject.next([...current]);
        }
        this.selectedExperienceSubject.next(updatedExperience);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.experiencesSubject.value;
        this.experiencesSubject.next(current.filter(e => e.experienceId !== id));
        if (this.selectedExperienceSubject.value?.experienceId === id) {
          this.selectedExperienceSubject.next(null);
        }
      })
    );
  }
}

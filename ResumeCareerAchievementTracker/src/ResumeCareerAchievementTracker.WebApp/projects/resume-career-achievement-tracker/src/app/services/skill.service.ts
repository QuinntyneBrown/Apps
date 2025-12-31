import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Skill, CreateSkill, UpdateSkill } from '../models';

@Injectable({
  providedIn: 'root'
})
export class SkillService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/skills`;

  private skillsSubject = new BehaviorSubject<Skill[]>([]);
  public skills$ = this.skillsSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getSkills(): Observable<Skill[]> {
    this.loadingSubject.next(true);
    return this.http.get<Skill[]>(this.baseUrl).pipe(
      tap(skills => {
        this.skillsSubject.next(skills);
        this.loadingSubject.next(false);
      })
    );
  }

  getSkillById(id: string): Observable<Skill> {
    return this.http.get<Skill>(`${this.baseUrl}/${id}`);
  }

  createSkill(skill: CreateSkill): Observable<Skill> {
    return this.http.post<Skill>(this.baseUrl, skill).pipe(
      tap(newSkill => {
        const currentSkills = this.skillsSubject.value;
        this.skillsSubject.next([...currentSkills, newSkill]);
      })
    );
  }

  updateSkill(skill: UpdateSkill): Observable<Skill> {
    return this.http.put<Skill>(`${this.baseUrl}/${skill.skillId}`, skill).pipe(
      tap(updatedSkill => {
        const currentSkills = this.skillsSubject.value;
        const index = currentSkills.findIndex(s => s.skillId === updatedSkill.skillId);
        if (index !== -1) {
          currentSkills[index] = updatedSkill;
          this.skillsSubject.next([...currentSkills]);
        }
      })
    );
  }

  deleteSkill(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentSkills = this.skillsSubject.value;
        this.skillsSubject.next(currentSkills.filter(s => s.skillId !== id));
      })
    );
  }

  toggleFeatured(id: string): Observable<Skill> {
    return this.http.patch<Skill>(`${this.baseUrl}/${id}/toggle-featured`, {}).pipe(
      tap(updatedSkill => {
        const currentSkills = this.skillsSubject.value;
        const index = currentSkills.findIndex(s => s.skillId === updatedSkill.skillId);
        if (index !== -1) {
          currentSkills[index] = updatedSkill;
          this.skillsSubject.next([...currentSkills]);
        }
      })
    );
  }
}

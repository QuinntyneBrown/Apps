import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Skill } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SkillService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _skillsSubject = new BehaviorSubject<Skill[]>([]);
  public skills$ = this._skillsSubject.asObservable();

  getSkills(): Observable<Skill[]> {
    return this._http.get<Skill[]>(`${this._baseUrl}/api/skills`).pipe(
      tap(skills => this._skillsSubject.next(skills))
    );
  }

  getSkillById(id: string): Observable<Skill> {
    return this._http.get<Skill>(`${this._baseUrl}/api/skills/${id}`);
  }

  createSkill(skill: Partial<Skill>): Observable<Skill> {
    return this._http.post<Skill>(`${this._baseUrl}/api/skills`, skill).pipe(
      tap(() => this.getSkills().subscribe())
    );
  }

  updateSkill(id: string, skill: Partial<Skill>): Observable<Skill> {
    return this._http.put<Skill>(`${this._baseUrl}/api/skills/${id}`, skill).pipe(
      tap(() => this.getSkills().subscribe())
    );
  }

  deleteSkill(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/skills/${id}`).pipe(
      tap(() => this.getSkills().subscribe())
    );
  }
}

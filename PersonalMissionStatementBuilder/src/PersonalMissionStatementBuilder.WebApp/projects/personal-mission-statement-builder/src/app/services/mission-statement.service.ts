import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { MissionStatement, CreateMissionStatement, UpdateMissionStatement } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MissionStatementService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/mission-statements`;

  private missionStatementsSubject = new BehaviorSubject<MissionStatement[]>([]);
  public missionStatements$ = this.missionStatementsSubject.asObservable();

  private currentMissionStatementSubject = new BehaviorSubject<MissionStatement | null>(null);
  public currentMissionStatement$ = this.currentMissionStatementSubject.asObservable();

  getAll(): Observable<MissionStatement[]> {
    return this.http.get<MissionStatement[]>(this.baseUrl).pipe(
      tap(missionStatements => this.missionStatementsSubject.next(missionStatements))
    );
  }

  getById(id: string): Observable<MissionStatement> {
    return this.http.get<MissionStatement>(`${this.baseUrl}/${id}`).pipe(
      tap(missionStatement => this.currentMissionStatementSubject.next(missionStatement))
    );
  }

  create(missionStatement: CreateMissionStatement): Observable<MissionStatement> {
    return this.http.post<MissionStatement>(this.baseUrl, missionStatement).pipe(
      tap(newMissionStatement => {
        const current = this.missionStatementsSubject.value;
        this.missionStatementsSubject.next([...current, newMissionStatement]);
      })
    );
  }

  update(missionStatement: UpdateMissionStatement): Observable<MissionStatement> {
    return this.http.put<MissionStatement>(
      `${this.baseUrl}/${missionStatement.missionStatementId}`,
      missionStatement
    ).pipe(
      tap(updated => {
        const current = this.missionStatementsSubject.value;
        const index = current.findIndex(m => m.missionStatementId === updated.missionStatementId);
        if (index !== -1) {
          current[index] = updated;
          this.missionStatementsSubject.next([...current]);
        }
        this.currentMissionStatementSubject.next(updated);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.missionStatementsSubject.value;
        this.missionStatementsSubject.next(current.filter(m => m.missionStatementId !== id));
        if (this.currentMissionStatementSubject.value?.missionStatementId === id) {
          this.currentMissionStatementSubject.next(null);
        }
      })
    );
  }
}

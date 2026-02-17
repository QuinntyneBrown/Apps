import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { GearChecklist, CreateGearChecklist, UpdateGearChecklist } from '../models';

@Injectable({
  providedIn: 'root'
})
export class GearChecklistService {
  private baseUrl = `${environment.baseUrl}/api/gearchecklists`;
  private gearChecklistsSubject = new BehaviorSubject<GearChecklist[]>([]);
  public gearChecklists$ = this.gearChecklistsSubject.asObservable();

  private selectedGearChecklistSubject = new BehaviorSubject<GearChecklist | null>(null);
  public selectedGearChecklist$ = this.selectedGearChecklistSubject.asObservable();

  constructor(private http: HttpClient) {}

  getGearChecklists(userId?: string, tripId?: string, isPacked?: boolean): Observable<GearChecklist[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (tripId) params = params.set('tripId', tripId);
    if (isPacked !== undefined) params = params.set('isPacked', isPacked.toString());

    return this.http.get<GearChecklist[]>(this.baseUrl, { params }).pipe(
      tap(gearChecklists => this.gearChecklistsSubject.next(gearChecklists))
    );
  }

  getGearChecklistById(gearChecklistId: string): Observable<GearChecklist> {
    return this.http.get<GearChecklist>(`${this.baseUrl}/${gearChecklistId}`).pipe(
      tap(gearChecklist => this.selectedGearChecklistSubject.next(gearChecklist))
    );
  }

  createGearChecklist(gearChecklist: CreateGearChecklist): Observable<GearChecklist> {
    return this.http.post<GearChecklist>(this.baseUrl, gearChecklist).pipe(
      tap(newGearChecklist => {
        const current = this.gearChecklistsSubject.value;
        this.gearChecklistsSubject.next([...current, newGearChecklist]);
      })
    );
  }

  updateGearChecklist(gearChecklistId: string, gearChecklist: UpdateGearChecklist): Observable<GearChecklist> {
    return this.http.put<GearChecklist>(`${this.baseUrl}/${gearChecklistId}`, gearChecklist).pipe(
      tap(updatedGearChecklist => {
        const current = this.gearChecklistsSubject.value;
        const index = current.findIndex(g => g.gearChecklistId === gearChecklistId);
        if (index !== -1) {
          current[index] = updatedGearChecklist;
          this.gearChecklistsSubject.next([...current]);
        }
      })
    );
  }

  deleteGearChecklist(gearChecklistId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${gearChecklistId}`).pipe(
      tap(() => {
        const current = this.gearChecklistsSubject.value;
        this.gearChecklistsSubject.next(current.filter(g => g.gearChecklistId !== gearChecklistId));
      })
    );
  }

  clearSelectedGearChecklist(): void {
    this.selectedGearChecklistSubject.next(null);
  }
}

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Campsite, CreateCampsite, UpdateCampsite } from '../models';
import { CampsiteType } from '../models';

@Injectable({
  providedIn: 'root'
})
export class CampsiteService {
  private baseUrl = `${environment.baseUrl}/api/campsites`;
  private campsitesSubject = new BehaviorSubject<Campsite[]>([]);
  public campsites$ = this.campsitesSubject.asObservable();

  private selectedCampsiteSubject = new BehaviorSubject<Campsite | null>(null);
  public selectedCampsite$ = this.selectedCampsiteSubject.asObservable();

  constructor(private http: HttpClient) {}

  getCampsites(userId?: string, campsiteType?: CampsiteType, isFavorite?: boolean): Observable<Campsite[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (campsiteType !== undefined) params = params.set('campsiteType', campsiteType.toString());
    if (isFavorite !== undefined) params = params.set('isFavorite', isFavorite.toString());

    return this.http.get<Campsite[]>(this.baseUrl, { params }).pipe(
      tap(campsites => this.campsitesSubject.next(campsites))
    );
  }

  getCampsiteById(campsiteId: string): Observable<Campsite> {
    return this.http.get<Campsite>(`${this.baseUrl}/${campsiteId}`).pipe(
      tap(campsite => this.selectedCampsiteSubject.next(campsite))
    );
  }

  createCampsite(campsite: CreateCampsite): Observable<Campsite> {
    return this.http.post<Campsite>(this.baseUrl, campsite).pipe(
      tap(newCampsite => {
        const current = this.campsitesSubject.value;
        this.campsitesSubject.next([...current, newCampsite]);
      })
    );
  }

  updateCampsite(campsiteId: string, campsite: UpdateCampsite): Observable<Campsite> {
    return this.http.put<Campsite>(`${this.baseUrl}/${campsiteId}`, campsite).pipe(
      tap(updatedCampsite => {
        const current = this.campsitesSubject.value;
        const index = current.findIndex(c => c.campsiteId === campsiteId);
        if (index !== -1) {
          current[index] = updatedCampsite;
          this.campsitesSubject.next([...current]);
        }
      })
    );
  }

  deleteCampsite(campsiteId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${campsiteId}`).pipe(
      tap(() => {
        const current = this.campsitesSubject.value;
        this.campsitesSubject.next(current.filter(c => c.campsiteId !== campsiteId));
      })
    );
  }

  clearSelectedCampsite(): void {
    this.selectedCampsiteSubject.next(null);
  }
}

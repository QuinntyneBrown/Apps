import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Gear, CreateGear, UpdateGear } from '../models';

@Injectable({
  providedIn: 'root'
})
export class GearService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/gears`;

  private gearsSubject = new BehaviorSubject<Gear[]>([]);
  public gears$ = this.gearsSubject.asObservable();

  getAll(): Observable<Gear[]> {
    return this.http.get<Gear[]>(this.baseUrl).pipe(
      tap(gears => this.gearsSubject.next(gears))
    );
  }

  getById(id: string): Observable<Gear> {
    return this.http.get<Gear>(`${this.baseUrl}/${id}`);
  }

  create(gear: CreateGear): Observable<Gear> {
    return this.http.post<Gear>(this.baseUrl, gear).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(gear: UpdateGear): Observable<Gear> {
    return this.http.put<Gear>(`${this.baseUrl}/${gear.gearId}`, gear).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}

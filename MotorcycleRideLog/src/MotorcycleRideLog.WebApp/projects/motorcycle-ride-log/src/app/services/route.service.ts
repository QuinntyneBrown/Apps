import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Route, CreateRoute, UpdateRoute } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class RouteService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.baseUrl}/api/routes`;

  private routesSubject = new BehaviorSubject<Route[]>([]);
  public routes$ = this.routesSubject.asObservable();

  getAll(userId?: string): Observable<Route[]> {
    const url = userId ? `${this.apiUrl}?userId=${userId}` : this.apiUrl;
    return this.http.get<Route[]>(url).pipe(
      tap(routes => this.routesSubject.next(routes))
    );
  }

  getById(id: string): Observable<Route> {
    return this.http.get<Route>(`${this.apiUrl}/${id}`);
  }

  create(route: CreateRoute): Observable<Route> {
    return this.http.post<Route>(this.apiUrl, route).pipe(
      tap(() => this.refreshRoutes())
    );
  }

  update(id: string, route: UpdateRoute): Observable<Route> {
    return this.http.put<Route>(`${this.apiUrl}/${id}`, route).pipe(
      tap(() => this.refreshRoutes())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshRoutes())
    );
  }

  private refreshRoutes(): void {
    this.getAll().subscribe();
  }
}

import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Destination, DestinationType } from '../models';

@Injectable({
  providedIn: 'root'
})
export class DestinationService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/destinations`;

  private destinationsSubject = new BehaviorSubject<Destination[]>([]);
  public destinations$ = this.destinationsSubject.asObservable();

  getDestinations(userId?: string, destinationType?: DestinationType, isVisited?: boolean, country?: string): Observable<Destination[]> {
    let url = this.baseUrl;
    const params: string[] = [];

    if (userId) params.push(`userId=${userId}`);
    if (destinationType) params.push(`destinationType=${destinationType}`);
    if (isVisited !== undefined) params.push(`isVisited=${isVisited}`);
    if (country) params.push(`country=${country}`);

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http.get<Destination[]>(url).pipe(
      tap(destinations => this.destinationsSubject.next(destinations))
    );
  }

  getDestinationById(destinationId: string): Observable<Destination> {
    return this.http.get<Destination>(`${this.baseUrl}/${destinationId}`);
  }

  createDestination(destination: Partial<Destination>): Observable<Destination> {
    return this.http.post<Destination>(this.baseUrl, destination).pipe(
      tap(() => this.refreshDestinations(destination.userId))
    );
  }

  updateDestination(destinationId: string, destination: Partial<Destination>): Observable<Destination> {
    return this.http.put<Destination>(`${this.baseUrl}/${destinationId}`, {
      ...destination,
      destinationId
    }).pipe(
      tap(() => this.refreshDestinations(destination.userId))
    );
  }

  deleteDestination(destinationId: string, userId?: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${destinationId}`).pipe(
      tap(() => this.refreshDestinations(userId))
    );
  }

  markDestinationVisited(destinationId: string, visitedDate: string, userId?: string): Observable<Destination> {
    return this.http.put<Destination>(`${this.baseUrl}/${destinationId}/visited`, {
      destinationId,
      visitedDate
    }).pipe(
      tap(() => this.refreshDestinations(userId))
    );
  }

  private refreshDestinations(userId?: string): void {
    if (userId) {
      this.getDestinations(userId).subscribe();
    }
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Donation, CreateDonationCommand, UpdateDonationCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class DonationService {
  private readonly apiUrl = `${environment.baseUrl}/api/Donations`;
  private donationsSubject = new BehaviorSubject<Donation[]>([]);
  public donations$ = this.donationsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(): Observable<Donation[]> {
    return this.http.get<Donation[]>(this.apiUrl).pipe(
      tap(donations => this.donationsSubject.next(donations))
    );
  }

  getById(id: string): Observable<Donation> {
    return this.http.get<Donation>(`${this.apiUrl}/${id}`);
  }

  getByOrganization(organizationId: string): Observable<Donation[]> {
    return this.http.get<Donation[]>(`${this.apiUrl}/organization/${organizationId}`);
  }

  create(command: CreateDonationCommand): Observable<Donation> {
    return this.http.post<Donation>(this.apiUrl, command).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, command: UpdateDonationCommand): Observable<Donation> {
    return this.http.put<Donation>(`${this.apiUrl}/${id}`, command).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}

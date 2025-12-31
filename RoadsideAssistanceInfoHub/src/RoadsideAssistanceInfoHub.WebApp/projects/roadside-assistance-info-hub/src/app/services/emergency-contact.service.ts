import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { EmergencyContact } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmergencyContactService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _emergencyContactsSubject = new BehaviorSubject<EmergencyContact[]>([]);

  public emergencyContacts$ = this._emergencyContactsSubject.asObservable();

  getEmergencyContacts(): Observable<EmergencyContact[]> {
    return this._http.get<EmergencyContact[]>(`${this._baseUrl}/api/emergencycontacts`).pipe(
      tap(contacts => this._emergencyContactsSubject.next(contacts))
    );
  }

  getEmergencyContact(id: string): Observable<EmergencyContact> {
    return this._http.get<EmergencyContact>(`${this._baseUrl}/api/emergencycontacts/${id}`);
  }

  createEmergencyContact(contact: Partial<EmergencyContact>): Observable<EmergencyContact> {
    return this._http.post<EmergencyContact>(`${this._baseUrl}/api/emergencycontacts`, contact).pipe(
      tap(() => this.getEmergencyContacts().subscribe())
    );
  }

  updateEmergencyContact(id: string, contact: Partial<EmergencyContact>): Observable<EmergencyContact> {
    return this._http.put<EmergencyContact>(`${this._baseUrl}/api/emergencycontacts/${id}`, contact).pipe(
      tap(() => this.getEmergencyContacts().subscribe())
    );
  }

  deleteEmergencyContact(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/emergencycontacts/${id}`).pipe(
      tap(() => this.getEmergencyContacts().subscribe())
    );
  }
}

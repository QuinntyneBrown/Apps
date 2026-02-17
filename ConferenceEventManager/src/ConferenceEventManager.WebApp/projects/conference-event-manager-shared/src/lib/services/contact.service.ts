import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Contact, CreateContactCommand, UpdateContactCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private readonly apiUrl = `${environment.baseUrl}/api/contacts`;
  private contactsSubject = new BehaviorSubject<Contact[]>([]);
  public contacts$ = this.contactsSubject.asObservable();

  private currentContactSubject = new BehaviorSubject<Contact | null>(null);
  public currentContact$ = this.currentContactSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(eventId?: string, userId?: string): Observable<Contact[]> {
    let url = this.apiUrl;
    const params: string[] = [];
    if (eventId) params.push(`eventId=${eventId}`);
    if (userId) params.push(`userId=${userId}`);
    if (params.length > 0) url += '?' + params.join('&');

    return this.http.get<Contact[]>(url).pipe(
      tap(contacts => this.contactsSubject.next(contacts))
    );
  }

  getById(id: string): Observable<Contact> {
    return this.http.get<Contact>(`${this.apiUrl}/${id}`).pipe(
      tap(contact => this.currentContactSubject.next(contact))
    );
  }

  create(command: CreateContactCommand): Observable<Contact> {
    return this.http.post<Contact>(this.apiUrl, command).pipe(
      tap(contact => {
        const current = this.contactsSubject.value;
        this.contactsSubject.next([...current, contact]);
      })
    );
  }

  update(id: string, command: UpdateContactCommand): Observable<Contact> {
    return this.http.put<Contact>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedContact => {
        const current = this.contactsSubject.value;
        const index = current.findIndex(c => c.contactId === id);
        if (index !== -1) {
          current[index] = updatedContact;
          this.contactsSubject.next([...current]);
        }
        this.currentContactSubject.next(updatedContact);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.contactsSubject.value;
        this.contactsSubject.next(current.filter(c => c.contactId !== id));
        if (this.currentContactSubject.value?.contactId === id) {
          this.currentContactSubject.next(null);
        }
      })
    );
  }
}

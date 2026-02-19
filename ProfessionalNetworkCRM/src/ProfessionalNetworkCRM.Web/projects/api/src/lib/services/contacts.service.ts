import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Contact, CreateContactRequest, UpdateContactRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ContactsService {
  private http = inject(HttpClient);
  private baseUrl = environment.baseUrl + '/api';

  private contactsSubject = new BehaviorSubject<Contact[]>([]);
  public contacts$ = this.contactsSubject.asObservable();

  private selectedContactSubject = new BehaviorSubject<Contact | null>(null);
  public selectedContact$ = this.selectedContactSubject.asObservable();

  loadContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(`${this.baseUrl}/contacts`).pipe(
      tap(contacts => this.contactsSubject.next(contacts))
    );
  }

  getContactById(id: string): Observable<Contact> {
    return this.http.get<Contact>(`${this.baseUrl}/contacts/${id}`).pipe(
      tap(contact => this.selectedContactSubject.next(contact))
    );
  }

  createContact(request: CreateContactRequest): Observable<Contact> {
    return this.http.post<Contact>(`${this.baseUrl}/contacts`, request).pipe(
      tap(contact => {
        const currentContacts = this.contactsSubject.value;
        this.contactsSubject.next([...currentContacts, contact]);
      })
    );
  }

  updateContact(request: UpdateContactRequest): Observable<Contact> {
    return this.http.put<Contact>(`${this.baseUrl}/contacts/${request.contactId}`, request).pipe(
      tap(updatedContact => {
        const currentContacts = this.contactsSubject.value;
        const index = currentContacts.findIndex(c => c.contactId === updatedContact.contactId);
        if (index !== -1) {
          currentContacts[index] = updatedContact;
          this.contactsSubject.next([...currentContacts]);
        }
      })
    );
  }

  deleteContact(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/contacts/${id}`).pipe(
      tap(() => {
        const currentContacts = this.contactsSubject.value;
        this.contactsSubject.next(currentContacts.filter(c => c.contactId !== id));
      })
    );
  }

  clearSelectedContact(): void {
    this.selectedContactSubject.next(null);
  }
}

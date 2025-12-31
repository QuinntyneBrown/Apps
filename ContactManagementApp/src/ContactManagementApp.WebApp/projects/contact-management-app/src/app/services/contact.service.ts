import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Contact, CreateContactRequest, UpdateContactRequest, ContactType } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/api/contacts`;

  private contactsSubject = new BehaviorSubject<Contact[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  private selectedContactSubject = new BehaviorSubject<Contact | null>(null);

  contacts$ = this.contactsSubject.asObservable();
  loading$ = this.loadingSubject.asObservable();
  selectedContact$ = this.selectedContactSubject.asObservable();

  getContacts(
    userId?: string,
    contactType?: ContactType,
    isPriority?: boolean,
    tag?: string
  ): Observable<Contact[]> {
    this.loadingSubject.next(true);

    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (contactType !== undefined) params = params.set('contactType', contactType.toString());
    if (isPriority !== undefined) params = params.set('isPriority', isPriority.toString());
    if (tag) params = params.set('tag', tag);

    return this.http.get<Contact[]>(this.baseUrl, { params }).pipe(
      tap(contacts => {
        this.contactsSubject.next(contacts);
        this.loadingSubject.next(false);
      })
    );
  }

  getContactById(contactId: string): Observable<Contact> {
    this.loadingSubject.next(true);

    return this.http.get<Contact>(`${this.baseUrl}/${contactId}`).pipe(
      tap(contact => {
        this.selectedContactSubject.next(contact);
        this.loadingSubject.next(false);
      })
    );
  }

  createContact(request: CreateContactRequest): Observable<Contact> {
    this.loadingSubject.next(true);

    return this.http.post<Contact>(this.baseUrl, request).pipe(
      tap(contact => {
        const currentContacts = this.contactsSubject.value;
        this.contactsSubject.next([contact, ...currentContacts]);
        this.loadingSubject.next(false);
      })
    );
  }

  updateContact(contactId: string, request: UpdateContactRequest): Observable<Contact> {
    this.loadingSubject.next(true);

    return this.http.put<Contact>(`${this.baseUrl}/${contactId}`, request).pipe(
      tap(updatedContact => {
        const currentContacts = this.contactsSubject.value;
        const index = currentContacts.findIndex(c => c.contactId === contactId);
        if (index !== -1) {
          currentContacts[index] = updatedContact;
          this.contactsSubject.next([...currentContacts]);
        }
        if (this.selectedContactSubject.value?.contactId === contactId) {
          this.selectedContactSubject.next(updatedContact);
        }
        this.loadingSubject.next(false);
      })
    );
  }

  deleteContact(contactId: string): Observable<void> {
    this.loadingSubject.next(true);

    return this.http.delete<void>(`${this.baseUrl}/${contactId}`).pipe(
      tap(() => {
        const currentContacts = this.contactsSubject.value;
        this.contactsSubject.next(currentContacts.filter(c => c.contactId !== contactId));
        if (this.selectedContactSubject.value?.contactId === contactId) {
          this.selectedContactSubject.next(null);
        }
        this.loadingSubject.next(false);
      })
    );
  }

  setSelectedContact(contact: Contact | null): void {
    this.selectedContactSubject.next(contact);
  }
}

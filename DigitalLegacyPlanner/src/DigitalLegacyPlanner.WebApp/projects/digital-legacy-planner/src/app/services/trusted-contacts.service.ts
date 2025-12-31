import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { TrustedContact, CreateTrustedContactCommand, UpdateTrustedContactCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TrustedContactsService {
  private readonly baseUrl = `${environment.baseUrl}/api/TrustedContacts`;
  private contactsSubject = new BehaviorSubject<TrustedContact[]>([]);
  public contacts$ = this.contactsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(userId?: string): Observable<TrustedContact[]> {
    let url = this.baseUrl;

    if (userId) {
      url += `?userId=${userId}`;
    }

    return this.http.get<TrustedContact[]>(url).pipe(
      tap(contacts => this.contactsSubject.next(contacts))
    );
  }

  getById(id: string): Observable<TrustedContact> {
    return this.http.get<TrustedContact>(`${this.baseUrl}/${id}`);
  }

  create(command: CreateTrustedContactCommand): Observable<TrustedContact> {
    return this.http.post<TrustedContact>(this.baseUrl, command).pipe(
      tap(contact => {
        const current = this.contactsSubject.value;
        this.contactsSubject.next([...current, contact]);
      })
    );
  }

  update(id: string, command: UpdateTrustedContactCommand): Observable<TrustedContact> {
    return this.http.put<TrustedContact>(`${this.baseUrl}/${id}`, command).pipe(
      tap(updated => {
        const current = this.contactsSubject.value;
        const index = current.findIndex(c => c.trustedContactId === id);
        if (index !== -1) {
          current[index] = updated;
          this.contactsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.contactsSubject.value;
        this.contactsSubject.next(current.filter(c => c.trustedContactId !== id));
      })
    );
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { Client, CreateClientRequest, UpdateClientRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  private readonly apiUrl = `${environment.baseUrl}/api/Clients`;
  private clientsSubject = new BehaviorSubject<Client[]>([]);
  public clients$ = this.clientsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getClients(userId: string): Observable<Client[]> {
    return this.http.get<Client[]>(`${this.apiUrl}?userId=${userId}`).pipe(
      tap(clients => this.clientsSubject.next(clients))
    );
  }

  getClientById(id: string, userId: string): Observable<Client> {
    return this.http.get<Client>(`${this.apiUrl}/${id}?userId=${userId}`);
  }

  createClient(request: CreateClientRequest): Observable<Client> {
    return this.http.post<Client>(this.apiUrl, request).pipe(
      tap(client => {
        const current = this.clientsSubject.value;
        this.clientsSubject.next([...current, client]);
      })
    );
  }

  updateClient(id: string, request: UpdateClientRequest): Observable<Client> {
    return this.http.put<Client>(`${this.apiUrl}/${id}`, request).pipe(
      tap(updatedClient => {
        const current = this.clientsSubject.value;
        const index = current.findIndex(c => c.clientId === id);
        if (index !== -1) {
          current[index] = updatedClient;
          this.clientsSubject.next([...current]);
        }
      })
    );
  }

  deleteClient(id: string, userId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}?userId=${userId}`).pipe(
      tap(() => {
        const current = this.clientsSubject.value;
        this.clientsSubject.next(current.filter(c => c.clientId !== id));
      })
    );
  }
}

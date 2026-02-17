import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Person, CreatePersonRequest, UpdatePersonRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  private readonly apiUrl = `${environment.baseUrl}/api/persons`;
  private personsSubject = new BehaviorSubject<Person[]>([]);
  public persons$ = this.personsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getPersons(userId?: string): Observable<Person[]> {
    const params = userId ? { userId } : {};
    return this.http.get<Person[]>(this.apiUrl, { params }).pipe(
      tap(persons => this.personsSubject.next(persons))
    );
  }

  getPersonById(id: string): Observable<Person> {
    return this.http.get<Person>(`${this.apiUrl}/${id}`);
  }

  createPerson(request: CreatePersonRequest): Observable<Person> {
    return this.http.post<Person>(this.apiUrl, request).pipe(
      tap(() => this.refreshPersons())
    );
  }

  updatePerson(id: string, request: UpdatePersonRequest): Observable<Person> {
    return this.http.put<Person>(`${this.apiUrl}/${id}`, request).pipe(
      tap(() => this.refreshPersons())
    );
  }

  deletePerson(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshPersons())
    );
  }

  private refreshPersons(): void {
    this.getPersons().subscribe();
  }
}

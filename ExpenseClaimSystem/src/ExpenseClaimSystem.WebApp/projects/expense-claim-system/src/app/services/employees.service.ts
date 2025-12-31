import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Employee } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class EmployeesService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = `${environment.baseUrl}/api/employees`;
  private readonly _employeesSubject = new BehaviorSubject<Employee[]>([]);

  public readonly employees$ = this._employeesSubject.asObservable();

  public getAll(): Observable<Employee[]> {
    return this._http.get<Employee[]>(this._baseUrl).pipe(
      tap((employees) => this._employeesSubject.next(employees))
    );
  }

  public getById(id: string): Observable<Employee> {
    return this._http.get<Employee>(`${this._baseUrl}/${id}`);
  }

  public create(employee: Partial<Employee>): Observable<Employee> {
    return this._http.post<Employee>(this._baseUrl, employee).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public update(id: string, employee: Partial<Employee>): Observable<Employee> {
    return this._http.put<Employee>(`${this._baseUrl}/${id}`, { ...employee, employeeId: id }).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}

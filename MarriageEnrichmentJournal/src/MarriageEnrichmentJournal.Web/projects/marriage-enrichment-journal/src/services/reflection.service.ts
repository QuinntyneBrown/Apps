import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Reflection, CreateReflection, UpdateReflection } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ReflectionService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/reflections`;

  private reflectionsSubject = new BehaviorSubject<Reflection[]>([]);
  public reflections$ = this.reflectionsSubject.asObservable();

  private selectedReflectionSubject = new BehaviorSubject<Reflection | null>(null);
  public selectedReflection$ = this.selectedReflectionSubject.asObservable();

  getAll(): Observable<Reflection[]> {
    return this.http.get<Reflection[]>(this.baseUrl).pipe(
      tap(reflections => this.reflectionsSubject.next(reflections))
    );
  }

  getById(id: string): Observable<Reflection> {
    return this.http.get<Reflection>(`${this.baseUrl}/${id}`).pipe(
      tap(reflection => this.selectedReflectionSubject.next(reflection))
    );
  }

  create(reflection: CreateReflection): Observable<Reflection> {
    return this.http.post<Reflection>(this.baseUrl, reflection).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(reflection: UpdateReflection): Observable<Reflection> {
    return this.http.put<Reflection>(`${this.baseUrl}/${reflection.reflectionId}`, reflection).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  clearSelected(): void {
    this.selectedReflectionSubject.next(null);
  }
}

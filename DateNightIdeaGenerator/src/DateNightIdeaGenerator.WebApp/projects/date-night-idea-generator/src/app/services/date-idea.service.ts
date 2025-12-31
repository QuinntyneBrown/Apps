import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { DateIdea, CreateDateIdea, UpdateDateIdea, Category, BudgetRange } from '../models';

@Injectable({
  providedIn: 'root'
})
export class DateIdeaService {
  private readonly apiUrl = `${environment.baseUrl}/api/DateIdeas`;
  private dateIdeasSubject = new BehaviorSubject<DateIdea[]>([]);
  public dateIdeas$ = this.dateIdeasSubject.asObservable();

  private selectedDateIdeaSubject = new BehaviorSubject<DateIdea | null>(null);
  public selectedDateIdea$ = this.selectedDateIdeaSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(userId?: string, category?: Category, budgetRange?: BudgetRange, favoritesOnly?: boolean): Observable<DateIdea[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (category !== undefined) params = params.set('category', category.toString());
    if (budgetRange !== undefined) params = params.set('budgetRange', budgetRange.toString());
    if (favoritesOnly !== undefined) params = params.set('favoritesOnly', favoritesOnly.toString());

    return this.http.get<DateIdea[]>(this.apiUrl, { params }).pipe(
      tap(dateIdeas => this.dateIdeasSubject.next(dateIdeas))
    );
  }

  getById(id: string): Observable<DateIdea> {
    return this.http.get<DateIdea>(`${this.apiUrl}/${id}`).pipe(
      tap(dateIdea => this.selectedDateIdeaSubject.next(dateIdea))
    );
  }

  create(dateIdea: CreateDateIdea): Observable<DateIdea> {
    return this.http.post<DateIdea>(this.apiUrl, dateIdea).pipe(
      tap(newDateIdea => {
        const current = this.dateIdeasSubject.value;
        this.dateIdeasSubject.next([...current, newDateIdea]);
      })
    );
  }

  update(id: string, dateIdea: UpdateDateIdea): Observable<DateIdea> {
    return this.http.put<DateIdea>(`${this.apiUrl}/${id}`, dateIdea).pipe(
      tap(updatedDateIdea => {
        const current = this.dateIdeasSubject.value;
        const index = current.findIndex(d => d.dateIdeaId === id);
        if (index !== -1) {
          current[index] = updatedDateIdea;
          this.dateIdeasSubject.next([...current]);
        }
        this.selectedDateIdeaSubject.next(updatedDateIdea);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.dateIdeasSubject.value;
        this.dateIdeasSubject.next(current.filter(d => d.dateIdeaId !== id));
        if (this.selectedDateIdeaSubject.value?.dateIdeaId === id) {
          this.selectedDateIdeaSubject.next(null);
        }
      })
    );
  }
}

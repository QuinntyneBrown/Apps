import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Prompt, CreatePromptRequest, UpdatePromptRequest, Category, Depth } from '../models';

@Injectable({
  providedIn: 'root'
})
export class PromptService {
  private readonly apiUrl = `${environment.baseUrl}/api/prompts`;
  private promptsSubject = new BehaviorSubject<Prompt[]>([]);
  public prompts$ = this.promptsSubject.asObservable();

  private currentPromptSubject = new BehaviorSubject<Prompt | null>(null);
  public currentPrompt$ = this.currentPromptSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(userId?: string, category?: Category, depth?: Depth): Observable<Prompt[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (category !== undefined) params = params.set('category', category.toString());
    if (depth !== undefined) params = params.set('depth', depth.toString());

    return this.http.get<Prompt[]>(this.apiUrl, { params }).pipe(
      tap(prompts => this.promptsSubject.next(prompts))
    );
  }

  getById(id: string): Observable<Prompt> {
    return this.http.get<Prompt>(`${this.apiUrl}/${id}`).pipe(
      tap(prompt => this.currentPromptSubject.next(prompt))
    );
  }

  getRandom(category?: Category, depth?: Depth): Observable<Prompt> {
    let params = new HttpParams();
    if (category !== undefined) params = params.set('category', category.toString());
    if (depth !== undefined) params = params.set('depth', depth.toString());

    return this.http.get<Prompt>(`${this.apiUrl}/random`, { params }).pipe(
      tap(prompt => this.currentPromptSubject.next(prompt))
    );
  }

  create(request: CreatePromptRequest): Observable<Prompt> {
    return this.http.post<Prompt>(this.apiUrl, request).pipe(
      tap(prompt => {
        const current = this.promptsSubject.value;
        this.promptsSubject.next([...current, prompt]);
      })
    );
  }

  update(id: string, request: UpdatePromptRequest): Observable<Prompt> {
    return this.http.put<Prompt>(`${this.apiUrl}/${id}`, request).pipe(
      tap(updated => {
        const current = this.promptsSubject.value;
        const index = current.findIndex(p => p.promptId === id);
        if (index !== -1) {
          const newPrompts = [...current];
          newPrompts[index] = updated;
          this.promptsSubject.next(newPrompts);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.promptsSubject.value;
        this.promptsSubject.next(current.filter(p => p.promptId !== id));
      })
    );
  }

  incrementUsage(id: string): Observable<Prompt> {
    return this.http.post<Prompt>(`${this.apiUrl}/${id}/use`, {}).pipe(
      tap(updated => {
        const current = this.promptsSubject.value;
        const index = current.findIndex(p => p.promptId === id);
        if (index !== -1) {
          const newPrompts = [...current];
          newPrompts[index] = updated;
          this.promptsSubject.next(newPrompts);
        }
      })
    );
  }
}

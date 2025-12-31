import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Prompt, CreatePrompt } from '../models';

@Injectable({
  providedIn: 'root'
})
export class PromptService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/prompts`;

  private readonly promptsSubject = new BehaviorSubject<Prompt[]>([]);
  public readonly prompts$ = this.promptsSubject.asObservable();

  private readonly currentPromptSubject = new BehaviorSubject<Prompt | null>(null);
  public readonly currentPrompt$ = this.currentPromptSubject.asObservable();

  getAll(
    category?: string,
    systemPromptsOnly?: boolean,
    userId?: string
  ): Observable<Prompt[]> {
    let params = new HttpParams();
    if (category) params = params.set('category', category);
    if (systemPromptsOnly !== undefined) params = params.set('systemPromptsOnly', systemPromptsOnly.toString());
    if (userId) params = params.set('userId', userId);

    return this.http.get<Prompt[]>(this.baseUrl, { params }).pipe(
      tap(prompts => this.promptsSubject.next(prompts))
    );
  }

  getById(id: string): Observable<Prompt> {
    return this.http.get<Prompt>(`${this.baseUrl}/${id}`).pipe(
      tap(prompt => this.currentPromptSubject.next(prompt))
    );
  }

  create(prompt: CreatePrompt): Observable<Prompt> {
    return this.http.post<Prompt>(this.baseUrl, prompt).pipe(
      tap(newPrompt => {
        const current = this.promptsSubject.value;
        this.promptsSubject.next([newPrompt, ...current]);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.promptsSubject.value;
        this.promptsSubject.next(current.filter(p => p.promptId !== id));
        if (this.currentPromptSubject.value?.promptId === id) {
          this.currentPromptSubject.next(null);
        }
      })
    );
  }
}

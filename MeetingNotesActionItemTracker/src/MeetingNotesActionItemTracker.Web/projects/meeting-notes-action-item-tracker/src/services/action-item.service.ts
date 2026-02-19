import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { ActionItem, CreateActionItemDto, UpdateActionItemDto } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ActionItemService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/actionitems`;

  private actionItemsSubject = new BehaviorSubject<ActionItem[]>([]);
  public actionItems$ = this.actionItemsSubject.asObservable();

  getActionItems(): Observable<ActionItem[]> {
    return this.http.get<ActionItem[]>(this.baseUrl).pipe(
      tap(actionItems => this.actionItemsSubject.next(actionItems))
    );
  }

  getActionItemById(id: string): Observable<ActionItem> {
    return this.http.get<ActionItem>(`${this.baseUrl}/${id}`);
  }

  createActionItem(dto: CreateActionItemDto): Observable<ActionItem> {
    return this.http.post<ActionItem>(this.baseUrl, dto).pipe(
      tap(actionItem => {
        const actionItems = [...this.actionItemsSubject.value, actionItem];
        this.actionItemsSubject.next(actionItems);
      })
    );
  }

  updateActionItem(dto: UpdateActionItemDto): Observable<ActionItem> {
    return this.http.put<ActionItem>(`${this.baseUrl}/${dto.actionItemId}`, dto).pipe(
      tap(actionItem => {
        const actionItems = this.actionItemsSubject.value.map(a =>
          a.actionItemId === actionItem.actionItemId ? actionItem : a
        );
        this.actionItemsSubject.next(actionItems);
      })
    );
  }

  deleteActionItem(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const actionItems = this.actionItemsSubject.value.filter(a => a.actionItemId !== id);
        this.actionItemsSubject.next(actionItems);
      })
    );
  }
}

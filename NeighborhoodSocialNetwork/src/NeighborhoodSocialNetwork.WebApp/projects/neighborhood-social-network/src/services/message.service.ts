import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, tap } from 'rxjs';
import { environment } from '../environments';
import { Message, CreateMessage, UpdateMessage } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/messages`;

  private messagesSubject = new BehaviorSubject<Message[]>([]);
  messages$ = this.messagesSubject.asObservable();

  private selectedMessageSubject = new BehaviorSubject<Message | null>(null);
  selectedMessage$ = this.selectedMessageSubject.asObservable();

  getAll() {
    return this.http.get<Message[]>(this.baseUrl).pipe(
      tap(messages => this.messagesSubject.next(messages))
    );
  }

  getById(id: string) {
    return this.http.get<Message>(`${this.baseUrl}/${id}`).pipe(
      tap(message => this.selectedMessageSubject.next(message))
    );
  }

  create(message: CreateMessage) {
    return this.http.post<Message>(this.baseUrl, message).pipe(
      tap(newMessage => {
        const messages = this.messagesSubject.value;
        this.messagesSubject.next([...messages, newMessage]);
      })
    );
  }

  update(message: UpdateMessage) {
    return this.http.put<Message>(`${this.baseUrl}/${message.messageId}`, message).pipe(
      tap(updatedMessage => {
        const messages = this.messagesSubject.value.map(m =>
          m.messageId === updatedMessage.messageId ? updatedMessage : m
        );
        this.messagesSubject.next(messages);
        this.selectedMessageSubject.next(updatedMessage);
      })
    );
  }

  delete(id: string) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const messages = this.messagesSubject.value.filter(m => m.messageId !== id);
        this.messagesSubject.next(messages);
        if (this.selectedMessageSubject.value?.messageId === id) {
          this.selectedMessageSubject.next(null);
        }
      })
    );
  }
}

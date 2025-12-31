import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Document, CreateDocumentCommand, UpdateDocumentCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {
  private readonly apiUrl = `${environment.baseUrl}/api/documents`;
  private documentsSubject = new BehaviorSubject<Document[]>([]);
  public documents$ = this.documentsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getDocuments(userId?: string): Observable<Document[]> {
    const params = userId ? { userId } : {};
    return this.http.get<Document[]>(this.apiUrl, { params }).pipe(
      tap(documents => this.documentsSubject.next(documents))
    );
  }

  getDocumentById(id: string): Observable<Document> {
    return this.http.get<Document>(`${this.apiUrl}/${id}`);
  }

  createDocument(command: CreateDocumentCommand): Observable<Document> {
    return this.http.post<Document>(this.apiUrl, command).pipe(
      tap(() => this.getDocuments().subscribe())
    );
  }

  updateDocument(id: string, command: UpdateDocumentCommand): Observable<Document> {
    return this.http.put<Document>(`${this.apiUrl}/${id}`, command).pipe(
      tap(() => this.getDocuments().subscribe())
    );
  }

  deleteDocument(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.getDocuments().subscribe())
    );
  }
}

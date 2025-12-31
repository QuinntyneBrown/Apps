import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { LegacyDocument, CreateLegacyDocumentCommand, UpdateLegacyDocumentCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class LegacyDocumentsService {
  private readonly baseUrl = `${environment.baseUrl}/api/LegacyDocuments`;
  private documentsSubject = new BehaviorSubject<LegacyDocument[]>([]);
  public documents$ = this.documentsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(userId?: string, documentType?: string, needsReview?: boolean): Observable<LegacyDocument[]> {
    let url = this.baseUrl;
    const params: string[] = [];

    if (userId) params.push(`userId=${userId}`);
    if (documentType) params.push(`documentType=${documentType}`);
    if (needsReview !== undefined) params.push(`needsReview=${needsReview}`);

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http.get<LegacyDocument[]>(url).pipe(
      tap(documents => this.documentsSubject.next(documents))
    );
  }

  getById(id: string): Observable<LegacyDocument> {
    return this.http.get<LegacyDocument>(`${this.baseUrl}/${id}`);
  }

  create(command: CreateLegacyDocumentCommand): Observable<LegacyDocument> {
    return this.http.post<LegacyDocument>(this.baseUrl, command).pipe(
      tap(document => {
        const current = this.documentsSubject.value;
        this.documentsSubject.next([...current, document]);
      })
    );
  }

  update(id: string, command: UpdateLegacyDocumentCommand): Observable<LegacyDocument> {
    return this.http.put<LegacyDocument>(`${this.baseUrl}/${id}`, command).pipe(
      tap(updated => {
        const current = this.documentsSubject.value;
        const index = current.findIndex(d => d.legacyDocumentId === id);
        if (index !== -1) {
          current[index] = updated;
          this.documentsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.documentsSubject.value;
        this.documentsSubject.next(current.filter(d => d.legacyDocumentId !== id));
      })
    );
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { DocumentCategory, CreateDocumentCategoryCommand, UpdateDocumentCategoryCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class DocumentCategoryService {
  private readonly apiUrl = `${environment.baseUrl}/api/documentcategories`;
  private categoriesSubject = new BehaviorSubject<DocumentCategory[]>([]);
  public categories$ = this.categoriesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getDocumentCategories(): Observable<DocumentCategory[]> {
    return this.http.get<DocumentCategory[]>(this.apiUrl).pipe(
      tap(categories => this.categoriesSubject.next(categories))
    );
  }

  getDocumentCategoryById(id: string): Observable<DocumentCategory> {
    return this.http.get<DocumentCategory>(`${this.apiUrl}/${id}`);
  }

  createDocumentCategory(command: CreateDocumentCategoryCommand): Observable<DocumentCategory> {
    return this.http.post<DocumentCategory>(this.apiUrl, command).pipe(
      tap(() => this.getDocumentCategories().subscribe())
    );
  }

  updateDocumentCategory(id: string, command: UpdateDocumentCategoryCommand): Observable<DocumentCategory> {
    return this.http.put<DocumentCategory>(`${this.apiUrl}/${id}`, command).pipe(
      tap(() => this.getDocumentCategories().subscribe())
    );
  }

  deleteDocumentCategory(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.getDocumentCategories().subscribe())
    );
  }
}

import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Tool } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ToolService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _toolsSubject = new BehaviorSubject<Tool[]>([]);

  tools$ = this._toolsSubject.asObservable();

  getTools(): Observable<Tool[]> {
    return this._http.get<Tool[]>(`${this._baseUrl}/api/tools`).pipe(
      tap(tools => this._toolsSubject.next(tools))
    );
  }

  getToolById(id: string): Observable<Tool> {
    return this._http.get<Tool>(`${this._baseUrl}/api/tools/${id}`);
  }

  createTool(tool: Partial<Tool>): Observable<Tool> {
    return this._http.post<Tool>(`${this._baseUrl}/api/tools`, tool).pipe(
      tap(() => this.getTools().subscribe())
    );
  }

  updateTool(id: string, tool: Partial<Tool>): Observable<Tool> {
    return this._http.put<Tool>(`${this._baseUrl}/api/tools/${id}`, tool).pipe(
      tap(() => this.getTools().subscribe())
    );
  }

  deleteTool(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/tools/${id}`).pipe(
      tap(() => this.getTools().subscribe())
    );
  }
}

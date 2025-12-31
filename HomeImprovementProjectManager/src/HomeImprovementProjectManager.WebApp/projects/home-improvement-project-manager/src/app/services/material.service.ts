import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Material } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MaterialService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/materials`;

  private materialsSubject = new BehaviorSubject<Material[]>([]);
  public materials$ = this.materialsSubject.asObservable();

  getByProjectId(projectId: string): Observable<Material[]> {
    return this.http.get<Material[]>(`${this.baseUrl}?projectId=${projectId}`).pipe(
      tap(materials => this.materialsSubject.next(materials))
    );
  }

  create(material: Partial<Material>): Observable<Material> {
    return this.http.post<Material>(this.baseUrl, material).pipe(
      tap(newMaterial => {
        const current = this.materialsSubject.value;
        this.materialsSubject.next([...current, newMaterial]);
      })
    );
  }
}

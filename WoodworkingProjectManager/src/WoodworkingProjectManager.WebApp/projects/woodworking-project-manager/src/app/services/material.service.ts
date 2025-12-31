import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Material } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MaterialService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _materialsSubject = new BehaviorSubject<Material[]>([]);

  materials$ = this._materialsSubject.asObservable();

  getMaterials(): Observable<Material[]> {
    return this._http.get<Material[]>(`${this._baseUrl}/api/materials`).pipe(
      tap(materials => this._materialsSubject.next(materials))
    );
  }

  getMaterialById(id: string): Observable<Material> {
    return this._http.get<Material>(`${this._baseUrl}/api/materials/${id}`);
  }

  createMaterial(material: Partial<Material>): Observable<Material> {
    return this._http.post<Material>(`${this._baseUrl}/api/materials`, material).pipe(
      tap(() => this.getMaterials().subscribe())
    );
  }

  updateMaterial(id: string, material: Partial<Material>): Observable<Material> {
    return this._http.put<Material>(`${this._baseUrl}/api/materials/${id}`, material).pipe(
      tap(() => this.getMaterials().subscribe())
    );
  }

  deleteMaterial(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/materials/${id}`).pipe(
      tap(() => this.getMaterials().subscribe())
    );
  }
}

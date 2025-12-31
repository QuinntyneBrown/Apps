import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Equipment } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EquipmentService {
  private readonly apiUrl = `${environment.baseUrl}/api/Equipment`;
  private equipmentListSubject = new BehaviorSubject<Equipment[]>([]);
  private selectedEquipmentSubject = new BehaviorSubject<Equipment | null>(null);

  equipmentList$ = this.equipmentListSubject.asObservable();
  selectedEquipment$ = this.selectedEquipmentSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(userId?: string, isActive?: boolean): Observable<Equipment[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (isActive !== undefined) params = params.set('isActive', isActive.toString());

    return this.http.get<Equipment[]>(this.apiUrl, { params }).pipe(
      tap(equipment => this.equipmentListSubject.next(equipment))
    );
  }

  getById(id: string): Observable<Equipment> {
    return this.http.get<Equipment>(`${this.apiUrl}/${id}`).pipe(
      tap(equipment => this.selectedEquipmentSubject.next(equipment))
    );
  }

  create(equipment: Equipment): Observable<Equipment> {
    return this.http.post<Equipment>(this.apiUrl, equipment).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, equipment: Equipment): Observable<Equipment> {
    return this.http.put<Equipment>(`${this.apiUrl}/${id}`, equipment).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}

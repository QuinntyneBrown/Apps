import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments';
import { NutritionInfo, CreateNutritionInfo, UpdateNutritionInfo } from '../models';

@Injectable({
  providedIn: 'root'
})
export class NutritionInfoService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/nutritioninfos`;

  private nutritionInfosSubject = new BehaviorSubject<NutritionInfo[]>([]);
  public nutritionInfos$ = this.nutritionInfosSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getAll(): Observable<NutritionInfo[]> {
    this.loadingSubject.next(true);
    return this.http.get<NutritionInfo[]>(this.baseUrl).pipe(
      tap(nutritionInfos => {
        this.nutritionInfosSubject.next(nutritionInfos);
        this.loadingSubject.next(false);
      })
    );
  }

  getById(id: string): Observable<NutritionInfo> {
    return this.http.get<NutritionInfo>(`${this.baseUrl}/${id}`);
  }

  create(nutritionInfo: CreateNutritionInfo): Observable<NutritionInfo> {
    this.loadingSubject.next(true);
    return this.http.post<NutritionInfo>(this.baseUrl, nutritionInfo).pipe(
      tap(() => {
        this.getAll().subscribe();
      })
    );
  }

  update(nutritionInfo: UpdateNutritionInfo): Observable<NutritionInfo> {
    this.loadingSubject.next(true);
    return this.http.put<NutritionInfo>(`${this.baseUrl}/${nutritionInfo.nutritionInfoId}`, nutritionInfo).pipe(
      tap(() => {
        this.getAll().subscribe();
      })
    );
  }

  delete(id: string): Observable<void> {
    this.loadingSubject.next(true);
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        this.getAll().subscribe();
      })
    );
  }
}

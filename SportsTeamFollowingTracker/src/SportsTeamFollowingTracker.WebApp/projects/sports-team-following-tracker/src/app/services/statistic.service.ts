import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Statistic, CreateStatisticRequest, UpdateStatisticRequest } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StatisticService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _statisticsSubject = new BehaviorSubject<Statistic[]>([]);
  public statistics$ = this._statisticsSubject.asObservable();

  getStatistics(teamId?: string): Observable<Statistic[]> {
    const url = teamId
      ? `${this._baseUrl}/api/statistics?teamId=${teamId}`
      : `${this._baseUrl}/api/statistics`;
    return this._http.get<Statistic[]>(url).pipe(
      tap(statistics => this._statisticsSubject.next(statistics))
    );
  }

  getStatisticById(id: string): Observable<Statistic> {
    return this._http.get<Statistic>(`${this._baseUrl}/api/statistics/${id}`);
  }

  createStatistic(request: CreateStatisticRequest): Observable<Statistic> {
    return this._http.post<Statistic>(`${this._baseUrl}/api/statistics`, request).pipe(
      tap(statistic => {
        const currentStatistics = this._statisticsSubject.value;
        this._statisticsSubject.next([...currentStatistics, statistic]);
      })
    );
  }

  updateStatistic(request: UpdateStatisticRequest): Observable<Statistic> {
    return this._http.put<Statistic>(`${this._baseUrl}/api/statistics/${request.statisticId}`, request).pipe(
      tap(updatedStatistic => {
        const currentStatistics = this._statisticsSubject.value;
        const index = currentStatistics.findIndex(s => s.statisticId === updatedStatistic.statisticId);
        if (index !== -1) {
          currentStatistics[index] = updatedStatistic;
          this._statisticsSubject.next([...currentStatistics]);
        }
      })
    );
  }

  deleteStatistic(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/statistics/${id}`).pipe(
      tap(() => {
        const currentStatistics = this._statisticsSubject.value;
        this._statisticsSubject.next(currentStatistics.filter(s => s.statisticId !== id));
      })
    );
  }
}

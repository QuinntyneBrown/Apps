import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { ValueEstimate, CreateValueEstimateCommand, UpdateValueEstimateCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ValueEstimateService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/valueestimates`;

  private valueEstimatesSubject = new BehaviorSubject<ValueEstimate[]>([]);
  public valueEstimates$ = this.valueEstimatesSubject.asObservable();

  private selectedValueEstimateSubject = new BehaviorSubject<ValueEstimate | null>(null);
  public selectedValueEstimate$ = this.selectedValueEstimateSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getValueEstimates(itemId?: string): Observable<ValueEstimate[]> {
    this.loadingSubject.next(true);

    let params = new HttpParams();
    if (itemId) params = params.set('itemId', itemId);

    return this.http.get<ValueEstimate[]>(this.baseUrl, { params }).pipe(
      tap(estimates => {
        this.valueEstimatesSubject.next(estimates);
        this.loadingSubject.next(false);
      })
    );
  }

  getValueEstimateById(valueEstimateId: string): Observable<ValueEstimate> {
    this.loadingSubject.next(true);

    return this.http.get<ValueEstimate>(`${this.baseUrl}/${valueEstimateId}`).pipe(
      tap(estimate => {
        this.selectedValueEstimateSubject.next(estimate);
        this.loadingSubject.next(false);
      })
    );
  }

  createValueEstimate(command: CreateValueEstimateCommand): Observable<ValueEstimate> {
    this.loadingSubject.next(true);

    return this.http.post<ValueEstimate>(this.baseUrl, command).pipe(
      tap(estimate => {
        const currentEstimates = this.valueEstimatesSubject.value;
        this.valueEstimatesSubject.next([...currentEstimates, estimate]);
        this.loadingSubject.next(false);
      })
    );
  }

  updateValueEstimate(valueEstimateId: string, command: UpdateValueEstimateCommand): Observable<ValueEstimate> {
    this.loadingSubject.next(true);

    return this.http.put<ValueEstimate>(`${this.baseUrl}/${valueEstimateId}`, command).pipe(
      tap(updatedEstimate => {
        const currentEstimates = this.valueEstimatesSubject.value;
        const index = currentEstimates.findIndex(e => e.valueEstimateId === valueEstimateId);
        if (index !== -1) {
          const newEstimates = [...currentEstimates];
          newEstimates[index] = updatedEstimate;
          this.valueEstimatesSubject.next(newEstimates);
        }
        this.selectedValueEstimateSubject.next(updatedEstimate);
        this.loadingSubject.next(false);
      })
    );
  }

  deleteValueEstimate(valueEstimateId: string): Observable<void> {
    this.loadingSubject.next(true);

    return this.http.delete<void>(`${this.baseUrl}/${valueEstimateId}`).pipe(
      tap(() => {
        const currentEstimates = this.valueEstimatesSubject.value;
        this.valueEstimatesSubject.next(currentEstimates.filter(e => e.valueEstimateId !== valueEstimateId));
        if (this.selectedValueEstimateSubject.value?.valueEstimateId === valueEstimateId) {
          this.selectedValueEstimateSubject.next(null);
        }
        this.loadingSubject.next(false);
      })
    );
  }

  clearSelection(): void {
    this.selectedValueEstimateSubject.next(null);
  }
}

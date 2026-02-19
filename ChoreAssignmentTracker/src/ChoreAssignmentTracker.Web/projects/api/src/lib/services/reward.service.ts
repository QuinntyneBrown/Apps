import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Reward, CreateReward, UpdateReward, RedeemReward } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RewardService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/rewards`;

  private rewardsSubject = new BehaviorSubject<Reward[]>([]);
  public rewards$ = this.rewardsSubject.asObservable();

  getAll(userId?: string, isAvailable?: boolean): Observable<Reward[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (isAvailable !== undefined) params = params.set('isAvailable', isAvailable.toString());

    return this.http.get<Reward[]>(this.baseUrl, { params }).pipe(
      tap(rewards => this.rewardsSubject.next(rewards))
    );
  }

  getById(id: string): Observable<Reward> {
    return this.http.get<Reward>(`${this.baseUrl}/${id}`);
  }

  create(reward: CreateReward): Observable<Reward> {
    return this.http.post<Reward>(this.baseUrl, reward).pipe(
      tap(() => this.refresh())
    );
  }

  update(id: string, reward: UpdateReward): Observable<Reward> {
    return this.http.put<Reward>(`${this.baseUrl}/${id}`, reward).pipe(
      tap(() => this.refresh())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.refresh())
    );
  }

  redeem(id: string, data: RedeemReward): Observable<Reward> {
    return this.http.post<Reward>(`${this.baseUrl}/${id}/redeem`, data).pipe(
      tap(() => this.refresh())
    );
  }

  private refresh(): void {
    this.getAll().subscribe();
  }
}

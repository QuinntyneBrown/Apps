import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import {
  Subscription,
  CreateSubscriptionRequest,
  UpdateSubscriptionRequest,
  SubscriptionStatus,
  BillingCycle
} from '../models';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _subscriptionsSubject = new BehaviorSubject<Subscription[]>([]);
  public subscriptions$ = this._subscriptionsSubject.asObservable();

  getSubscriptions(status?: SubscriptionStatus, categoryId?: string, billingCycle?: BillingCycle): Observable<Subscription[]> {
    let url = `${this._baseUrl}/api/subscriptions`;
    const params = new URLSearchParams();

    if (status !== undefined && status !== null) {
      params.append('status', status.toString());
    }
    if (categoryId) {
      params.append('categoryId', categoryId);
    }
    if (billingCycle !== undefined && billingCycle !== null) {
      params.append('billingCycle', billingCycle.toString());
    }

    if (params.toString()) {
      url += `?${params.toString()}`;
    }

    return this._http.get<Subscription[]>(url).pipe(
      tap(subscriptions => this._subscriptionsSubject.next(subscriptions))
    );
  }

  getSubscriptionById(subscriptionId: string): Observable<Subscription> {
    return this._http.get<Subscription>(`${this._baseUrl}/api/subscriptions/${subscriptionId}`);
  }

  createSubscription(request: CreateSubscriptionRequest): Observable<Subscription> {
    return this._http.post<Subscription>(`${this._baseUrl}/api/subscriptions`, request).pipe(
      tap(subscription => {
        const current = this._subscriptionsSubject.value;
        this._subscriptionsSubject.next([...current, subscription]);
      })
    );
  }

  updateSubscription(request: UpdateSubscriptionRequest): Observable<Subscription> {
    return this._http.put<Subscription>(
      `${this._baseUrl}/api/subscriptions/${request.subscriptionId}`,
      request
    ).pipe(
      tap(updated => {
        const current = this._subscriptionsSubject.value;
        const index = current.findIndex(s => s.subscriptionId === updated.subscriptionId);
        if (index !== -1) {
          current[index] = updated;
          this._subscriptionsSubject.next([...current]);
        }
      })
    );
  }

  deleteSubscription(subscriptionId: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/subscriptions/${subscriptionId}`).pipe(
      tap(() => {
        const current = this._subscriptionsSubject.value;
        const filtered = current.filter(s => s.subscriptionId !== subscriptionId);
        this._subscriptionsSubject.next(filtered);
      })
    );
  }

  calculateTotalMonthlyCost(subscriptions: Subscription[]): number {
    return subscriptions.reduce((total, sub) => {
      if (sub.status !== SubscriptionStatus.Active) {
        return total;
      }

      switch (sub.billingCycle) {
        case BillingCycle.Monthly:
          return total + sub.cost;
        case BillingCycle.Quarterly:
          return total + (sub.cost / 3);
        case BillingCycle.Annual:
          return total + (sub.cost / 12);
        case BillingCycle.Weekly:
          return total + (sub.cost * 52 / 12);
        default:
          return total;
      }
    }, 0);
  }

  calculateTotalAnnualCost(subscriptions: Subscription[]): number {
    return subscriptions
      .filter(s => s.status === SubscriptionStatus.Active)
      .reduce((total, sub) => total + sub.annualCost, 0);
  }
}

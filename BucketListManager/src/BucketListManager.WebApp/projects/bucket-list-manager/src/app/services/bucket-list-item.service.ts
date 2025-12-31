import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { BucketListItem, CreateBucketListItem, UpdateBucketListItem, Category, ItemStatus } from '../models';

@Injectable({
  providedIn: 'root'
})
export class BucketListItemService {
  private readonly apiUrl = `${environment.baseUrl}/api/BucketListItems`;
  private bucketListItemsSubject = new BehaviorSubject<BucketListItem[]>([]);
  public bucketListItems$ = this.bucketListItemsSubject.asObservable();

  private currentItemSubject = new BehaviorSubject<BucketListItem | null>(null);
  public currentItem$ = this.currentItemSubject.asObservable();

  constructor(private http: HttpClient) {}

  getBucketListItems(userId?: string, category?: Category, status?: ItemStatus): Observable<BucketListItem[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (category !== undefined) params = params.set('category', category.toString());
    if (status !== undefined) params = params.set('status', status.toString());

    return this.http.get<BucketListItem[]>(this.apiUrl, { params }).pipe(
      tap(items => this.bucketListItemsSubject.next(items))
    );
  }

  getBucketListItemById(bucketListItemId: string): Observable<BucketListItem> {
    return this.http.get<BucketListItem>(`${this.apiUrl}/${bucketListItemId}`).pipe(
      tap(item => this.currentItemSubject.next(item))
    );
  }

  createBucketListItem(command: CreateBucketListItem): Observable<BucketListItem> {
    return this.http.post<BucketListItem>(this.apiUrl, command).pipe(
      tap(newItem => {
        const currentItems = this.bucketListItemsSubject.value;
        this.bucketListItemsSubject.next([...currentItems, newItem]);
      })
    );
  }

  updateBucketListItem(bucketListItemId: string, command: UpdateBucketListItem): Observable<BucketListItem> {
    return this.http.put<BucketListItem>(`${this.apiUrl}/${bucketListItemId}`, command).pipe(
      tap(updatedItem => {
        const currentItems = this.bucketListItemsSubject.value;
        const index = currentItems.findIndex(item => item.bucketListItemId === bucketListItemId);
        if (index !== -1) {
          currentItems[index] = updatedItem;
          this.bucketListItemsSubject.next([...currentItems]);
        }
        if (this.currentItemSubject.value?.bucketListItemId === bucketListItemId) {
          this.currentItemSubject.next(updatedItem);
        }
      })
    );
  }

  deleteBucketListItem(bucketListItemId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${bucketListItemId}`).pipe(
      tap(() => {
        const currentItems = this.bucketListItemsSubject.value;
        this.bucketListItemsSubject.next(currentItems.filter(item => item.bucketListItemId !== bucketListItemId));
        if (this.currentItemSubject.value?.bucketListItemId === bucketListItemId) {
          this.currentItemSubject.next(null);
        }
      })
    );
  }
}

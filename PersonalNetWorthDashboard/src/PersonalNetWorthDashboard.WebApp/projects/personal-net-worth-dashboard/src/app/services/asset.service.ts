import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Asset, CreateAsset, UpdateAsset } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class AssetService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/assets`;

  private assetsSubject = new BehaviorSubject<Asset[]>([]);
  public assets$ = this.assetsSubject.asObservable();

  getAssets(): Observable<Asset[]> {
    return this.http.get<Asset[]>(this.baseUrl).pipe(
      tap(assets => this.assetsSubject.next(assets))
    );
  }

  getAssetById(id: string): Observable<Asset> {
    return this.http.get<Asset>(`${this.baseUrl}/${id}`);
  }

  createAsset(asset: CreateAsset): Observable<Asset> {
    return this.http.post<Asset>(this.baseUrl, asset).pipe(
      tap(() => this.getAssets().subscribe())
    );
  }

  updateAsset(asset: UpdateAsset): Observable<Asset> {
    return this.http.put<Asset>(`${this.baseUrl}/${asset.assetId}`, asset).pipe(
      tap(() => this.getAssets().subscribe())
    );
  }

  deleteAsset(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAssets().subscribe())
    );
  }
}

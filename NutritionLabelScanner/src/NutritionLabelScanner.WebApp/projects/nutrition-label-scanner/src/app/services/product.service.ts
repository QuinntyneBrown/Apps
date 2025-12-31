import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments';
import { Product, CreateProduct, UpdateProduct } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/products`;

  private productsSubject = new BehaviorSubject<Product[]>([]);
  public products$ = this.productsSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getAll(): Observable<Product[]> {
    this.loadingSubject.next(true);
    return this.http.get<Product[]>(this.baseUrl).pipe(
      tap(products => {
        this.productsSubject.next(products);
        this.loadingSubject.next(false);
      })
    );
  }

  getById(id: string): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/${id}`);
  }

  create(product: CreateProduct): Observable<Product> {
    this.loadingSubject.next(true);
    return this.http.post<Product>(this.baseUrl, product).pipe(
      tap(() => {
        this.getAll().subscribe();
      })
    );
  }

  update(product: UpdateProduct): Observable<Product> {
    this.loadingSubject.next(true);
    return this.http.put<Product>(`${this.baseUrl}/${product.productId}`, product).pipe(
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

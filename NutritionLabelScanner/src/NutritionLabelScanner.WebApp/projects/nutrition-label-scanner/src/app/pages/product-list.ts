import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCardModule } from '@angular/material/card';
import { ProductService } from '../services';
import { Product } from '../models';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatCardModule
  ],
  template: `
    <div class="product-list">
      <div class="product-list__header">
        <h1 class="product-list__title">Products</h1>
        <button mat-raised-button color="primary" (click)="navigateToCreate()">
          <mat-icon>add</mat-icon>
          Add Product
        </button>
      </div>

      <mat-card class="product-list__card">
        <div *ngIf="productService.loading$ | async" class="product-list__loading">
          <mat-spinner></mat-spinner>
        </div>

        <div *ngIf="!(productService.loading$ | async)">
          <table mat-table [dataSource]="(productService.products$ | async) || []" class="product-list__table">
            <ng-container matColumnDef="name">
              <th mat-header-cell *matHeaderCellDef>Name</th>
              <td mat-cell *matCellDef="let product">{{ product.name }}</td>
            </ng-container>

            <ng-container matColumnDef="brand">
              <th mat-header-cell *matHeaderCellDef>Brand</th>
              <td mat-cell *matCellDef="let product">{{ product.brand || 'N/A' }}</td>
            </ng-container>

            <ng-container matColumnDef="barcode">
              <th mat-header-cell *matHeaderCellDef>Barcode</th>
              <td mat-cell *matCellDef="let product">{{ product.barcode || 'N/A' }}</td>
            </ng-container>

            <ng-container matColumnDef="category">
              <th mat-header-cell *matHeaderCellDef>Category</th>
              <td mat-cell *matCellDef="let product">{{ product.category || 'N/A' }}</td>
            </ng-container>

            <ng-container matColumnDef="scannedAt">
              <th mat-header-cell *matHeaderCellDef>Scanned At</th>
              <td mat-cell *matCellDef="let product">{{ product.scannedAt | date:'short' }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let product">
                <button mat-icon-button color="primary" (click)="navigateToEdit(product.productId)">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteProduct(product.productId)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>

          <div *ngIf="(productService.products$ | async)?.length === 0" class="product-list__empty">
            <mat-icon class="product-list__empty-icon">inventory_2</mat-icon>
            <p>No products found. Add your first product to get started!</p>
          </div>
        </div>
      </mat-card>
    </div>
  `,
  styles: [`
    .product-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;
    }

    .product-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .product-list__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .product-list__card {
      overflow: auto;
    }

    .product-list__loading {
      display: flex;
      justify-content: center;
      padding: 3rem;
    }

    .product-list__table {
      width: 100%;
    }

    .product-list__empty {
      text-align: center;
      padding: 3rem;
      color: rgba(0, 0, 0, 0.6);
    }

    .product-list__empty-icon {
      font-size: 64px;
      width: 64px;
      height: 64px;
      color: rgba(0, 0, 0, 0.26);
    }
  `]
})
export class ProductList implements OnInit {
  private readonly router = inject(Router);
  public readonly productService = inject(ProductService);

  displayedColumns: string[] = ['name', 'brand', 'barcode', 'category', 'scannedAt', 'actions'];

  ngOnInit(): void {
    this.productService.getAll().subscribe();
  }

  navigateToCreate(): void {
    this.router.navigate(['/products/new']);
  }

  navigateToEdit(id: string): void {
    this.router.navigate(['/products', id]);
  }

  deleteProduct(id: string): void {
    if (confirm('Are you sure you want to delete this product?')) {
      this.productService.delete(id).subscribe();
    }
  }
}

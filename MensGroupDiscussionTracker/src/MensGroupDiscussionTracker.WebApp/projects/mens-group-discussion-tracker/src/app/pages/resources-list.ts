import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { ResourceService } from '../services';
import { Resource } from '../models';

@Component({
  selector: 'app-resources-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  template: `
    <div class="resources-list">
      <div class="resources-list__header">
        <h1 class="resources-list__title">Resources</h1>
        <button mat-raised-button color="primary" (click)="createResource()">
          <mat-icon>add</mat-icon>
          New Resource
        </button>
      </div>

      <mat-card class="resources-list__card">
        <table mat-table [dataSource]="resources$ | async" class="resources-list__table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let resource">{{ resource.title }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let resource">{{ resource.description || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="url">
            <th mat-header-cell *matHeaderCellDef>URL</th>
            <td mat-cell *matCellDef="let resource">
              <a *ngIf="resource.url" [href]="resource.url" target="_blank" class="resources-list__link">
                {{ resource.url.length > 40 ? resource.url.substring(0, 40) + '...' : resource.url }}
              </a>
              <span *ngIf="!resource.url">-</span>
            </td>
          </ng-container>

          <ng-container matColumnDef="resourceType">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let resource">{{ resource.resourceType || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let resource">
              <button mat-icon-button color="primary" (click)="editResource(resource.resourceId)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteResource(resource.resourceId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .resources-list {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
        font-weight: 400;
      }

      &__card {
        overflow: auto;
      }

      &__table {
        width: 100%;
      }

      &__link {
        color: #3f51b5;
        text-decoration: none;

        &:hover {
          text-decoration: underline;
        }
      }
    }
  `]
})
export class ResourcesList implements OnInit {
  private readonly resourceService = inject(ResourceService);
  private readonly router = inject(Router);

  resources$ = this.resourceService.resources$;
  displayedColumns = ['title', 'description', 'url', 'resourceType', 'actions'];

  ngOnInit(): void {
    this.resourceService.getAll().subscribe();
  }

  createResource(): void {
    this.router.navigate(['/resources/new']);
  }

  editResource(id: string): void {
    this.router.navigate(['/resources', id]);
  }

  deleteResource(id: string): void {
    if (confirm('Are you sure you want to delete this resource?')) {
      this.resourceService.delete(id).subscribe();
    }
  }
}

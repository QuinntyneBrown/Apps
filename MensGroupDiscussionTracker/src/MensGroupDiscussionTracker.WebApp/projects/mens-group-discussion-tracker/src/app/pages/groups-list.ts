import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { GroupService } from '../services';
import { Group } from '../models';

@Component({
  selector: 'app-groups-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipsModule
  ],
  template: `
    <div class="groups-list">
      <div class="groups-list__header">
        <h1 class="groups-list__title">Groups</h1>
        <button mat-raised-button color="primary" (click)="createGroup()">
          <mat-icon>add</mat-icon>
          New Group
        </button>
      </div>

      <mat-card class="groups-list__card">
        <table mat-table [dataSource]="groups$ | async" class="groups-list__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let group">{{ group.name }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let group">{{ group.description || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="meetingSchedule">
            <th mat-header-cell *matHeaderCellDef>Meeting Schedule</th>
            <td mat-cell *matCellDef="let group">{{ group.meetingSchedule || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="isActive">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let group">
              <mat-chip [class.groups-list__chip--active]="group.isActive" [class.groups-list__chip--inactive]="!group.isActive">
                {{ group.isActive ? 'Active' : 'Inactive' }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let group">
              <button mat-icon-button color="primary" (click)="editGroup(group.groupId)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteGroup(group.groupId)">
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
    .groups-list {
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

      &__chip--active {
        background-color: #4caf50 !important;
        color: white;
      }

      &__chip--inactive {
        background-color: #9e9e9e !important;
        color: white;
      }
    }
  `]
})
export class GroupsList implements OnInit {
  private readonly groupService = inject(GroupService);
  private readonly router = inject(Router);

  groups$ = this.groupService.groups$;
  displayedColumns = ['name', 'description', 'meetingSchedule', 'isActive', 'actions'];

  ngOnInit(): void {
    this.groupService.getAll().subscribe();
  }

  createGroup(): void {
    this.router.navigate(['/groups/new']);
  }

  editGroup(id: string): void {
    this.router.navigate(['/groups', id]);
  }

  deleteGroup(id: string): void {
    if (confirm('Are you sure you want to delete this group?')) {
      this.groupService.delete(id).subscribe();
    }
  }
}

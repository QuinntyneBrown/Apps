import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { FollowUpsService, ContactsService } from '../services';
import { FollowUp } from '../models';

@Component({
  selector: 'app-follow-ups-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  template: `
    <div class="follow-ups-list">
      <div class="follow-ups-list__header">
        <h1 class="follow-ups-list__title">Follow-ups</h1>
        <button mat-raised-button color="primary" routerLink="/follow-ups/new" class="follow-ups-list__add-btn">
          <mat-icon>add</mat-icon>
          Add Follow-up
        </button>
      </div>

      <mat-card class="follow-ups-list__card">
        <table mat-table [dataSource]="(followUpsService.followUps$ | async) || []" class="follow-ups-list__table">
          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let followUp">{{ followUp.description }}</td>
          </ng-container>

          <ng-container matColumnDef="contactId">
            <th mat-header-cell *matHeaderCellDef>Contact</th>
            <td mat-cell *matCellDef="let followUp">{{ getContactName(followUp.contactId) }}</td>
          </ng-container>

          <ng-container matColumnDef="dueDate">
            <th mat-header-cell *matHeaderCellDef>Due Date</th>
            <td mat-cell *matCellDef="let followUp">{{ followUp.dueDate | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="priority">
            <th mat-header-cell *matHeaderCellDef>Priority</th>
            <td mat-cell *matCellDef="let followUp">
              <mat-chip [class]="'follow-ups-list__priority-chip--' + followUp.priority.toLowerCase()">
                {{ followUp.priority }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let followUp">
              <mat-chip *ngIf="followUp.isCompleted" class="follow-ups-list__status-chip--completed">
                Completed
              </mat-chip>
              <mat-chip *ngIf="!followUp.isCompleted && followUp.isOverdue" class="follow-ups-list__status-chip--overdue">
                Overdue
              </mat-chip>
              <mat-chip *ngIf="!followUp.isCompleted && !followUp.isOverdue" class="follow-ups-list__status-chip--pending">
                Pending
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let followUp">
              <button mat-icon-button color="accent" *ngIf="!followUp.isCompleted" (click)="completeFollowUp(followUp.followUpId)" matTooltip="Mark as complete">
                <mat-icon>check_circle</mat-icon>
              </button>
              <button mat-icon-button color="primary" (click)="editFollowUp(followUp.followUpId)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteFollowUp(followUp.followUpId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;" class="follow-ups-list__row"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .follow-ups-list {
      padding: 2rem;
    }

    .follow-ups-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .follow-ups-list__title {
      margin: 0;
      color: #333;
    }

    .follow-ups-list__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .follow-ups-list__card {
      overflow-x: auto;
    }

    .follow-ups-list__table {
      width: 100%;
    }

    .follow-ups-list__row:hover {
      background-color: #f5f5f5;
    }

    .follow-ups-list__priority-chip--high {
      background-color: #f44336;
      color: white;
    }

    .follow-ups-list__priority-chip--medium {
      background-color: #ff9800;
      color: white;
    }

    .follow-ups-list__priority-chip--low {
      background-color: #4caf50;
      color: white;
    }

    .follow-ups-list__status-chip--completed {
      background-color: #4caf50;
      color: white;
    }

    .follow-ups-list__status-chip--overdue {
      background-color: #f44336;
      color: white;
    }

    .follow-ups-list__status-chip--pending {
      background-color: #2196f3;
      color: white;
    }
  `]
})
export class FollowUpsList implements OnInit {
  followUpsService = inject(FollowUpsService);
  private contactsService = inject(ContactsService);
  private router = inject(Router);

  displayedColumns: string[] = ['description', 'contactId', 'dueDate', 'priority', 'status', 'actions'];

  ngOnInit(): void {
    this.followUpsService.loadFollowUps().subscribe();
    this.contactsService.loadContacts().subscribe();
  }

  getContactName(contactId: string): string {
    const contacts = this.contactsService.contacts$.value;
    const contact = contacts.find(c => c.contactId === contactId);
    return contact ? contact.fullName : 'Unknown';
  }

  completeFollowUp(id: string): void {
    this.followUpsService.completeFollowUp(id).subscribe();
  }

  editFollowUp(id: string): void {
    this.router.navigate(['/follow-ups', id]);
  }

  deleteFollowUp(id: string): void {
    if (confirm('Are you sure you want to delete this follow-up?')) {
      this.followUpsService.deleteFollowUp(id).subscribe();
    }
  }
}

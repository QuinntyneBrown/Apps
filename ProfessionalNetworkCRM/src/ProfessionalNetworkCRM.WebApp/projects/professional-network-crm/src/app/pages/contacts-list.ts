import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { ContactsService } from '../services';
import { ContactTypeLabels } from '../models';

@Component({
  selector: 'app-contacts-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  template: `
    <div class="contacts-list">
      <div class="contacts-list__header">
        <h1 class="contacts-list__title">Contacts</h1>
        <button mat-raised-button color="primary" routerLink="/contacts/new" class="contacts-list__add-btn">
          <mat-icon>add</mat-icon>
          Add Contact
        </button>
      </div>

      <mat-card class="contacts-list__card">
        <table mat-table [dataSource]="(contactsService.contacts$ | async) || []" class="contacts-list__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let contact">{{ contact.fullName }}</td>
          </ng-container>

          <ng-container matColumnDef="type">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let contact">{{ getContactTypeLabel(contact.contactType) }}</td>
          </ng-container>

          <ng-container matColumnDef="company">
            <th mat-header-cell *matHeaderCellDef>Company</th>
            <td mat-cell *matCellDef="let contact">{{ contact.company || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="jobTitle">
            <th mat-header-cell *matHeaderCellDef>Job Title</th>
            <td mat-cell *matCellDef="let contact">{{ contact.jobTitle || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="email">
            <th mat-header-cell *matHeaderCellDef>Email</th>
            <td mat-cell *matCellDef="let contact">{{ contact.email || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="priority">
            <th mat-header-cell *matHeaderCellDef>Priority</th>
            <td mat-cell *matCellDef="let contact">
              <mat-chip *ngIf="contact.isPriority" class="contacts-list__priority-chip">Priority</mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let contact">
              <button mat-icon-button color="primary" (click)="editContact(contact.contactId)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteContact(contact.contactId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;" class="contacts-list__row"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .contacts-list {
      padding: 2rem;
    }

    .contacts-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .contacts-list__title {
      margin: 0;
      color: #333;
    }

    .contacts-list__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .contacts-list__card {
      overflow-x: auto;
    }

    .contacts-list__table {
      width: 100%;
    }

    .contacts-list__row:hover {
      background-color: #f5f5f5;
    }

    .contacts-list__priority-chip {
      background-color: #ff9800;
      color: white;
    }
  `]
})
export class ContactsList implements OnInit {
  contactsService = inject(ContactsService);
  private router = inject(Router);

  displayedColumns: string[] = ['name', 'type', 'company', 'jobTitle', 'email', 'priority', 'actions'];

  ngOnInit(): void {
    this.contactsService.loadContacts().subscribe();
  }

  getContactTypeLabel(type: number): string {
    return ContactTypeLabels[type] || 'Unknown';
  }

  editContact(id: string): void {
    this.router.navigate(['/contacts', id]);
  }

  deleteContact(id: string): void {
    if (confirm('Are you sure you want to delete this contact?')) {
      this.contactsService.deleteContact(id).subscribe();
    }
  }
}

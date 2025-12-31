import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { InteractionsService, ContactsService } from '../services';

@Component({
  selector: 'app-interactions-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="interactions-list">
      <div class="interactions-list__header">
        <h1 class="interactions-list__title">Interactions</h1>
        <button mat-raised-button color="primary" routerLink="/interactions/new" class="interactions-list__add-btn">
          <mat-icon>add</mat-icon>
          Log Interaction
        </button>
      </div>

      <mat-card class="interactions-list__card">
        <table mat-table [dataSource]="(interactionsService.interactions$ | async) || []" class="interactions-list__table">
          <ng-container matColumnDef="contactId">
            <th mat-header-cell *matHeaderCellDef>Contact</th>
            <td mat-cell *matCellDef="let interaction">{{ getContactName(interaction.contactId) }}</td>
          </ng-container>

          <ng-container matColumnDef="interactionType">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let interaction">{{ interaction.interactionType }}</td>
          </ng-container>

          <ng-container matColumnDef="interactionDate">
            <th mat-header-cell *matHeaderCellDef>Date</th>
            <td mat-cell *matCellDef="let interaction">{{ interaction.interactionDate | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="subject">
            <th mat-header-cell *matHeaderCellDef>Subject</th>
            <td mat-cell *matCellDef="let interaction">{{ interaction.subject || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="outcome">
            <th mat-header-cell *matHeaderCellDef>Outcome</th>
            <td mat-cell *matCellDef="let interaction">{{ interaction.outcome || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="duration">
            <th mat-header-cell *matHeaderCellDef>Duration</th>
            <td mat-cell *matCellDef="let interaction">
              {{ interaction.durationMinutes ? interaction.durationMinutes + ' min' : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let interaction">
              <button mat-icon-button color="primary" (click)="editInteraction(interaction.interactionId)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteInteraction(interaction.interactionId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;" class="interactions-list__row"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .interactions-list {
      padding: 2rem;
    }

    .interactions-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .interactions-list__title {
      margin: 0;
      color: #333;
    }

    .interactions-list__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .interactions-list__card {
      overflow-x: auto;
    }

    .interactions-list__table {
      width: 100%;
    }

    .interactions-list__row:hover {
      background-color: #f5f5f5;
    }
  `]
})
export class InteractionsList implements OnInit {
  interactionsService = inject(InteractionsService);
  private contactsService = inject(ContactsService);
  private router = inject(Router);

  displayedColumns: string[] = ['contactId', 'interactionType', 'interactionDate', 'subject', 'outcome', 'duration', 'actions'];

  ngOnInit(): void {
    this.interactionsService.loadInteractions().subscribe();
    this.contactsService.loadContacts().subscribe();
  }

  getContactName(contactId: string): string {
    const contacts = this.contactsService.contacts$.value;
    const contact = contacts.find(c => c.contactId === contactId);
    return contact ? contact.fullName : 'Unknown';
  }

  editInteraction(id: string): void {
    this.router.navigate(['/interactions', id]);
  }

  deleteInteraction(id: string): void {
    if (confirm('Are you sure you want to delete this interaction?')) {
      this.interactionsService.deleteInteraction(id).subscribe();
    }
  }
}

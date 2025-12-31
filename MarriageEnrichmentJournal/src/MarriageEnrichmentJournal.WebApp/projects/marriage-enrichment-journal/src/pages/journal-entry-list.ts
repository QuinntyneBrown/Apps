import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { JournalEntryService } from '../services';
import { EntryTypeLabels } from '../models';

@Component({
  selector: 'app-journal-entry-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule],
  template: `
    <div class="journal-entry-list">
      <div class="journal-entry-list__header">
        <h1 class="journal-entry-list__title">Journal Entries</h1>
        <a mat-raised-button color="primary" routerLink="/journal-entries/create">
          <mat-icon>add</mat-icon>
          Add Journal Entry
        </a>
      </div>

      <div class="journal-entry-list__table-container">
        <table mat-table [dataSource]="(journalEntryService.journalEntries$ | async) || []" class="journal-entry-list__table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let entry">{{ entry.title }}</td>
          </ng-container>

          <ng-container matColumnDef="entryType">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let entry">
              <mat-chip>{{ getEntryTypeLabel(entry.entryType) }}</mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="entryDate">
            <th mat-header-cell *matHeaderCellDef>Date</th>
            <td mat-cell *matCellDef="let entry">{{ entry.entryDate | date }}</td>
          </ng-container>

          <ng-container matColumnDef="isSharedWithPartner">
            <th mat-header-cell *matHeaderCellDef>Shared</th>
            <td mat-cell *matCellDef="let entry">
              <mat-icon *ngIf="entry.isSharedWithPartner" color="primary">check_circle</mat-icon>
              <mat-icon *ngIf="!entry.isSharedWithPartner">cancel</mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="isPrivate">
            <th mat-header-cell *matHeaderCellDef>Private</th>
            <td mat-cell *matCellDef="let entry">
              <mat-icon *ngIf="entry.isPrivate" color="accent">lock</mat-icon>
              <mat-icon *ngIf="!entry.isPrivate">lock_open</mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let entry">
              <button mat-icon-button color="primary" [routerLink]="['/journal-entries/edit', entry.journalEntryId]">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteEntry(entry.journalEntryId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </div>
    </div>
  `,
  styles: [`
    .journal-entry-list {
      padding: 2rem;
    }

    .journal-entry-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .journal-entry-list__title {
      margin: 0;
      color: #333;
    }

    .journal-entry-list__table-container {
      overflow-x: auto;
    }

    .journal-entry-list__table {
      width: 100%;
    }
  `]
})
export class JournalEntryList implements OnInit {
  journalEntryService = inject(JournalEntryService);
  displayedColumns = ['title', 'entryType', 'entryDate', 'isSharedWithPartner', 'isPrivate', 'actions'];

  ngOnInit(): void {
    this.journalEntryService.getAll().subscribe();
  }

  getEntryTypeLabel(type: number): string {
    return EntryTypeLabels[type];
  }

  deleteEntry(id: string): void {
    if (confirm('Are you sure you want to delete this journal entry?')) {
      this.journalEntryService.delete(id).subscribe();
    }
  }
}

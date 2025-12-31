import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { MessageService } from '../services';
import { Message, CreateMessage, UpdateMessage } from '../models';

@Component({
  selector: 'app-message-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Send' }} Message</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="message-form">
        <mat-form-field class="message-form__field">
          <mat-label>Sender Neighbor ID</mat-label>
          <input matInput formControlName="senderNeighborId" required>
        </mat-form-field>

        <mat-form-field class="message-form__field">
          <mat-label>Recipient Neighbor ID</mat-label>
          <input matInput formControlName="recipientNeighborId" required>
        </mat-form-field>

        <mat-form-field class="message-form__field">
          <mat-label>Subject</mat-label>
          <input matInput formControlName="subject" required>
        </mat-form-field>

        <mat-form-field class="message-form__field">
          <mat-label>Content</mat-label>
          <textarea matInput formControlName="content" rows="5" required></textarea>
        </mat-form-field>

        <mat-checkbox *ngIf="data" formControlName="isRead" class="message-form__checkbox">
          Mark as Read
        </mat-checkbox>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="form.invalid" (click)="save()">
        {{ data ? 'Save' : 'Send' }}
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .message-form {
      display: flex;
      flex-direction: column;
      gap: 16px;
      min-width: 400px;

      &__field {
        width: 100%;
      }

      &__checkbox {
        margin-top: 8px;
      }
    }
  `]
})
export class MessageDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);

  data?: Message;
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      senderNeighborId: ['', Validators.required],
      recipientNeighborId: ['', Validators.required],
      subject: ['', Validators.required],
      content: ['', Validators.required],
      isRead: [false]
    });

    if (this.data) {
      this.form.patchValue(this.data);
    }
  }

  save() {
    if (this.form.valid) {
      const result = this.data
        ? { ...this.form.value, messageId: this.data.messageId }
        : this.form.value;
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-messages',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  template: `
    <div class="messages">
      <div class="messages__header">
        <h1 class="messages__title">Messages</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Send Message
        </button>
      </div>

      <mat-card class="messages__card">
        <table mat-table [dataSource]="messages$ | async" class="messages__table">
          <ng-container matColumnDef="subject">
            <th mat-header-cell *matHeaderCellDef>Subject</th>
            <td mat-cell *matCellDef="let message">{{ message.subject }}</td>
          </ng-container>

          <ng-container matColumnDef="content">
            <th mat-header-cell *matHeaderCellDef>Content</th>
            <td mat-cell *matCellDef="let message">
              {{ message.content.length > 50 ? message.content.substring(0, 50) + '...' : message.content }}
            </td>
          </ng-container>

          <ng-container matColumnDef="senderNeighborId">
            <th mat-header-cell *matHeaderCellDef>Sender ID</th>
            <td mat-cell *matCellDef="let message">{{ message.senderNeighborId.substring(0, 8) }}...</td>
          </ng-container>

          <ng-container matColumnDef="recipientNeighborId">
            <th mat-header-cell *matHeaderCellDef>Recipient ID</th>
            <td mat-cell *matCellDef="let message">{{ message.recipientNeighborId.substring(0, 8) }}...</td>
          </ng-container>

          <ng-container matColumnDef="isRead">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let message">
              <mat-icon [class.read]="message.isRead">
                {{ message.isRead ? 'mark_email_read' : 'mark_email_unread' }}
              </mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="createdAt">
            <th mat-header-cell *matHeaderCellDef>Sent</th>
            <td mat-cell *matCellDef="let message">{{ message.createdAt | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let message">
              <button mat-icon-button color="primary" (click)="openDialog(message)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(message.messageId)">
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
    .messages {
      padding: 24px;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 24px;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
        font-weight: 400;
      }

      &__card {
        overflow-x: auto;
      }

      &__table {
        width: 100%;

        .read {
          color: #4caf50;
        }

        mat-icon:not(.read) {
          color: #2196f3;
        }
      }
    }
  `]
})
export class Messages implements OnInit {
  private messageService = inject(MessageService);
  private dialog = inject(MatDialog);

  messages$ = this.messageService.messages$;
  displayedColumns = ['subject', 'content', 'senderNeighborId', 'recipientNeighborId', 'isRead', 'createdAt', 'actions'];

  ngOnInit() {
    this.messageService.getAll().subscribe();
  }

  openDialog(message?: Message) {
    const dialogRef = this.dialog.open(MessageDialog, {
      width: '500px',
      data: message
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (message) {
          this.messageService.update(result as UpdateMessage).subscribe();
        } else {
          this.messageService.create(result as CreateMessage).subscribe();
        }
      }
    });
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this message?')) {
      this.messageService.delete(id).subscribe();
    }
  }
}

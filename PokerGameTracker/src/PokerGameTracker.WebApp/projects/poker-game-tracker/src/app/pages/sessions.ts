import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { SessionService } from '../services';
import { Session, CreateSession, UpdateSession, GameType, gameTypeLabels } from '../models';

@Component({
  selector: 'app-session-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Add' }} Session</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content class="dialog__content">
        <mat-form-field class="dialog__field">
          <mat-label>User ID</mat-label>
          <input matInput formControlName="userId" required>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Game Type</mat-label>
          <mat-select formControlName="gameType" required>
            <mat-option *ngFor="let type of gameTypes" [value]="type.value">
              {{ type.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Start Time</mat-label>
          <input matInput type="datetime-local" formControlName="startTime" required>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>End Time</mat-label>
          <input matInput type="datetime-local" formControlName="endTime">
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Buy In</mat-label>
          <input matInput type="number" formControlName="buyIn" required>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Cash Out</mat-label>
          <input matInput type="number" formControlName="cashOut">
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Location</mat-label>
          <input matInput formControlName="location">
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button type="button" mat-dialog-close>Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="!form.valid">
          {{ data ? 'Update' : 'Create' }}
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .dialog {
      &__content {
        display: flex;
        flex-direction: column;
        min-width: 400px;
        padding: 20px 24px;
      }

      &__field {
        width: 100%;
        margin-bottom: 16px;
      }
    }
  `]
})
export class SessionDialog {
  private fb = inject(FormBuilder);
  private sessionService = inject(SessionService);
  private dialogRef = inject(MatDialog);

  data: Session | null = null;
  form: FormGroup;

  gameTypes = Object.values(GameType)
    .filter(v => typeof v === 'number')
    .map(value => ({
      value: value as GameType,
      label: gameTypeLabels[value as GameType]
    }));

  constructor() {
    const now = new Date().toISOString().slice(0, 16);
    this.form = this.fb.group({
      userId: ['00000000-0000-0000-0000-000000000000', Validators.required],
      gameType: [GameType.TexasHoldem, Validators.required],
      startTime: [now, Validators.required],
      endTime: [''],
      buyIn: [0, [Validators.required, Validators.min(0)]],
      cashOut: [''],
      location: [''],
      notes: ['']
    });

    if (this.data) {
      const startTime = new Date(this.data.startTime).toISOString().slice(0, 16);
      const endTime = this.data.endTime ? new Date(this.data.endTime).toISOString().slice(0, 16) : '';
      this.form.patchValue({
        userId: this.data.userId,
        gameType: this.data.gameType,
        startTime: startTime,
        endTime: endTime,
        buyIn: this.data.buyIn,
        cashOut: this.data.cashOut,
        location: this.data.location,
        notes: this.data.notes
      });
    }
  }

  onSubmit() {
    if (this.form.valid) {
      const formValue = this.form.value;
      const startTime = new Date(formValue.startTime).toISOString();
      const endTime = formValue.endTime ? new Date(formValue.endTime).toISOString() : undefined;

      if (this.data) {
        const updateData: UpdateSession = {
          sessionId: this.data.sessionId,
          userId: formValue.userId,
          gameType: formValue.gameType,
          startTime: startTime,
          endTime: endTime,
          buyIn: formValue.buyIn,
          cashOut: formValue.cashOut || undefined,
          location: formValue.location || undefined,
          notes: formValue.notes || undefined
        };
        this.sessionService.updateSession(updateData).subscribe(() => {
          this.dialogRef.closeAll();
        });
      } else {
        const createData: CreateSession = {
          userId: formValue.userId,
          gameType: formValue.gameType,
          startTime: startTime,
          buyIn: formValue.buyIn,
          location: formValue.location || undefined,
          notes: formValue.notes || undefined
        };
        this.sessionService.createSession(createData).subscribe(() => {
          this.dialogRef.closeAll();
        });
      }
    }
  }
}

@Component({
  selector: 'app-sessions',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  template: `
    <div class="sessions">
      <div class="sessions__container">
        <div class="sessions__header">
          <h1 class="sessions__title">Sessions</h1>
          <button mat-raised-button color="primary" (click)="openDialog()">
            <mat-icon>add</mat-icon>
            Add Session
          </button>
        </div>

        <mat-card class="sessions__card">
          <table mat-table [dataSource]="sessions$ | async" class="sessions__table">
            <ng-container matColumnDef="gameType">
              <th mat-header-cell *matHeaderCellDef>Game Type</th>
              <td mat-cell *matCellDef="let session">{{ getGameTypeLabel(session.gameType) }}</td>
            </ng-container>

            <ng-container matColumnDef="startTime">
              <th mat-header-cell *matHeaderCellDef>Start Time</th>
              <td mat-cell *matCellDef="let session">{{ session.startTime | date:'short' }}</td>
            </ng-container>

            <ng-container matColumnDef="endTime">
              <th mat-header-cell *matHeaderCellDef>End Time</th>
              <td mat-cell *matCellDef="let session">{{ session.endTime ? (session.endTime | date:'short') : 'Active' }}</td>
            </ng-container>

            <ng-container matColumnDef="buyIn">
              <th mat-header-cell *matHeaderCellDef>Buy In</th>
              <td mat-cell *matCellDef="let session">\${{ session.buyIn | number:'1.2-2' }}</td>
            </ng-container>

            <ng-container matColumnDef="cashOut">
              <th mat-header-cell *matHeaderCellDef>Cash Out</th>
              <td mat-cell *matCellDef="let session">{{ session.cashOut ? ('$' + (session.cashOut | number:'1.2-2')) : '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="profit">
              <th mat-header-cell *matHeaderCellDef>Profit/Loss</th>
              <td mat-cell *matCellDef="let session">
                <span [style.color]="getProfit(session) >= 0 ? 'green' : 'red'">
                  {{ session.cashOut ? ('$' + (getProfit(session) | number:'1.2-2')) : '-' }}
                </span>
              </td>
            </ng-container>

            <ng-container matColumnDef="location">
              <th mat-header-cell *matHeaderCellDef>Location</th>
              <td mat-cell *matCellDef="let session">{{ session.location || '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let session">
                <button mat-icon-button color="primary" (click)="openDialog(session)">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteSession(session.sessionId)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .sessions {
      padding: 24px;

      &__container {
        max-width: 1400px;
        margin: 0 auto;
      }

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 24px;
      }

      &__title {
        margin: 0;
        font-size: 32px;
        font-weight: 400;
      }

      &__card {
        overflow-x: auto;
      }

      &__table {
        width: 100%;
      }
    }
  `]
})
export class Sessions implements OnInit {
  private sessionService = inject(SessionService);
  private dialog = inject(MatDialog);

  sessions$ = this.sessionService.sessions$;
  displayedColumns = ['gameType', 'startTime', 'endTime', 'buyIn', 'cashOut', 'profit', 'location', 'actions'];

  ngOnInit() {
    this.sessionService.getSessions().subscribe();
  }

  getGameTypeLabel(gameType: GameType): string {
    return gameTypeLabels[gameType];
  }

  getProfit(session: Session): number {
    return session.cashOut ? session.cashOut - session.buyIn : 0;
  }

  openDialog(session?: Session) {
    const dialogRef = this.dialog.open(SessionDialog, {
      width: '500px'
    });

    if (session) {
      dialogRef.componentInstance.data = session;
      const startTime = new Date(session.startTime).toISOString().slice(0, 16);
      const endTime = session.endTime ? new Date(session.endTime).toISOString().slice(0, 16) : '';
      dialogRef.componentInstance.form.patchValue({
        userId: session.userId,
        gameType: session.gameType,
        startTime: startTime,
        endTime: endTime,
        buyIn: session.buyIn,
        cashOut: session.cashOut,
        location: session.location,
        notes: session.notes
      });
    }
  }

  deleteSession(id: string) {
    if (confirm('Are you sure you want to delete this session?')) {
      this.sessionService.deleteSession(id).subscribe();
    }
  }
}

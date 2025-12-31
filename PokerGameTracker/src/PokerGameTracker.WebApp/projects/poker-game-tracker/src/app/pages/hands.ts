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
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { HandService, SessionService } from '../services';
import { Hand, CreateHand, UpdateHand } from '../models';

@Component({
  selector: 'app-hand-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Add' }} Hand</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content class="dialog__content">
        <mat-form-field class="dialog__field">
          <mat-label>User ID</mat-label>
          <input matInput formControlName="userId" required>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Session ID</mat-label>
          <input matInput formControlName="sessionId" required>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Starting Cards</mat-label>
          <input matInput formControlName="startingCards" placeholder="e.g., As Kh">
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Pot Size</mat-label>
          <input matInput type="number" formControlName="potSize">
        </mat-form-field>

        <div class="dialog__field">
          <mat-checkbox formControlName="wasWon">Hand Was Won</mat-checkbox>
        </div>

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
export class HandDialog {
  private fb = inject(FormBuilder);
  private handService = inject(HandService);
  private dialogRef = inject(MatDialog);

  data: Hand | null = null;
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      userId: ['00000000-0000-0000-0000-000000000000', Validators.required],
      sessionId: ['00000000-0000-0000-0000-000000000000', Validators.required],
      startingCards: [''],
      potSize: [''],
      wasWon: [false],
      notes: ['']
    });

    if (this.data) {
      this.form.patchValue({
        userId: this.data.userId,
        sessionId: this.data.sessionId,
        startingCards: this.data.startingCards,
        potSize: this.data.potSize,
        wasWon: this.data.wasWon,
        notes: this.data.notes
      });
    }
  }

  onSubmit() {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.data) {
        const updateData: UpdateHand = {
          handId: this.data.handId,
          userId: formValue.userId,
          sessionId: formValue.sessionId,
          startingCards: formValue.startingCards || undefined,
          potSize: formValue.potSize || undefined,
          wasWon: formValue.wasWon,
          notes: formValue.notes || undefined
        };
        this.handService.updateHand(updateData).subscribe(() => {
          this.dialogRef.closeAll();
        });
      } else {
        const createData: CreateHand = {
          userId: formValue.userId,
          sessionId: formValue.sessionId,
          startingCards: formValue.startingCards || undefined,
          potSize: formValue.potSize || undefined,
          wasWon: formValue.wasWon,
          notes: formValue.notes || undefined
        };
        this.handService.createHand(createData).subscribe(() => {
          this.dialogRef.closeAll();
        });
      }
    }
  }
}

@Component({
  selector: 'app-hands',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  template: `
    <div class="hands">
      <div class="hands__container">
        <div class="hands__header">
          <h1 class="hands__title">Hands</h1>
          <button mat-raised-button color="primary" (click)="openDialog()">
            <mat-icon>add</mat-icon>
            Add Hand
          </button>
        </div>

        <mat-card class="hands__card">
          <table mat-table [dataSource]="hands$ | async" class="hands__table">
            <ng-container matColumnDef="startingCards">
              <th mat-header-cell *matHeaderCellDef>Starting Cards</th>
              <td mat-cell *matCellDef="let hand">{{ hand.startingCards || '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="potSize">
              <th mat-header-cell *matHeaderCellDef>Pot Size</th>
              <td mat-cell *matCellDef="let hand">{{ hand.potSize ? ('$' + (hand.potSize | number:'1.2-2')) : '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="wasWon">
              <th mat-header-cell *matHeaderCellDef>Result</th>
              <td mat-cell *matCellDef="let hand">
                <span [style.color]="hand.wasWon ? 'green' : 'red'">
                  {{ hand.wasWon ? 'Won' : 'Lost' }}
                </span>
              </td>
            </ng-container>

            <ng-container matColumnDef="notes">
              <th mat-header-cell *matHeaderCellDef>Notes</th>
              <td mat-cell *matCellDef="let hand">{{ hand.notes || '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="createdAt">
              <th mat-header-cell *matHeaderCellDef>Created At</th>
              <td mat-cell *matCellDef="let hand">{{ hand.createdAt | date:'short' }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let hand">
                <button mat-icon-button color="primary" (click)="openDialog(hand)">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteHand(hand.handId)">
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
    .hands {
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
export class Hands implements OnInit {
  private handService = inject(HandService);
  private dialog = inject(MatDialog);

  hands$ = this.handService.hands$;
  displayedColumns = ['startingCards', 'potSize', 'wasWon', 'notes', 'createdAt', 'actions'];

  ngOnInit() {
    this.handService.getHands().subscribe();
  }

  openDialog(hand?: Hand) {
    const dialogRef = this.dialog.open(HandDialog, {
      width: '500px'
    });

    if (hand) {
      dialogRef.componentInstance.data = hand;
      dialogRef.componentInstance.form.patchValue({
        userId: hand.userId,
        sessionId: hand.sessionId,
        startingCards: hand.startingCards,
        potSize: hand.potSize,
        wasWon: hand.wasWon,
        notes: hand.notes
      });
    }
  }

  deleteHand(id: string) {
    if (confirm('Are you sure you want to delete this hand?')) {
      this.handService.deleteHand(id).subscribe();
    }
  }
}

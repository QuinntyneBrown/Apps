import { Component, inject, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Game, Platform, Genre, CompletionStatus } from '../../models';

@Component({
  selector: 'app-game-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './game-form-dialog.html',
  styleUrl: './game-form-dialog.scss'
})
export class GameFormDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<GameFormDialog>);

  platforms = Object.values(Platform);
  genres = Object.values(Genre);
  statuses = Object.values(CompletionStatus);

  gameForm: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { game?: Game }) {
    this.gameForm = this._fb.group({
      title: [data.game?.title || '', Validators.required],
      platform: [data.game?.platform || '', Validators.required],
      genre: [data.game?.genre || '', Validators.required],
      status: [data.game?.status || CompletionStatus.NotStarted, Validators.required],
      publisher: [data.game?.publisher || ''],
      developer: [data.game?.developer || ''],
      releaseDate: [data.game?.releaseDate ? new Date(data.game.releaseDate) : null],
      purchaseDate: [data.game?.purchaseDate ? new Date(data.game.purchaseDate) : null],
      purchasePrice: [data.game?.purchasePrice || null],
      rating: [data.game?.rating || null],
      notes: [data.game?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.gameForm.valid) {
      const formValue = this.gameForm.value;
      const result = {
        ...formValue,
        releaseDate: formValue.releaseDate ? formValue.releaseDate.toISOString() : null,
        purchaseDate: formValue.purchaseDate ? formValue.purchaseDate.toISOString() : null
      };
      this._dialogRef.close(result);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}

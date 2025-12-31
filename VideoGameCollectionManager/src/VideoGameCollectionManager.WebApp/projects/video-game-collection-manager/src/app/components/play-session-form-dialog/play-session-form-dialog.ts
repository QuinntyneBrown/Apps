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
import { PlaySession, Game } from '../../models';
import { GamesService } from '../../services';

@Component({
  selector: 'app-play-session-form-dialog',
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
  templateUrl: './play-session-form-dialog.html',
  styleUrl: './play-session-form-dialog.scss'
})
export class PlaySessionFormDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<PlaySessionFormDialog>);
  private _gamesService = inject(GamesService);

  games$ = this._gamesService.games$;
  sessionForm: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { session?: PlaySession }) {
    this.sessionForm = this._fb.group({
      gameId: [data.session?.gameId || '', Validators.required],
      startTime: [data.session?.startTime ? new Date(data.session.startTime) : new Date(), Validators.required],
      endTime: [data.session?.endTime ? new Date(data.session.endTime) : null],
      durationMinutes: [data.session?.durationMinutes || null],
      notes: [data.session?.notes || '']
    });

    this._gamesService.getAll().subscribe();
  }

  onSubmit(): void {
    if (this.sessionForm.valid) {
      const formValue = this.sessionForm.value;
      const result = {
        ...formValue,
        startTime: formValue.startTime ? formValue.startTime.toISOString() : null,
        endTime: formValue.endTime ? formValue.endTime.toISOString() : null
      };
      this._dialogRef.close(result);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}

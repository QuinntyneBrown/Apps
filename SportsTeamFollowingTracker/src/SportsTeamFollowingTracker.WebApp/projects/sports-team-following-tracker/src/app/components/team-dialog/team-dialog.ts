import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { CommonModule } from '@angular/common';
import { Team, Sport } from '../../models';

@Component({
  selector: 'app-team-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule
  ],
  templateUrl: './team-dialog.html',
  styleUrl: './team-dialog.scss'
})
export class TeamDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<TeamDialog>);
  public data = inject<{ team?: Team }>(MAT_DIALOG_DATA);

  teamForm: FormGroup;
  sports = Object.values(Sport);

  constructor() {
    this.teamForm = this._fb.group({
      name: [this.data?.team?.name || '', Validators.required],
      sport: [this.data?.team?.sport || '', Validators.required],
      league: [this.data?.team?.league || ''],
      city: [this.data?.team?.city || ''],
      isFavorite: [this.data?.team?.isFavorite || false]
    });
  }

  onSubmit(): void {
    if (this.teamForm.valid) {
      const formValue = this.teamForm.value;
      if (this.data?.team) {
        this._dialogRef.close({ ...formValue, teamId: this.data.team.teamId });
      } else {
        this._dialogRef.close(formValue);
      }
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}

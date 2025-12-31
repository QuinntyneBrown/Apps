import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { Season } from '../../models';
import { TeamService } from '../../services';

@Component({
  selector: 'app-season-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule
  ],
  templateUrl: './season-dialog.html',
  styleUrl: './season-dialog.scss'
})
export class SeasonDialog implements OnInit {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<SeasonDialog>);
  private _teamService = inject(TeamService);
  public data = inject<{ season?: Season }>(MAT_DIALOG_DATA);

  seasonForm: FormGroup;
  teams$ = this._teamService.teams$;

  constructor() {
    this.seasonForm = this._fb.group({
      teamId: [this.data?.season?.teamId || '', Validators.required],
      seasonName: [this.data?.season?.seasonName || '', Validators.required],
      year: [this.data?.season?.year || new Date().getFullYear(), [Validators.required, Validators.min(1900), Validators.max(2100)]],
      wins: [this.data?.season?.wins || 0, [Validators.required, Validators.min(0)]],
      losses: [this.data?.season?.losses || 0, [Validators.required, Validators.min(0)]],
      notes: [this.data?.season?.notes || '']
    });
  }

  ngOnInit(): void {
    this._teamService.getTeams().subscribe();
  }

  onSubmit(): void {
    if (this.seasonForm.valid) {
      const formValue = this.seasonForm.value;
      if (this.data?.season) {
        this._dialogRef.close({ ...formValue, seasonId: this.data.season.seasonId });
      } else {
        this._dialogRef.close(formValue);
      }
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}

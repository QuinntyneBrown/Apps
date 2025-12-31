import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { Statistic } from '../../models';
import { TeamService } from '../../services';

@Component({
  selector: 'app-statistic-dialog',
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
  templateUrl: './statistic-dialog.html',
  styleUrl: './statistic-dialog.scss'
})
export class StatisticDialog implements OnInit {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<StatisticDialog>);
  private _teamService = inject(TeamService);
  public data = inject<{ statistic?: Statistic }>(MAT_DIALOG_DATA);

  statisticForm: FormGroup;
  teams$ = this._teamService.teams$;

  constructor() {
    this.statisticForm = this._fb.group({
      teamId: [this.data?.statistic?.teamId || '', Validators.required],
      statName: [this.data?.statistic?.statName || '', Validators.required],
      value: [this.data?.statistic?.value || 0, Validators.required],
      recordedDate: [this.data?.statistic ? new Date(this.data.statistic.recordedDate) : new Date(), Validators.required]
    });
  }

  ngOnInit(): void {
    this._teamService.getTeams().subscribe();
  }

  onSubmit(): void {
    if (this.statisticForm.valid) {
      const formValue = this.statisticForm.value;
      if (this.data?.statistic) {
        this._dialogRef.close({ ...formValue, statisticId: this.data.statistic.statisticId });
      } else {
        this._dialogRef.close(formValue);
      }
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}

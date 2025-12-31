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
import { Game, Team } from '../../models';
import { TeamService } from '../../services';

@Component({
  selector: 'app-game-dialog',
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
  templateUrl: './game-dialog.html',
  styleUrl: './game-dialog.scss'
})
export class GameDialog implements OnInit {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<GameDialog>);
  private _teamService = inject(TeamService);
  public data = inject<{ game?: Game }>(MAT_DIALOG_DATA);

  gameForm: FormGroup;
  teams$ = this._teamService.teams$;

  constructor() {
    this.gameForm = this._fb.group({
      teamId: [this.data?.game?.teamId || '', Validators.required],
      gameDate: [this.data?.game ? new Date(this.data.game.gameDate) : new Date(), Validators.required],
      opponent: [this.data?.game?.opponent || '', Validators.required],
      teamScore: [this.data?.game?.teamScore ?? null],
      opponentScore: [this.data?.game?.opponentScore ?? null],
      notes: [this.data?.game?.notes || '']
    });
  }

  ngOnInit(): void {
    this._teamService.getTeams().subscribe();
  }

  onSubmit(): void {
    if (this.gameForm.valid) {
      const formValue = this.gameForm.value;
      if (this.data?.game) {
        this._dialogRef.close({ ...formValue, gameId: this.data.game.gameId });
      } else {
        this._dialogRef.close(formValue);
      }
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}

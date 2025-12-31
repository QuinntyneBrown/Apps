import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { RaceService } from '../../services';
import { Race, RaceType, CreateRaceRequest, UpdateRaceRequest } from '../../models';

@Component({
  selector: 'app-races',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './races.html',
  styleUrl: './races.scss'
})
export class Races implements OnInit {
  private readonly _raceService = inject(RaceService);
  private readonly _dialog = inject(MatDialog);
  private readonly _fb = inject(FormBuilder);

  races$ = this._raceService.races$;
  displayedColumns = ['name', 'raceDate', 'raceType', 'distance', 'isCompleted', 'actions'];

  raceTypes = Object.values(RaceType);

  raceForm = this._fb.group({
    raceId: [''],
    userId: ['00000000-0000-0000-0000-000000000001'],
    name: ['', Validators.required],
    raceType: [RaceType.FiveK, Validators.required],
    raceDate: [new Date(), Validators.required],
    location: ['', Validators.required],
    distance: [0, [Validators.required, Validators.min(0)]],
    finishTimeMinutes: [null as number | null],
    goalTimeMinutes: [null as number | null],
    placement: [null as number | null],
    isCompleted: [false],
    notes: ['']
  });

  isEditing = false;

  ngOnInit() {
    this._raceService.getRaces().subscribe();
  }

  openDialog(templateRef: any, race?: Race) {
    if (race) {
      this.isEditing = true;
      this.raceForm.patchValue({
        ...race,
        raceDate: new Date(race.raceDate)
      });
    } else {
      this.isEditing = false;
      this.raceForm.reset({
        userId: '00000000-0000-0000-0000-000000000001',
        name: '',
        raceType: RaceType.FiveK,
        raceDate: new Date(),
        location: '',
        distance: 0,
        isCompleted: false
      });
    }

    this._dialog.open(templateRef, {
      width: '600px'
    });
  }

  saveRace() {
    if (this.raceForm.valid) {
      const formValue = this.raceForm.value;

      if (this.isEditing && formValue.raceId) {
        const request: UpdateRaceRequest = {
          name: formValue.name!,
          raceType: formValue.raceType!,
          raceDate: formValue.raceDate!.toISOString(),
          location: formValue.location!,
          distance: formValue.distance!,
          finishTimeMinutes: formValue.finishTimeMinutes ?? undefined,
          goalTimeMinutes: formValue.goalTimeMinutes ?? undefined,
          placement: formValue.placement ?? undefined,
          isCompleted: formValue.isCompleted!,
          notes: formValue.notes || undefined
        };

        this._raceService.updateRace(formValue.raceId, request).subscribe(() => {
          this._dialog.closeAll();
        });
      } else {
        const request: CreateRaceRequest = {
          userId: formValue.userId!,
          name: formValue.name!,
          raceType: formValue.raceType!,
          raceDate: formValue.raceDate!.toISOString(),
          location: formValue.location!,
          distance: formValue.distance!,
          finishTimeMinutes: formValue.finishTimeMinutes ?? undefined,
          goalTimeMinutes: formValue.goalTimeMinutes ?? undefined,
          placement: formValue.placement ?? undefined,
          isCompleted: formValue.isCompleted!,
          notes: formValue.notes || undefined
        };

        this._raceService.createRace(request).subscribe(() => {
          this._dialog.closeAll();
        });
      }
    }
  }

  deleteRace(race: Race) {
    if (confirm(`Are you sure you want to delete "${race.name}"?`)) {
      this._raceService.deleteRace(race.raceId).subscribe();
    }
  }

  closeDialog() {
    this._dialog.closeAll();
  }

  getRaceTypeLabel(raceType: RaceType): string {
    const labels: Record<RaceType, string> = {
      [RaceType.FiveK]: '5K',
      [RaceType.TenK]: '10K',
      [RaceType.HalfMarathon]: 'Half Marathon',
      [RaceType.Marathon]: 'Marathon',
      [RaceType.UltraMarathon]: 'Ultra Marathon',
      [RaceType.Trail]: 'Trail',
      [RaceType.Other]: 'Other'
    };
    return labels[raceType];
  }
}

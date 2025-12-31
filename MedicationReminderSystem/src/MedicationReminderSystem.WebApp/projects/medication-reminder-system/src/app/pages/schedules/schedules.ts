import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DoseScheduleService, MedicationService } from '../../services';
import { DoseScheduleCard, DoseScheduleDialog, DoseScheduleDialogData } from '../../components';
import { DoseSchedule, CreateDoseScheduleCommand, UpdateDoseScheduleCommand } from '../../models';

@Component({
  selector: 'app-schedules',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    DoseScheduleCard
  ],
  templateUrl: './schedules.html',
  styleUrl: './schedules.scss'
})
export class Schedules implements OnInit {
  private scheduleService = inject(DoseScheduleService);
  private medicationService = inject(MedicationService);
  private dialog = inject(MatDialog);

  schedules$ = this.scheduleService.doseSchedules$;
  loading$ = this.scheduleService.loading$;

  // Hardcoded userId and medicationId for demo purposes
  private readonly userId = '00000000-0000-0000-0000-000000000001';
  private readonly defaultMedicationId = '00000000-0000-0000-0000-000000000001';

  ngOnInit(): void {
    this.scheduleService.getAll().subscribe();
    this.medicationService.getAll().subscribe();
  }

  onAdd(): void {
    const dialogRef = this.dialog.open(DoseScheduleDialog, {
      width: '600px',
      data: {
        userId: this.userId,
        medicationId: this.defaultMedicationId
      } as DoseScheduleDialogData
    });

    dialogRef.afterClosed().subscribe((result: CreateDoseScheduleCommand | undefined) => {
      if (result) {
        this.scheduleService.create(result).subscribe();
      }
    });
  }

  onEdit(schedule: DoseSchedule): void {
    const dialogRef = this.dialog.open(DoseScheduleDialog, {
      width: '600px',
      data: {
        schedule,
        userId: this.userId,
        medicationId: schedule.medicationId
      } as DoseScheduleDialogData
    });

    dialogRef.afterClosed().subscribe((result: UpdateDoseScheduleCommand | undefined) => {
      if (result) {
        this.scheduleService.update(schedule.doseScheduleId, result).subscribe();
      }
    });
  }

  onDelete(id: string): void {
    if (confirm('Are you sure you want to delete this dose schedule?')) {
      this.scheduleService.delete(id).subscribe();
    }
  }
}

import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MedicationService } from '../../services';
import { MedicationCard, MedicationDialog, MedicationDialogData } from '../../components';
import { Medication, CreateMedicationCommand, UpdateMedicationCommand } from '../../models';

@Component({
  selector: 'app-medications',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    MedicationCard
  ],
  templateUrl: './medications.html',
  styleUrl: './medications.scss'
})
export class Medications implements OnInit {
  private medicationService = inject(MedicationService);
  private dialog = inject(MatDialog);

  medications$ = this.medicationService.medications$;
  loading$ = this.medicationService.loading$;

  // Hardcoded userId for demo purposes
  private readonly userId = '00000000-0000-0000-0000-000000000001';

  ngOnInit(): void {
    this.medicationService.getAll().subscribe();
  }

  onAdd(): void {
    const dialogRef = this.dialog.open(MedicationDialog, {
      width: '600px',
      data: { userId: this.userId } as MedicationDialogData
    });

    dialogRef.afterClosed().subscribe((result: CreateMedicationCommand | undefined) => {
      if (result) {
        this.medicationService.create(result).subscribe();
      }
    });
  }

  onEdit(medication: Medication): void {
    const dialogRef = this.dialog.open(MedicationDialog, {
      width: '600px',
      data: { medication, userId: this.userId } as MedicationDialogData
    });

    dialogRef.afterClosed().subscribe((result: UpdateMedicationCommand | undefined) => {
      if (result) {
        this.medicationService.update(medication.medicationId, result).subscribe();
      }
    });
  }

  onDelete(id: string): void {
    if (confirm('Are you sure you want to delete this medication?')) {
      this.medicationService.delete(id).subscribe();
    }
  }
}

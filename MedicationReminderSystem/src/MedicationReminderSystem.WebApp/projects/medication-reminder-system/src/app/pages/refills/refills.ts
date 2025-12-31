import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RefillService, MedicationService } from '../../services';
import { RefillCard, RefillDialog, RefillDialogData } from '../../components';
import { Refill, CreateRefillCommand, UpdateRefillCommand } from '../../models';

@Component({
  selector: 'app-refills',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    RefillCard
  ],
  templateUrl: './refills.html',
  styleUrl: './refills.scss'
})
export class Refills implements OnInit {
  private refillService = inject(RefillService);
  private medicationService = inject(MedicationService);
  private dialog = inject(MatDialog);

  refills$ = this.refillService.refills$;
  loading$ = this.refillService.loading$;

  // Hardcoded userId and medicationId for demo purposes
  private readonly userId = '00000000-0000-0000-0000-000000000001';
  private readonly defaultMedicationId = '00000000-0000-0000-0000-000000000001';

  ngOnInit(): void {
    this.refillService.getAll().subscribe();
    this.medicationService.getAll().subscribe();
  }

  onAdd(): void {
    const dialogRef = this.dialog.open(RefillDialog, {
      width: '600px',
      data: {
        userId: this.userId,
        medicationId: this.defaultMedicationId
      } as RefillDialogData
    });

    dialogRef.afterClosed().subscribe((result: CreateRefillCommand | undefined) => {
      if (result) {
        this.refillService.create(result).subscribe();
      }
    });
  }

  onEdit(refill: Refill): void {
    const dialogRef = this.dialog.open(RefillDialog, {
      width: '600px',
      data: {
        refill,
        userId: this.userId,
        medicationId: refill.medicationId
      } as RefillDialogData
    });

    dialogRef.afterClosed().subscribe((result: UpdateRefillCommand | undefined) => {
      if (result) {
        this.refillService.update(refill.refillId, result).subscribe();
      }
    });
  }

  onDelete(id: string): void {
    if (confirm('Are you sure you want to delete this refill?')) {
      this.refillService.delete(id).subscribe();
    }
  }
}

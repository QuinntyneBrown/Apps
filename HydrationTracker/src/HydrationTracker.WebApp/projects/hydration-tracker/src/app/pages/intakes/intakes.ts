import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { IntakeService } from '../../services';
import { IntakeCard } from '../../components/intake-card/intake-card';
import { IntakeDialog } from '../../components/intake-dialog/intake-dialog';
import { Intake, CreateIntakeCommand, UpdateIntakeCommand } from '../../models';

@Component({
  selector: 'app-intakes',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    IntakeCard
  ],
  templateUrl: './intakes.html',
  styleUrls: ['./intakes.scss']
})
export class Intakes implements OnInit {
  private intakeService = inject(IntakeService);
  private dialog = inject(MatDialog);

  intakes$ = this.intakeService.intakes$;
  loading$ = this.intakeService.loading$;

  ngOnInit(): void {
    this.intakeService.getIntakes().subscribe();
  }

  onAdd(): void {
    const dialogRef = this.dialog.open(IntakeDialog, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result: CreateIntakeCommand) => {
      if (result) {
        this.intakeService.createIntake(result).subscribe();
      }
    });
  }

  onEdit(intake: Intake): void {
    const dialogRef = this.dialog.open(IntakeDialog, {
      width: '500px',
      data: { intake }
    });

    dialogRef.afterClosed().subscribe((result: UpdateIntakeCommand) => {
      if (result) {
        this.intakeService.updateIntake(intake.intakeId, result).subscribe();
      }
    });
  }

  onDelete(id: string): void {
    if (confirm('Are you sure you want to delete this intake?')) {
      this.intakeService.deleteIntake(id).subscribe();
    }
  }
}

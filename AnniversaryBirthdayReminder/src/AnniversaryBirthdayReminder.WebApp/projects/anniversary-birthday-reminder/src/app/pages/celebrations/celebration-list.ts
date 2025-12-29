import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { map } from 'rxjs/operators';
import { CelebrationService } from '../../services';
import { CelebrationStatus } from '../../models';
import { CelebrationDetailDialog } from './celebration-detail-dialog';

@Component({
  selector: 'app-celebration-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatDialogModule
  ],
  templateUrl: './celebration-list.html',
  styleUrl: './celebration-list.scss'
})
export class CelebrationList {
  private readonly celebrationService = inject(CelebrationService);
  private readonly dialog = inject(MatDialog);

  CelebrationStatus = CelebrationStatus;

  viewModel$ = this.celebrationService.getCelebrations().pipe(
    map(celebrations => ({
      celebrations: celebrations.map(c => ({
        ...c,
        year: new Date(c.celebrationDate).getFullYear(),
        formattedDate: new Date(c.celebrationDate).toLocaleDateString('en-US', {
          month: 'long',
          day: 'numeric',
          year: 'numeric'
        }),
        stars: this.celebrationService.getRatingStars(c.rating)
      })),
      completedCount: celebrations.filter(c => c.status === CelebrationStatus.Completed).length,
      skippedCount: celebrations.filter(c => c.status === CelebrationStatus.Skipped).length
    }))
  );

  openDetails(celebrationId: string): void {
    this.dialog.open(CelebrationDetailDialog, {
      width: '600px',
      data: { celebrationId }
    });
  }

  getStatusIcon(status: CelebrationStatus): string {
    return status === CelebrationStatus.Completed ? 'check_circle' : 'cancel';
  }

  getStatusColor(status: CelebrationStatus): string {
    return status === CelebrationStatus.Completed ? 'primary' : 'warn';
  }
}

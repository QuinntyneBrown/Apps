import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatDividerModule } from '@angular/material/divider';
import { Observable, of } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { CelebrationService } from '../../services';
import { Celebration, CelebrationStatus } from '../../models';

interface DialogData {
  celebrationId: string;
}

@Component({
  selector: 'app-celebration-detail-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatDividerModule
  ],
  templateUrl: './celebration-detail-dialog.html',
  styleUrl: './celebration-detail-dialog.scss'
})
export class CelebrationDetailDialog implements OnInit {
  private readonly celebrationService = inject(CelebrationService);
  private readonly dialogRef = inject(MatDialogRef<CelebrationDetailDialog>);
  readonly data = inject<DialogData>(MAT_DIALOG_DATA);

  celebration$: Observable<{
    celebration: Celebration;
    formattedDate: string;
    stars: string;
  } | null> = of(null);

  CelebrationStatus = CelebrationStatus;

  ngOnInit(): void {
    this.celebration$ = this.celebrationService.getCelebration(this.data.celebrationId).pipe(
      map(celebration => ({
        celebration,
        formattedDate: new Date(celebration.celebrationDate).toLocaleDateString('en-US', {
          weekday: 'long',
          month: 'long',
          day: 'numeric',
          year: 'numeric'
        }),
        stars: this.celebrationService.getRatingStars(celebration.rating)
      }))
    );
  }

  close(): void {
    this.dialogRef.close();
  }
}

import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog } from '@angular/material/dialog';
import { ReadingService } from '../../services';
import { Reading } from '../../models';
import { ReadingCard } from '../../components/reading-card';
import { ReadingDialog, ReadingDialogData } from '../../components/reading-dialog';

@Component({
  selector: 'app-readings-list',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatProgressSpinnerModule,
    ReadingCard
  ],
  templateUrl: './readings-list.html',
  styleUrl: './readings-list.scss'
})
export class ReadingsList implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();

  readings: Reading[] = [];
  filteredReadings: Reading[] = [];
  loading = false;
  error: string | null = null;
  filterForm!: FormGroup;

  // Hardcoded user ID for demo purposes
  private userId = '00000000-0000-0000-0000-000000000001';

  constructor(
    private readingService: ReadingService,
    private dialog: MatDialog,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initializeFilterForm();
    this.loadReadings();

    this.readingService.readings$
      .pipe(takeUntil(this.destroy$))
      .subscribe(readings => {
        this.readings = readings;
        this.applyFilters();
      });

    this.readingService.loading$
      .pipe(takeUntil(this.destroy$))
      .subscribe(loading => this.loading = loading);

    this.readingService.error$
      .pipe(takeUntil(this.destroy$))
      .subscribe(error => this.error = error);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  initializeFilterForm(): void {
    this.filterForm = this.fb.group({
      startDate: [null],
      endDate: [null]
    });

    this.filterForm.valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => this.applyFilters());
  }

  loadReadings(): void {
    this.readingService.getReadingsByUserId(this.userId).subscribe();
  }

  applyFilters(): void {
    let filtered = [...this.readings];

    const startDate = this.filterForm.get('startDate')?.value;
    const endDate = this.filterForm.get('endDate')?.value;

    if (startDate) {
      filtered = filtered.filter(r => new Date(r.measuredAt) >= new Date(startDate));
    }

    if (endDate) {
      const endDateTime = new Date(endDate);
      endDateTime.setHours(23, 59, 59, 999);
      filtered = filtered.filter(r => new Date(r.measuredAt) <= endDateTime);
    }

    this.filteredReadings = filtered;
  }

  onClearFilters(): void {
    this.filterForm.reset();
  }

  onAddReading(): void {
    const dialogRef = this.dialog.open(ReadingDialog, {
      width: '600px',
      data: { userId: this.userId } as ReadingDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.readingService.createReading(result).subscribe();
      }
    });
  }

  onEditReading(reading: Reading): void {
    const dialogRef = this.dialog.open(ReadingDialog, {
      width: '600px',
      data: { reading, userId: this.userId } as ReadingDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.readingService.updateReading(reading.readingId, result).subscribe();
      }
    });
  }

  onDeleteReading(readingId: string): void {
    if (confirm('Are you sure you want to delete this reading?')) {
      this.readingService.deleteReading(readingId).subscribe();
    }
  }
}

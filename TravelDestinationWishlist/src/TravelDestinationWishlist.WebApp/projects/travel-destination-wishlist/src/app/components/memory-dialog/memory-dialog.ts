import { Component, Inject, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Memory, Trip } from '../../models';
import { TripService } from '../../services';

@Component({
  selector: 'app-memory-dialog',
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
  templateUrl: './memory-dialog.html',
  styleUrl: './memory-dialog.scss'
})
export class MemoryDialog implements OnInit {
  private tripService = inject(TripService);
  form: FormGroup;
  trips$ = this.tripService.trips$;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<MemoryDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { memory?: Memory; userId: string }
  ) {
    this.form = this.fb.group({
      tripId: [data.memory?.tripId || '', Validators.required],
      title: [data.memory?.title || '', Validators.required],
      description: [data.memory?.description || ''],
      memoryDate: [data.memory?.memoryDate ? new Date(data.memory.memoryDate) : null, Validators.required],
      photoUrl: [data.memory?.photoUrl || '']
    });
  }

  ngOnInit(): void {
    this.tripService.getTrips(this.data.userId).subscribe();
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        memoryDate: formValue.memoryDate?.toISOString(),
        userId: this.data.userId,
        ...(this.data.memory && { memoryId: this.data.memory.memoryId })
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}

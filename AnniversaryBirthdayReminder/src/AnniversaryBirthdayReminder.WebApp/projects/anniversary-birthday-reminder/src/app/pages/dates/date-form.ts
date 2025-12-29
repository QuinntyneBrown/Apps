import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { of, switchMap } from 'rxjs';
import { DateService } from '../../services';
import { DateType, RecurrencePattern } from '../../models';

@Component({
  selector: 'app-date-form',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatIconModule,
    MatSlideToggleModule
  ],
  templateUrl: './date-form.html',
  styleUrl: './date-form.scss'
})
export class DateForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly dateService = inject(DateService);

  form: FormGroup = this.fb.group({
    personName: ['', Validators.required],
    dateType: [DateType.Birthday, Validators.required],
    dateValue: [null, Validators.required],
    recurrencePattern: [RecurrencePattern.Annual],
    relationship: [''],
    notes: [''],
    isActive: [true]
  });

  dateTypes = Object.values(DateType);
  recurrencePatterns = Object.values(RecurrencePattern);
  isEditMode = false;
  dateId: string | null = null;

  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => {
        this.dateId = params.get('id');
        if (this.dateId) {
          this.isEditMode = true;
          return this.dateService.getDate(this.dateId);
        }
        return of(null);
      })
    ).subscribe(date => {
      if (date) {
        this.form.patchValue({
          personName: date.personName,
          dateType: date.dateType,
          dateValue: new Date(date.dateValue),
          recurrencePattern: date.recurrencePattern,
          relationship: date.relationship,
          notes: date.notes,
          isActive: date.isActive
        });
      }
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.dateId) {
        this.dateService.updateDate(this.dateId, formValue).subscribe(() => {
          this.router.navigate(['/dates']);
        });
      } else {
        this.dateService.createDate(formValue).subscribe(() => {
          this.router.navigate(['/dates']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/dates']);
  }
}

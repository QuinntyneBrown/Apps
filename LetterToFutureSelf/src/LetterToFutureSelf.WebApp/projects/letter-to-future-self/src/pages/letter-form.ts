import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LetterService } from '../services';
import { CreateLetter, UpdateLetter } from '../models';

@Component({
  selector: 'app-letter-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './letter-form.html',
  styleUrl: './letter-form.scss'
})
export class LetterForm implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private letterService = inject(LetterService);

  letterForm: FormGroup;
  isEditMode = false;
  letterId: string | null = null;
  loading$ = this.letterService.loading$;

  constructor() {
    this.letterForm = this.fb.group({
      userId: ['00000000-0000-0000-0000-000000000001', Validators.required],
      subject: ['', [Validators.required, Validators.maxLength(200)]],
      content: ['', [Validators.required, Validators.maxLength(10000)]],
      scheduledDeliveryDate: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.letterId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.letterId;

    if (this.isEditMode && this.letterId) {
      this.letterService.getById(this.letterId).subscribe(letter => {
        this.letterForm.patchValue({
          userId: letter.userId,
          subject: letter.subject,
          content: letter.content,
          scheduledDeliveryDate: new Date(letter.scheduledDeliveryDate)
        });
      });
    }
  }

  onSubmit(): void {
    if (this.letterForm.valid) {
      const formValue = this.letterForm.value;
      const scheduledDate = new Date(formValue.scheduledDeliveryDate);

      if (this.isEditMode && this.letterId) {
        const updateLetter: UpdateLetter = {
          letterId: this.letterId,
          subject: formValue.subject,
          content: formValue.content,
          scheduledDeliveryDate: scheduledDate.toISOString()
        };

        this.letterService.update(updateLetter).subscribe({
          next: () => this.router.navigate(['/letters']),
          error: (error) => console.error('Error updating letter:', error)
        });
      } else {
        const createLetter: CreateLetter = {
          userId: formValue.userId,
          subject: formValue.subject,
          content: formValue.content,
          scheduledDeliveryDate: scheduledDate.toISOString()
        };

        this.letterService.create(createLetter).subscribe({
          next: () => this.router.navigate(['/letters']),
          error: (error) => console.error('Error creating letter:', error)
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/letters']);
  }
}

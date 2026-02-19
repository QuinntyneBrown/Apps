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
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DeliveryScheduleService } from '../services';
import { CreateDeliverySchedule, UpdateDeliverySchedule } from '../models';

@Component({
  selector: 'app-delivery-schedule-form',
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
    MatSelectModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './delivery-schedule-form.html',
  styleUrl: './delivery-schedule-form.scss'
})
export class DeliveryScheduleForm implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private deliveryScheduleService = inject(DeliveryScheduleService);

  scheduleForm: FormGroup;
  isEditMode = false;
  scheduleId: string | null = null;
  loading$ = this.deliveryScheduleService.loading$;

  deliveryMethods = [
    { value: 'Email', label: 'Email' },
    { value: 'SMS', label: 'SMS' },
    { value: 'Push Notification', label: 'Push Notification' },
    { value: 'In-App', label: 'In-App' }
  ];

  constructor() {
    this.scheduleForm = this.fb.group({
      letterId: ['', Validators.required],
      scheduledDateTime: ['', Validators.required],
      deliveryMethod: ['Email', Validators.required],
      recipientContact: ['', [Validators.email]]
    });
  }

  ngOnInit(): void {
    this.scheduleId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.scheduleId;

    if (this.isEditMode && this.scheduleId) {
      this.deliveryScheduleService.getById(this.scheduleId).subscribe(schedule => {
        this.scheduleForm.patchValue({
          letterId: schedule.letterId,
          scheduledDateTime: new Date(schedule.scheduledDateTime),
          deliveryMethod: schedule.deliveryMethod,
          recipientContact: schedule.recipientContact
        });
      });
    }
  }

  onSubmit(): void {
    if (this.scheduleForm.valid) {
      const formValue = this.scheduleForm.value;
      const scheduledDate = new Date(formValue.scheduledDateTime);

      if (this.isEditMode && this.scheduleId) {
        const updateSchedule: UpdateDeliverySchedule = {
          deliveryScheduleId: this.scheduleId,
          scheduledDateTime: scheduledDate.toISOString(),
          deliveryMethod: formValue.deliveryMethod,
          recipientContact: formValue.recipientContact || undefined
        };

        this.deliveryScheduleService.update(updateSchedule).subscribe({
          next: () => this.router.navigate(['/delivery-schedules']),
          error: (error) => console.error('Error updating delivery schedule:', error)
        });
      } else {
        const createSchedule: CreateDeliverySchedule = {
          letterId: formValue.letterId,
          scheduledDateTime: scheduledDate.toISOString(),
          deliveryMethod: formValue.deliveryMethod,
          recipientContact: formValue.recipientContact || undefined
        };

        this.deliveryScheduleService.create(createSchedule).subscribe({
          next: () => this.router.navigate(['/delivery-schedules']),
          error: (error) => console.error('Error creating delivery schedule:', error)
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/delivery-schedules']);
  }
}

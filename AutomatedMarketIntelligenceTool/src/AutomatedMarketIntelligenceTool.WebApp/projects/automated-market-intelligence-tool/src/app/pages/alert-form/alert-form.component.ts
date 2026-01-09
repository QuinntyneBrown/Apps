import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { AlertsService } from '../../services';
import { AlertType, AlertTypeLabels, NotificationPreference, NotificationPreferenceLabels } from '../../models';

@Component({
  selector: 'app-alert-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, MatCheckboxModule],
  templateUrl: './alert-form.component.html',
  styleUrl: './alert-form.component.scss'
})
export class AlertForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly alertsService = inject(AlertsService);

  isEditMode = false;
  alertId: string | null = null;
  alertTypes = Object.entries(AlertTypeLabels).map(([k, v]) => ({ value: Number(k), label: v }));
  notificationPrefs = Object.entries(NotificationPreferenceLabels).map(([k, v]) => ({ value: Number(k), label: v }));

  form = this.fb.group({
    name: ['', [Validators.required]],
    description: [''],
    alertType: [AlertType.Keyword],
    isActive: [true],
    criteria: [''],
    notificationPreference: [NotificationPreference.InApp]
  });

  ngOnInit(): void {
    this.alertId = this.route.snapshot.params['id'];
    this.isEditMode = !!this.alertId && this.alertId !== 'new';
    if (this.isEditMode && this.alertId) {
      this.alertsService.getAlertById(this.alertId).subscribe(alert => this.form.patchValue(alert));
    }
  }

  onSubmit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    if (this.isEditMode && this.alertId) {
      this.alertsService.updateAlert({ alertId: this.alertId, ...this.form.value } as any).subscribe(() => this.router.navigate(['/alerts']));
    } else {
      this.alertsService.createAlert(this.form.value as any).subscribe(() => this.router.navigate(['/alerts']));
    }
  }

  onCancel(): void { this.router.navigate(['/alerts']); }
}

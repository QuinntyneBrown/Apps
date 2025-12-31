import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MaintenanceService, EquipmentService } from '../../services';
import { Equipment } from '../../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-maintenance-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './maintenance-form.html',
  styleUrl: './maintenance-form.scss'
})
export class MaintenanceForm implements OnInit {
  maintenanceForm: FormGroup;
  isEditMode = false;
  maintenanceId: string | null = null;
  equipmentList$: Observable<Equipment[]>;

  constructor(
    private fb: FormBuilder,
    private maintenanceService: MaintenanceService,
    private equipmentService: EquipmentService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.equipmentList$ = this.equipmentService.equipmentList$;
    this.maintenanceForm = this.fb.group({
      userId: ['00000000-0000-0000-0000-000000000000', Validators.required],
      equipmentId: ['', Validators.required],
      maintenanceDate: [new Date(), Validators.required],
      description: ['', Validators.required],
      cost: [''],
      nextMaintenanceDate: [''],
      notes: ['']
    });
  }

  ngOnInit(): void {
    this.equipmentService.getAll().subscribe();
    this.maintenanceId = this.route.snapshot.paramMap.get('id');
    if (this.maintenanceId) {
      this.isEditMode = true;
      this.loadMaintenance(this.maintenanceId);
    }
  }

  loadMaintenance(id: string): void {
    this.maintenanceService.getById(id).subscribe(maintenance => {
      this.maintenanceForm.patchValue({
        userId: maintenance.userId,
        equipmentId: maintenance.equipmentId,
        maintenanceDate: new Date(maintenance.maintenanceDate),
        description: maintenance.description,
        cost: maintenance.cost,
        nextMaintenanceDate: maintenance.nextMaintenanceDate ? new Date(maintenance.nextMaintenanceDate) : null,
        notes: maintenance.notes
      });
    });
  }

  onSubmit(): void {
    if (this.maintenanceForm.valid) {
      const formValue = this.maintenanceForm.value;
      const maintenance = {
        ...formValue,
        maintenanceDate: formValue.maintenanceDate.toISOString(),
        nextMaintenanceDate: formValue.nextMaintenanceDate ? formValue.nextMaintenanceDate.toISOString() : null
      };

      if (this.isEditMode && this.maintenanceId) {
        this.maintenanceService.update(this.maintenanceId, { ...maintenance, maintenanceId: this.maintenanceId }).subscribe({
          next: () => this.router.navigate(['/maintenance']),
          error: (error) => console.error('Error updating maintenance:', error)
        });
      } else {
        this.maintenanceService.create(maintenance).subscribe({
          next: () => this.router.navigate(['/maintenance']),
          error: (error) => console.error('Error creating maintenance:', error)
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/maintenance']);
  }
}

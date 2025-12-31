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
import { MatCheckboxModule } from '@angular/material/checkbox';
import { EquipmentService } from '../../services';
import { EquipmentType } from '../../models';

@Component({
  selector: 'app-equipment-form',
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
    MatNativeDateModule,
    MatCheckboxModule
  ],
  templateUrl: './equipment-form.html',
  styleUrl: './equipment-form.scss'
})
export class EquipmentForm implements OnInit {
  equipmentForm: FormGroup;
  isEditMode = false;
  equipmentId: string | null = null;
  equipmentTypes = [
    { value: EquipmentType.Cardio, label: 'Cardio' },
    { value: EquipmentType.Strength, label: 'Strength' },
    { value: EquipmentType.Flexibility, label: 'Flexibility' },
    { value: EquipmentType.Accessory, label: 'Accessory' }
  ];

  constructor(
    private fb: FormBuilder,
    private equipmentService: EquipmentService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.equipmentForm = this.fb.group({
      userId: ['00000000-0000-0000-0000-000000000000', Validators.required],
      name: ['', Validators.required],
      equipmentType: [EquipmentType.Strength, Validators.required],
      brand: [''],
      model: [''],
      purchaseDate: [''],
      purchasePrice: [''],
      location: [''],
      notes: [''],
      isActive: [true]
    });
  }

  ngOnInit(): void {
    this.equipmentId = this.route.snapshot.paramMap.get('id');
    if (this.equipmentId) {
      this.isEditMode = true;
      this.loadEquipment(this.equipmentId);
    }
  }

  loadEquipment(id: string): void {
    this.equipmentService.getById(id).subscribe(equipment => {
      this.equipmentForm.patchValue({
        userId: equipment.userId,
        name: equipment.name,
        equipmentType: equipment.equipmentType,
        brand: equipment.brand,
        model: equipment.model,
        purchaseDate: equipment.purchaseDate ? new Date(equipment.purchaseDate) : null,
        purchasePrice: equipment.purchasePrice,
        location: equipment.location,
        notes: equipment.notes,
        isActive: equipment.isActive
      });
    });
  }

  onSubmit(): void {
    if (this.equipmentForm.valid) {
      const formValue = this.equipmentForm.value;
      const equipment = {
        ...formValue,
        purchaseDate: formValue.purchaseDate ? formValue.purchaseDate.toISOString() : null
      };

      if (this.isEditMode && this.equipmentId) {
        this.equipmentService.update(this.equipmentId, { ...equipment, equipmentId: this.equipmentId }).subscribe({
          next: () => this.router.navigate(['/equipment']),
          error: (error) => console.error('Error updating equipment:', error)
        });
      } else {
        this.equipmentService.create(equipment).subscribe({
          next: () => this.router.navigate(['/equipment']),
          error: (error) => console.error('Error creating equipment:', error)
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/equipment']);
  }
}

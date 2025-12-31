import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { WorkoutMappingService, EquipmentService } from '../../services';
import { Equipment } from '../../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-workout-mapping-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule
  ],
  templateUrl: './workout-mapping-form.html',
  styleUrl: './workout-mapping-form.scss'
})
export class WorkoutMappingForm implements OnInit {
  workoutMappingForm: FormGroup;
  isEditMode = false;
  workoutMappingId: string | null = null;
  equipmentList$: Observable<Equipment[]>;

  constructor(
    private fb: FormBuilder,
    private workoutMappingService: WorkoutMappingService,
    private equipmentService: EquipmentService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.equipmentList$ = this.equipmentService.equipmentList$;
    this.workoutMappingForm = this.fb.group({
      userId: ['00000000-0000-0000-0000-000000000000', Validators.required],
      equipmentId: ['', Validators.required],
      exerciseName: ['', Validators.required],
      muscleGroup: [''],
      instructions: [''],
      isFavorite: [false]
    });
  }

  ngOnInit(): void {
    this.equipmentService.getAll().subscribe();
    this.workoutMappingId = this.route.snapshot.paramMap.get('id');
    if (this.workoutMappingId) {
      this.isEditMode = true;
      this.loadWorkoutMapping(this.workoutMappingId);
    }
  }

  loadWorkoutMapping(id: string): void {
    this.workoutMappingService.getById(id).subscribe(workoutMapping => {
      this.workoutMappingForm.patchValue({
        userId: workoutMapping.userId,
        equipmentId: workoutMapping.equipmentId,
        exerciseName: workoutMapping.exerciseName,
        muscleGroup: workoutMapping.muscleGroup,
        instructions: workoutMapping.instructions,
        isFavorite: workoutMapping.isFavorite
      });
    });
  }

  onSubmit(): void {
    if (this.workoutMappingForm.valid) {
      const workoutMapping = this.workoutMappingForm.value;

      if (this.isEditMode && this.workoutMappingId) {
        this.workoutMappingService.update(this.workoutMappingId, { ...workoutMapping, workoutMappingId: this.workoutMappingId }).subscribe({
          next: () => this.router.navigate(['/workouts']),
          error: (error) => console.error('Error updating workout mapping:', error)
        });
      } else {
        this.workoutMappingService.create(workoutMapping).subscribe({
          next: () => this.router.navigate(['/workouts']),
          error: (error) => console.error('Error creating workout mapping:', error)
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/workouts']);
  }
}

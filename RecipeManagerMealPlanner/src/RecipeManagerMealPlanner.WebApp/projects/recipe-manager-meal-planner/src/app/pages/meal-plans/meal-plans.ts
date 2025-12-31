import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MealPlanService } from '../../services';
import { MealPlan } from '../../models';

@Component({
  selector: 'app-meal-plans',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './meal-plans.html',
  styleUrls: ['./meal-plans.scss'],
})
export class MealPlans implements OnInit {
  mealPlans$ = this.mealPlanService.mealPlans$;
  loading$ = this.mealPlanService.loading$;
  displayedColumns = ['name', 'mealDate', 'mealType', 'isPrepared', 'actions'];
  userId = '00000000-0000-0000-0000-000000000001';

  constructor(private mealPlanService: MealPlanService) {}

  ngOnInit(): void {
    this.mealPlanService.getMealPlans(this.userId).subscribe();
  }

  onDelete(mealPlanId: string): void {
    if (confirm('Are you sure you want to delete this meal plan?')) {
      this.mealPlanService.deleteMealPlan(mealPlanId).subscribe();
    }
  }

  togglePrepared(mealPlan: MealPlan): void {
    const request = {
      ...mealPlan,
      isPrepared: !mealPlan.isPrepared,
    };
    this.mealPlanService.updateMealPlan(request).subscribe();
  }
}

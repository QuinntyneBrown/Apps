import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MealPlanService } from '../../services';

@Component({
  selector: 'app-meal-plans',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatProgressBarModule
  ],
  templateUrl: './meal-plans.html',
  styleUrl: './meal-plans.scss'
})
export class MealPlans implements OnInit {
  private mealPlanService = inject(MealPlanService);

  mealPlans$ = this.mealPlanService.mealPlans$;

  ngOnInit(): void {
    this.mealPlanService.getMealPlans().subscribe();
  }
}

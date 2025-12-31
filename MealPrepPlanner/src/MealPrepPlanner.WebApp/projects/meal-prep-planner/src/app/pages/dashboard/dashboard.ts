import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MealPlanService } from '../../services';
import { map } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressBarModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private mealPlanService = inject(MealPlanService);

  viewModel$ = this.mealPlanService.activeMealPlan$.pipe(
    map(activePlan => ({
      activePlan,
      hasActivePlan: !!activePlan
    }))
  );

  ngOnInit(): void {
    this.mealPlanService.getMealPlans(undefined, true).subscribe();
  }
}

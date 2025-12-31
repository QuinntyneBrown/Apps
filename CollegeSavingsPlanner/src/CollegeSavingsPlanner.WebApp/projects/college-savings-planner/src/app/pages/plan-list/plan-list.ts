import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Observable } from 'rxjs';
import { Plan } from '../../models';
import { PlanService } from '../../services';

@Component({
  selector: 'app-plan-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './plan-list.html',
  styleUrl: './plan-list.scss'
})
export class PlanList implements OnInit {
  plans$: Observable<Plan[]>;

  constructor(private planService: PlanService) {
    this.plans$ = this.planService.plans$;
  }

  ngOnInit(): void {
    this.planService.getPlans().subscribe();
  }

  deletePlan(id: string): void {
    if (confirm('Are you sure you want to delete this plan?')) {
      this.planService.deletePlan(id).subscribe();
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatListModule } from '@angular/material/list';
import { Observable } from 'rxjs';
import { Plan, Beneficiary, Contribution, Projection } from '../../models';
import { PlanService, BeneficiaryService, ContributionService, ProjectionService } from '../../services';

@Component({
  selector: 'app-plan-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTabsModule,
    MatListModule
  ],
  templateUrl: './plan-detail.html',
  styleUrl: './plan-detail.scss'
})
export class PlanDetail implements OnInit {
  plan$: Observable<Plan | null>;
  beneficiaries$: Observable<Beneficiary[]>;
  contributions$: Observable<Contribution[]>;
  projections$: Observable<Projection[]>;
  planId: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private planService: PlanService,
    private beneficiaryService: BeneficiaryService,
    private contributionService: ContributionService,
    private projectionService: ProjectionService
  ) {
    this.plan$ = this.planService.selectedPlan$;
    this.beneficiaries$ = this.beneficiaryService.beneficiaries$;
    this.contributions$ = this.contributionService.contributions$;
    this.projections$ = this.projectionService.projections$;
  }

  ngOnInit(): void {
    this.planId = this.route.snapshot.paramMap.get('id') || '';
    if (this.planId) {
      this.loadPlanDetails();
    }
  }

  loadPlanDetails(): void {
    this.planService.getPlanById(this.planId).subscribe();
    this.beneficiaryService.getBeneficiaries(this.planId).subscribe();
    this.contributionService.getContributions(this.planId).subscribe();
    this.projectionService.getProjections(this.planId).subscribe();
  }

  deletePlan(): void {
    if (confirm('Are you sure you want to delete this plan?')) {
      this.planService.deletePlan(this.planId).subscribe(() => {
        this.router.navigate(['/plans']);
      });
    }
  }
}

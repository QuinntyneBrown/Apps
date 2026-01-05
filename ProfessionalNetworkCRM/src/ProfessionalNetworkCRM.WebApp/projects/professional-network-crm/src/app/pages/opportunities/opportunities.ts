import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatChipsModule } from '@angular/material/chips';
import { Observable } from 'rxjs';
import { OpportunitiesService } from '../../services';
import { Opportunity, OpportunityType, OpportunityStatus } from '../../models';

@Component({
  selector: 'app-opportunities',
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatListModule,
    MatChipsModule
  ],
  templateUrl: './opportunities.html',
  styleUrl: './opportunities.scss',
})
export class Opportunities implements OnInit {
  private opportunitiesService = inject(OpportunitiesService);

  opportunities$: Observable<Opportunity[]> = this.opportunitiesService.opportunities$;

  OpportunityType = OpportunityType;
  OpportunityStatus = OpportunityStatus;

  ngOnInit(): void {
    this.opportunitiesService.loadOpportunities().subscribe();
  }

  getOpportunityTypeLabel(type: OpportunityType): string {
    return OpportunityType[type];
  }

  getOpportunityStatusLabel(status: OpportunityStatus): string {
    return OpportunityStatus[status];
  }

  getStatusColor(status: OpportunityStatus): string {
    switch (status) {
      case OpportunityStatus.Won:
        return 'primary';
      case OpportunityStatus.Pursuing:
        return 'accent';
      case OpportunityStatus.Lost:
        return 'warn';
      default:
        return '';
    }
  }
}

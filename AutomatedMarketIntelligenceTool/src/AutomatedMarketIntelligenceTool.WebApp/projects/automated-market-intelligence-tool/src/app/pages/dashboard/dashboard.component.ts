import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatDividerModule } from '@angular/material/divider';
import { combineLatest, map, Observable } from 'rxjs';
import { CompetitorsService, InsightsService, AlertsService } from '../../services';
import { Competitor, Insight, Alert, InsightCategoryLabels, InsightImpactLabels, MarketPositionLabels } from '../../models';

interface DashboardViewModel {
  competitorsCount: number;
  topCompetitors: Competitor[];
  insightsCount: number;
  actionableInsights: number;
  recentInsights: Insight[];
  activeAlerts: number;
  triggeredAlerts: Alert[];
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
    MatDividerModule
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class Dashboard implements OnInit {
  private readonly competitorsService = inject(CompetitorsService);
  private readonly insightsService = inject(InsightsService);
  private readonly alertsService = inject(AlertsService);

  InsightCategoryLabels = InsightCategoryLabels;
  InsightImpactLabels = InsightImpactLabels;
  MarketPositionLabels = MarketPositionLabels;

  viewModel$: Observable<DashboardViewModel> = combineLatest([
    this.competitorsService.competitors$,
    this.insightsService.insights$,
    this.alertsService.alerts$
  ]).pipe(
    map(([competitors, insights, alerts]) => ({
      competitorsCount: competitors.length,
      topCompetitors: competitors.slice(0, 3),
      insightsCount: insights.length,
      actionableInsights: insights.filter(i => i.isActionable).length,
      recentInsights: insights.slice(0, 3),
      activeAlerts: alerts.filter(a => a.isActive).length,
      triggeredAlerts: alerts.filter(a => a.lastTriggered).slice(0, 3)
    }))
  );

  ngOnInit(): void {
    this.competitorsService.loadCompetitors().subscribe();
    this.insightsService.loadInsights().subscribe();
    this.alertsService.loadAlerts().subscribe();
  }

  getCompetitorInitials(name: string): string {
    return name
      .split(' ')
      .map(word => word[0])
      .join('')
      .substring(0, 2)
      .toUpperCase();
  }
}

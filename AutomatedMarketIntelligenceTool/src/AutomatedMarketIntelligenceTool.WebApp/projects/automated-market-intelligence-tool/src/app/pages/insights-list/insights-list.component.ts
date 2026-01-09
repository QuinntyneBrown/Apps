import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { InsightsService } from '../../services';
import { InsightCategoryLabels, InsightImpactLabels } from '../../models';

@Component({
  selector: 'app-insights-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatChipsModule],
  templateUrl: './insights-list.component.html',
  styleUrl: './insights-list.component.scss'
})
export class InsightsList implements OnInit {
  private readonly insightsService = inject(InsightsService);
  insights$ = this.insightsService.insights$;
  displayedColumns = ['title', 'category', 'impact', 'isActionable', 'actions'];
  InsightCategoryLabels = InsightCategoryLabels;
  InsightImpactLabels = InsightImpactLabels;

  ngOnInit(): void { this.insightsService.loadInsights().subscribe(); }
  onDelete(id: string): void {
    if (confirm('Delete this insight?')) this.insightsService.deleteInsight(id).subscribe();
  }
}

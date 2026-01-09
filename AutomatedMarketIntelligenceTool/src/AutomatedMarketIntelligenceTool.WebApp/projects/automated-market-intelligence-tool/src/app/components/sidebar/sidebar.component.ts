import { Component, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { CompetitorsService, InsightsService, AlertsService } from '../../services';
import { map } from 'rxjs';

interface MenuItem {
  path: string;
  label: string;
  icon: string;
  exact?: boolean;
  count$?: ReturnType<typeof map>;
}

interface MenuSection {
  title: string;
  items: MenuItem[];
}

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    MatListModule,
    MatIconModule,
    MatDividerModule
  ],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class Sidebar {
  private readonly competitorsService = inject(CompetitorsService);
  private readonly insightsService = inject(InsightsService);
  private readonly alertsService = inject(AlertsService);

  competitorsCount$ = this.competitorsService.competitors$.pipe(
    map(competitors => competitors.length)
  );

  insightsCount$ = this.insightsService.insights$.pipe(
    map(insights => insights.filter(i => i.isActionable).length)
  );

  activeAlertsCount$ = this.alertsService.alerts$.pipe(
    map(alerts => alerts.filter(a => a.isActive).length)
  );

  menuSections: MenuSection[] = [
    {
      title: 'Main',
      items: [
        { path: '/', label: 'Dashboard', icon: 'dashboard', exact: true }
      ]
    },
    {
      title: 'Intelligence',
      items: [
        { path: '/competitors', label: 'Competitors', icon: 'business', count$: this.competitorsCount$ },
        { path: '/insights', label: 'Insights', icon: 'lightbulb', count$: this.insightsCount$ },
        { path: '/alerts', label: 'Alerts', icon: 'notifications_active', count$: this.activeAlertsCount$ }
      ]
    },
    {
      title: 'Analysis',
      items: [
        { path: '/trends', label: 'Trends', icon: 'trending_up' },
        { path: '/reports', label: 'Reports', icon: 'assessment' },
        { path: '/sources', label: 'Data Sources', icon: 'source' }
      ]
    },
    {
      title: 'Settings',
      items: [
        { path: '/settings', label: 'Settings', icon: 'settings' },
        { path: '/analytics', label: 'Analytics', icon: 'analytics' }
      ]
    }
  ];
}

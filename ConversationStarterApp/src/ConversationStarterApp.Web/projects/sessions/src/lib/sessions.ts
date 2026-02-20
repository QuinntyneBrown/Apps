import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { SessionService } from '@lib/api';
import { Session } from '@lib/api';
import {
  PageHeaderComponent,
  StreakBannerComponent,
  StatCardComponent,
  SectionHeaderComponent,
  InsightCardComponent
} from '@lib/components';

@Component({
  selector: 'app-sessions',
  standalone: true,
  imports: [
    CommonModule,
    PageHeaderComponent,
    StreakBannerComponent,
    StatCardComponent,
    SectionHeaderComponent,
    InsightCardComponent
  ],
  templateUrl: './sessions.html',
  styleUrl: './sessions.scss'
})
export class Sessions implements OnInit {
  sessions$: Observable<Session[]>;

  streakDays = [
    { label: 'M', completed: true },
    { label: 'T', completed: true },
    { label: 'W', completed: true },
    { label: 'T', completed: true },
    { label: 'F', completed: true },
    { label: 'S', completed: true },
    { label: 'S', completed: false }
  ];

  insights = [
    {
      title: 'Conversation Quality Rising',
      description: 'Your average rating improved 15% this month',
      iconColor: 'success' as const
    },
    {
      title: 'Most Popular Category',
      description: 'Romantic prompts are your favorite — 60% of sessions',
      iconColor: 'accent' as const
    }
  ];

  private readonly userId = '00000000-0000-0000-0000-000000000001';

  constructor(private sessionService: SessionService) {
    this.sessions$ = this.sessionService.sessions$;
  }

  ngOnInit(): void {
    this.sessionService.getByUserId(this.userId).subscribe();
  }
}

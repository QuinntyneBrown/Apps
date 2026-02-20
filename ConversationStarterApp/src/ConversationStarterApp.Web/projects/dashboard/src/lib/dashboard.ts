import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { PromptService, SessionService } from '@lib/api';
import { Prompt, Session, CategoryLabels, DepthLabels } from '@lib/api';
import {
  DailyPromptHeroComponent,
  StatCardComponent,
  SectionHeaderComponent,
  ConversationCardComponent
} from '@lib/components';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    DailyPromptHeroComponent,
    StatCardComponent,
    SectionHeaderComponent,
    ConversationCardComponent
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  randomPrompt$: Observable<Prompt | null>;
  recentSessions$: Observable<Session[]>;
  categoryLabels = CategoryLabels;
  depthLabels = DepthLabels;

  private readonly userId = '00000000-0000-0000-0000-000000000001';

  constructor(
    private promptService: PromptService,
    private sessionService: SessionService,
    private router: Router
  ) {
    this.randomPrompt$ = this.promptService.currentPrompt$;
    this.recentSessions$ = this.sessionService.sessions$;
  }

  ngOnInit(): void {
    this.loadRandomPrompt();
    this.loadRecentSessions();
  }

  loadRandomPrompt(): void {
    this.promptService.getRandom().subscribe();
  }

  loadRecentSessions(): void {
    this.sessionService.getRecent(this.userId, 5).subscribe();
  }

  usePrompt(): void {
    this.promptService.currentPrompt$.subscribe(prompt => {
      if (prompt) {
        this.promptService.incrementUsage(prompt.promptId).subscribe();
      }
    }).unsubscribe();
  }

  skipPrompt(): void {
    this.loadRandomPrompt();
  }
}

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Observable } from 'rxjs';
import { PromptService, SessionService } from '../../services';
import { Prompt, Session, CategoryLabels, DepthLabels } from '../../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  randomPrompt$: Observable<Prompt | null>;
  recentSessions$: Observable<Session[]>;
  categoryLabels = CategoryLabels;
  depthLabels = DepthLabels;

  // For demo purposes - in a real app, this would come from auth
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

  usePrompt(prompt: Prompt): void {
    this.promptService.incrementUsage(prompt.promptId).subscribe();
  }

  navigateToPrompts(): void {
    this.router.navigate(['/prompts']);
  }

  navigateToSessions(): void {
    this.router.navigate(['/sessions']);
  }
}

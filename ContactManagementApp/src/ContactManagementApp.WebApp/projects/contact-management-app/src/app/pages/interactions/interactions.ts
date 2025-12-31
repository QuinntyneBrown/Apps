import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { InteractionService } from '../../services';
import { Interaction } from '../../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-interactions',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './interactions.html',
  styleUrl: './interactions.scss'
})
export class Interactions implements OnInit {
  private interactionService = inject(InteractionService);

  interactions$!: Observable<Interaction[]>;
  loading$!: Observable<boolean>;

  private userId = '00000000-0000-0000-0000-000000000001';

  ngOnInit(): void {
    this.interactions$ = this.interactionService.interactions$;
    this.loading$ = this.interactionService.loading$;
    this.loadInteractions();
  }

  loadInteractions(): void {
    this.interactionService.getInteractions(this.userId).subscribe();
  }
}

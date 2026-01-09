import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { CompetitorsService } from '../../services';
import { MarketPositionLabels } from '../../models';

@Component({
  selector: 'app-competitors-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipsModule
  ],
  templateUrl: './competitors-list.component.html',
  styleUrl: './competitors-list.component.scss'
})
export class CompetitorsList implements OnInit {
  private readonly competitorsService = inject(CompetitorsService);

  competitors$ = this.competitorsService.competitors$;
  displayedColumns = ['name', 'industry', 'marketPosition', 'actions'];
  MarketPositionLabels = MarketPositionLabels;

  ngOnInit(): void {
    this.competitorsService.loadCompetitors().subscribe();
  }

  onDelete(id: string): void {
    if (confirm('Are you sure you want to delete this competitor?')) {
      this.competitorsService.deleteCompetitor(id).subscribe();
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { DateIdeaService } from '../../services';
import { Observable } from 'rxjs';
import { DateIdea } from '../../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatGridListModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  dateIdeas$!: Observable<DateIdea[]>;
  stats = {
    totalIdeas: 0,
    favorites: 0,
    tried: 0
  };

  constructor(private dateIdeaService: DateIdeaService) {}

  ngOnInit(): void {
    this.dateIdeas$ = this.dateIdeaService.dateIdeas$;
    this.loadStats();
  }

  loadStats(): void {
    this.dateIdeaService.getAll().subscribe(ideas => {
      this.stats.totalIdeas = ideas.length;
      this.stats.favorites = ideas.filter(i => i.isFavorite).length;
      this.stats.tried = ideas.filter(i => i.hasBeenTried).length;
    });
  }
}

import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { RouteService } from '../services';

@Component({
  selector: 'app-routes',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  templateUrl: './routes.html',
  styleUrl: './routes.scss'
})
export class RoutesPage implements OnInit {
  private readonly routeService = inject(RouteService);

  routes$ = this.routeService.routes$;
  displayedColumns = ['name', 'startLocation', 'endLocation', 'distanceMiles', 'difficulty', 'isFavorite', 'actions'];

  ngOnInit(): void {
    this.routeService.getAll().subscribe();
  }

  deleteRoute(id: string): void {
    if (confirm('Are you sure you want to delete this route?')) {
      this.routeService.delete(id).subscribe();
    }
  }
}

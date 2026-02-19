import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule, MatToolbarModule, MatButtonModule, MatIconModule],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class Header {
  navLinks = [
    { path: '/dashboard', label: 'Dashboard' },
    { path: '/watchlist', label: 'Watchlist' },
    { path: '/favorites', label: 'Favorites' },
    { path: '/ratings', label: 'Ratings' },
    { path: '/reviews', label: 'Reviews' },
    { path: '/history', label: 'History' },
    { path: '/statistics', label: 'Statistics' },
    { path: '/discover', label: 'Discover' }
  ];
}

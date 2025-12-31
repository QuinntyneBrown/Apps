import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, MatToolbarModule, MatButtonModule],
  template: `
    <mat-toolbar color="primary" class="header">
      <span class="header__title">Family Tree Builder</span>
      <nav class="header__nav">
        <a mat-button routerLink="/persons" class="header__nav-link">Persons</a>
        <a mat-button routerLink="/relationships" class="header__nav-link">Relationships</a>
        <a mat-button routerLink="/stories" class="header__nav-link">Stories</a>
        <a mat-button routerLink="/photos" class="header__nav-link">Photos</a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 0 16px;
    }

    .header__title {
      font-size: 20px;
      font-weight: 500;
    }

    .header__nav {
      display: flex;
      gap: 8px;
    }

    .header__nav-link {
      color: white;
    }
  `]
})
export class Header {}

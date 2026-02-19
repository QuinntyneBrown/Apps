import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatToolbarModule, MatButtonModule, MatIconModule, RouterLink],
  template: `
    <mat-toolbar color="primary" class="header">
      <span class="header__title" routerLink="/">Bucket List Manager</span>
      <span class="header__spacer"></span>
      <button mat-button routerLink="/">
        <mat-icon>home</mat-icon>
        Dashboard
      </button>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      &__title {
        font-size: 1.5rem;
        font-weight: 500;
        cursor: pointer;
      }

      &__spacer {
        flex: 1 1 auto;
      }
    }
  `]
})
export class Header {}

import { Component, inject } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { AsyncPipe } from '@angular/common';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
    AsyncPipe
  ],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  title = 'Family Calendar Event Planner Admin';
  private breakpointObserver = inject(BreakpointObserver);
  isLargeScreen$ = this.breakpointObserver.observe([
    Breakpoints.Medium,
    Breakpoints.Large,
    Breakpoints.XLarge
  ]).pipe(
    map(result => result.matches)
  );
}

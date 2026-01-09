import { Component, ViewChild, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatSidenavModule, MatSidenav } from '@angular/material/sidenav';
import { BreakpointObserver } from '@angular/cdk/layout';
import { Header } from '../../components/header';
import { Sidebar } from '../../components/sidebar';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    MatSidenavModule,
    Header,
    Sidebar
  ],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.scss'
})
export class MainLayout {
  @ViewChild('sidenav') sidenav!: MatSidenav;

  private readonly breakpointObserver = inject(BreakpointObserver);

  isMobile = false;
  sidenavMode: 'over' | 'side' = 'side';
  sidenavOpened = true;

  constructor() {
    this.breakpointObserver
      .observe(['(max-width: 1023px)'])
      .subscribe(result => {
        this.isMobile = result.matches;

        if (this.isMobile) {
          this.sidenavMode = 'over';
          this.sidenavOpened = false;
        } else {
          this.sidenavMode = 'side';
          this.sidenavOpened = true;
        }
      });
  }

  toggleSidenav(): void {
    this.sidenav.toggle();
  }
}

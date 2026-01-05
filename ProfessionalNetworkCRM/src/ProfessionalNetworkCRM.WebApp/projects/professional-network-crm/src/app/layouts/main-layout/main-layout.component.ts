import { Component, ViewChild, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatSidenavModule, MatSidenav } from '@angular/material/sidenav';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { CommonModule } from '@angular/common';
import { Header, Sidebar } from '../../components';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, MatSidenavModule, Header, Sidebar],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.scss'
})
export class MainLayout {
  @ViewChild('sidenav') sidenav!: MatSidenav;
  
  private breakpointObserver = inject(BreakpointObserver);
  
  // Responsive sidenav mode and state
  isMobile = false;
  sidenavMode: 'over' | 'side' = 'side';
  sidenavOpened = true;

  constructor() {
    // Observe breakpoint changes
    this.breakpointObserver
      .observe(['(max-width: 1023px)'])
      .subscribe(result => {
        this.isMobile = result.matches;
        
        if (this.isMobile) {
          // Mobile: overlay mode, closed by default
          this.sidenavMode = 'over';
          this.sidenavOpened = false;
        } else {
          // Desktop: side mode, open by default
          this.sidenavMode = 'side';
          this.sidenavOpened = true;
        }
      });
  }

  toggleSidenav(): void {
    this.sidenav.toggle();
  }
}
